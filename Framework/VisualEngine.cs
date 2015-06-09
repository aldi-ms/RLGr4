//
//  VisualEngine.cs
//
//  Author:
//       scienide <alexandar921@abv.bg>
//
//  Copyright (c) 2015 scienide
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace RLG.Framework
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    using RLG.Contracts;
    using RLG.Enumerations;
    using RLG.Framework.FieldOfView;
    using RLG.Utilities;

    public class VisualEngine
    {
        private static readonly Color TileMask = Color.DarkSlateGray;

        private bool highlighterIsOn;
        private int tileSize;
        private Texture2D highlightTexture;
        private List<Point> tilesToHighlight;
        private IMap currentMap;
        private VisualMode mode;
        private Dictionary<string, Texture2D> spriteDict;
        private FieldOfView<ITile> fieldOfView;
        private ContentManager content;

        #region Constructors

        public VisualEngine(
            VisualMode mode, 
            int tileSize,
            Point mapDrawboxTileSize,
            IMap map, 
            ContentManager content = null,
            SpriteFont spriteFont = null)
        {
            this.mode = mode;
            this.tileSize = tileSize;
            this.MapDrawboxTileSize = mapDrawboxTileSize;
            this.Map = map;
            this.FOVSettings = new FOVSettings();

            // Set defaults
            this.TopMargin = 10;
            this.LeftMargin = 10;
            this.spriteDict = new Dictionary<string, Texture2D>();
            this.DeltaTileDrawCoordinates = new Point(0, 0);
            this.highlighterIsOn = false;
            this.tilesToHighlight = new List<Point>();

            this.ASCIIColor = Color.White;
            this.ASCIIRotation = 0f;
            this.ASCIIScale = 1f;
            this.ASCIIOrigin = new Vector2(0, 0);
            this.ASCIIEffects = SpriteEffects.None;
            this.LayerDepth = 0f;

            switch (mode)
            {
                case VisualMode.ASCII:
                    if (spriteFont == null)
                    {
                        throw new ArgumentNullException(
                            "spriteFont",
                            "SpriteFont should be supplied to the constructor if ASCII mode is to be used.");
                    }

                    this.SpriteFont = spriteFont;
                    break;

                case VisualMode.Sprites:
                    if (content == null)
                    {
                        throw new ArgumentNullException(
                            "content",
                            "ContentManager should be supplied to the constructor if Tiles mode is to be used.");
                    }

                    this.content = content;
                    break;
            }
        }

        public VisualEngine(
            VisualMode mode, 
            int tileSize,
            int x,
            int y,
            IMap map, 
            ContentManager content,
            SpriteFont spriteFont)
            : this(mode, tileSize, new Point(x, y), map, content, spriteFont)
        {            
        }

        #endregion

        #region Properties

        public int TileSize
        {
            get
            {
                return this.tileSize;
            }
        }

        public IMap Map
        {
            get
            {
                return this.currentMap;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(
                        "Map",
                        "When setting VisualEngine's currentMap, it cannot be null!");
                }

                this.currentMap = value;
                this.fieldOfView = new FieldOfView<ITile>(this.currentMap.Tiles);
                if (this.mode == VisualMode.Sprites)
                {
                    this.LoadSprites();
                }
            }
        }

        public Point MapDrawboxTileSize { get; set; }

        public int TopMargin { get; set; }

        public int LeftMargin { get; set; }

        public SpriteFont SpriteFont { get; set; }

        public FOVSettings FOVSettings { get; set; }

        public Point DeltaTileDrawCoordinates { get; set; }

        // DrawString properties
        public Color ASCIIColor { get; set; }

        public float ASCIIRotation { get; set; }

        public Vector2 ASCIIOrigin { get; set; }

        public float ASCIIScale { get; set; }

        public SpriteEffects ASCIIEffects { get; set; }

        /// <summary>
        /// Gets or sets the layer depth of the font when using ASCII.
        /// 0 represents the front layer and 1 represents a back layer.
        /// </summary>
        /// <value>The layer depth.</value>
        public float LayerDepth { get; set; }

        #endregion

        public void DrawGame(SpriteBatch spriteBatch, Point mapCenter)
        {
            // TO DO: Draw UI
            this.DrawMap(spriteBatch, mapCenter);
        }

        public void HighlightPath(List<Point> tiles)
        {
            if (tiles == null)
            {
                this.highlighterIsOn = false;
                this.tilesToHighlight.Clear();
                return;
            }

            this.highlighterIsOn = true;
            this.tilesToHighlight = tiles;
        }

        public void DrawGrid(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            Texture2D simpleTexture = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            simpleTexture.SetData<Color>(new[] { Color.White });

            spriteBatch.Begin();

            int xStop = this.MapDrawboxTileSize.X * this.tileSize;
            int yStop = this.MapDrawboxTileSize.Y * this.tileSize;

            for (int x = this.LeftMargin; x <= xStop + this.LeftMargin; x += this.tileSize)
            {
                Rectangle rect = new Rectangle(x, this.LeftMargin, 1, yStop);
                spriteBatch.Draw(simpleTexture, rect, Color.Wheat);
            }

            for (int y = this.TopMargin; y <= yStop + this.TopMargin; y += this.tileSize)
            {
                Rectangle rect = new Rectangle(this.TopMargin, y, xStop, 1);
                spriteBatch.Draw(simpleTexture, rect, Color.Wheat);
            }

            spriteBatch.End();
        }

        private void DrawMap(SpriteBatch spriteBatch, Point mapCenter)
        {
            if (this.highlighterIsOn && this.highlightTexture == null)
            {
                this.highlightTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                this.highlightTexture.SetData(new Color[] { Color.Goldenrod });
            }

            this.fieldOfView.ComputeFov(
                mapCenter.X,
                mapCenter.Y,
                this.FOVSettings.MaxRange,
                this.FOVSettings.LightWalls,
                this.FOVSettings.Method,
                this.FOVSettings.Shape);
            
            #region Coordinates

            // Get the start (Tile)coordinates  for the Map
            Point startTile = new Point(
                                  mapCenter.X - (this.MapDrawboxTileSize.X / 2), 
                                  mapCenter.Y - (this.MapDrawboxTileSize.Y / 2));

            // Check coordinates lower bound < 0
            if (startTile.X < 0)
            {
                startTile.X = 0;
            }

            if (startTile.Y < 0)
            {
                startTile.Y = 0;
            }

            // Check coordinates higher bound > 0
            if (startTile.X + this.MapDrawboxTileSize.X >= this.currentMap.Tiles.Height)
            {
                startTile.X = this.currentMap.Tiles.Height - this.MapDrawboxTileSize.X;
            }

            if (startTile.Y + this.MapDrawboxTileSize.Y >= this.currentMap.Tiles.Width)
            {
                startTile.Y = this.currentMap.Tiles.Width - this.MapDrawboxTileSize.Y;
            }

            #endregion

            Texture2D terrainTexture = null;
            Texture2D itemTexture = null;
            Texture2D fringeTexture = null;
            Texture2D actorTexture = null;

            spriteBatch.Begin();

            for (int x = 0; x < this.MapDrawboxTileSize.X; x++)
            {
                for (int y = 0; y < this.MapDrawboxTileSize.Y; y++)
                {
                    Vector2 drawPosition = new Vector2(
                                               this.LeftMargin + (this.tileSize * x) + this.DeltaTileDrawCoordinates.X,
                                               this.TopMargin + (this.tileSize * y) + this.DeltaTileDrawCoordinates.Y);
                    Point tile = new Point(startTile.X + x, startTile.Y + y);

                    switch (this.mode)
                    {
                        #region ASCII
                        case VisualMode.ASCII:

                            if (this.currentMap[tile].IsVisible)
                            {
                                if (!this.currentMap[tile].Flags.HasFlag(Flags.HasBeenSeen))
                                {
                                    this.currentMap[tile].Flags |= Flags.HasBeenSeen;
                                }

                                if (this.highlighterIsOn && this.tilesToHighlight.Contains(tile))
                                {
                                    Rectangle rect = new Rectangle(
                                                         (int)drawPosition.X - this.DeltaTileDrawCoordinates.X, 
                                                         (int)drawPosition.Y - this.DeltaTileDrawCoordinates.Y, 
                                                         this.tileSize, 
                                                         this.tileSize);
                                    
                                    spriteBatch.Draw(this.highlightTexture, rect, Color.Goldenrod);
                                }

                                spriteBatch.DrawString(
                                    this.SpriteFont,
                                    this.currentMap[tile].DrawString,
                                    drawPosition, 
                                    this.ASCIIColor,
                                    this.ASCIIRotation,
                                    this.ASCIIOrigin,
                                    this.ASCIIScale,
                                    this.ASCIIEffects,
                                    this.LayerDepth);
                            }
                            else if (this.currentMap[tile].Flags.HasFlag(Flags.HasBeenSeen))
                            {                                  
                                spriteBatch.DrawString(
                                    this.SpriteFont, 
                                    this.currentMap[tile].DrawString,
                                    drawPosition,
                                    VisualEngine.TileMask,
                                    this.ASCIIRotation,
                                    this.ASCIIOrigin,
                                    this.ASCIIScale,
                                    this.ASCIIEffects,
                                    this.LayerDepth);
                            }

                            break;
                            #endregion

                        #region Sprites
                        case VisualMode.Sprites:
                            if (this.currentMap[tile].IsVisible)
                            {
                                if (!this.currentMap[tile].Flags.HasFlag(Flags.HasBeenSeen))
                                {
                                    this.currentMap[tile].Flags |= Flags.HasBeenSeen;
                                }

                                // Draw the Terrain first as it should exist for every Tile.
                                terrainTexture = null;
                                if (this.spriteDict.TryGetValue(
                                        this.currentMap[tile].ObjectsContained.GetTerrain().DrawString,
                                        out terrainTexture))
                                {
                                    spriteBatch.Draw(terrainTexture, drawPosition);                                
                                }

                                // Draw existing fringe objects
                                foreach (var fringe in this.currentMap[tile].ObjectsContained.GetFringes())
                                {
                                    fringeTexture = null;
                                    if (this.spriteDict.TryGetValue(fringe.DrawString, out fringeTexture))
                                    {
                                        spriteBatch.Draw(fringeTexture, drawPosition);                                
                                    }
                                }

                                // Draw existing items
                                foreach (var item in this.currentMap[tile].ObjectsContained.GetItems())
                                {
                                    itemTexture = null;
                                    if (this.spriteDict.TryGetValue(item.DrawString, out itemTexture))
                                    {
                                        spriteBatch.Draw(itemTexture, drawPosition);
                                    }
                                }

                                // Draw actor
                                actorTexture = null;
                                if (this.spriteDict.TryGetValue(
                                        this.currentMap[tile].ObjectsContained.GetActor().DrawString,
                                        out actorTexture))
                                {
                                    spriteBatch.Draw(actorTexture, drawPosition);
                                }   
                            }
                            else if (this.currentMap[tile].Flags.HasFlag(Flags.HasBeenSeen))
                            {
                                terrainTexture = null;
                                if (this.spriteDict.TryGetValue(
                                        this.currentMap[tile].ObjectsContained.GetTerrain().DrawString,
                                        out terrainTexture))
                                {
                                    spriteBatch.Draw(terrainTexture, drawPosition, VisualEngine.TileMask); 
                                }
                            }

                            break;
                            #endregion
                    }
                }
            }

            spriteBatch.End();
        }

        private void LoadSprites()
        {
            foreach (var tile in this.currentMap.Tiles)
            {
                foreach (var gameObject in tile.ObjectsContained)
                {
                    if (this.spriteDict.ContainsKey(gameObject.DrawString))
                    {
                        break;
                    }

                    this.spriteDict.Add(
                        gameObject.DrawString, 
                        this.content.Load<Texture2D>(gameObject.DrawString));
                }
            }
        }
    }
}