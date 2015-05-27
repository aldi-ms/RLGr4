//
//  CanasUvighi.cs
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

namespace RLG
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Storage;
    using Microsoft.Xna.Framework.Input;
    using System;

    using RLG.Contracts;
    using RLG.Enumerations;
    using RLG.Entities;
    using RLG.Framework;
    using RLG.Utilities;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class CanasUvighi : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;


        // Custom defined fields

        private const int ScreenWidth = 800;
        private const int ScreenHeight = 500;

        private VisualEngine visualEngine;
        private SpriteFont ASCIIGraphicsFont;
        private SpriteFont logFont;
        private IMessageLog messageLog;
        private KeyboardBuffer keyboardBuffer;

        public CanasUvighi()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";	            
            this.graphics.IsFullScreen = false;		
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(GraphicsDevice);

            this.graphics.PreferredBackBufferWidth = ScreenWidth;
            this.graphics.PreferredBackBufferHeight = ScreenHeight;
            this.graphics.ApplyChanges();

            this.keyboardBuffer = new KeyboardBuffer();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Font for ASCII graphics
            this.ASCIIGraphicsFont = this.Content.Load<SpriteFont>("Fonts/BPmono40Bold");
            this.logFont = this.Content.Load<SpriteFont>("Fonts/Consolas12");

            #region Temporary code

            // Create a map for testing purposes
            Point size = new Point(10, 10);
            MapContainer map = new MapContainer(
                MapUtilities.GenerateRandomMap(size, VisualMode.ASCII));

            this.visualEngine = new VisualEngine(
                VisualMode.ASCII,
                32,
                size,
                map,
                null,
                ASCIIGraphicsFont);
            this.visualEngine.DeltaTileDrawCoordinates = new Point(4, -4);
            this.visualEngine.ASCIIScale = 0.7f;

            #endregion


            Rectangle logRect = new Rectangle(
                                    0,
                                    map.Tiles.Height * this.visualEngine.TileSize,
                                    ScreenWidth - 30,
                                    (ScreenHeight - 30) - map.Tiles.Height * this.visualEngine.TileSize);
            
            this.messageLog = new MessageLog(logRect, this.logFont);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Update the keyboard buffer to get pressed keys
            this.keyboardBuffer.Update();

            // Get the first waiting (FIFO) key
            Keys key = this.keyboardBuffer.Dequeue();

            // Process the key
            switch (key) 
            {
                case Keys.Escape:
                    Exit();
                    break;   
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            this.visualEngine.DrawGame(this.spriteBatch, new Point(3,3));
            this.visualEngine.DrawGrid(this.GraphicsDevice, this.spriteBatch);
            this.messageLog.DrawLog(this.spriteBatch);

            base.Draw(gameTime);
        }
    }
}

