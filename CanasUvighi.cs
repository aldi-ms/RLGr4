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
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Storage;

    using RLG.Contracts;
    using RLG.Entities;
    using RLG.Enumerations;
    using RLG.Framework;
    using RLG.Utilities;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class CanasUvighi : Game
    {
        private const int ScreenWidth = 800;
        private const int ScreenHeight = 500;
        private const int MinTurnCost = 100;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        // Custom defined fields
        private bool expectCommand = false;
        private VisualEngine visualEngine;
        private SpriteFont asciiGraphicsFont;
        private SpriteFont logFont;
        private IMessageLog messageLog;
        private KeyboardBuffer keyboardBuffer;
        private ActorPriorityQueue actorQueue;
        private IActor actor;

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
            this.IsMouseVisible = true;

            this.actorQueue = new ActorPriorityQueue();
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
            this.asciiGraphicsFont = this.Content.Load<SpriteFont>("Fonts/BPmono40Bold");
            this.logFont = this.Content.Load<SpriteFont>("Fonts/Consolas12");

            #region Temporary code

            // Create a map for testing purposes
            Point size = new Point(20, 20);
            MapContainer map = new MapContainer(
                                   MapUtilities.GenerateRandomMap(size, VisualMode.ASCII));
            map.LoadTileNeighboors();

            this.visualEngine = new VisualEngine(
                VisualMode.ASCII,
                32,
                new Point(16, 11),
                map,
                null,
                this.asciiGraphicsFont);
            this.visualEngine.DeltaTileDrawCoordinates = new Point(4, -6);
            this.visualEngine.ASCIIScale = 0.7f;

            Rectangle logRect = new Rectangle(
                                    0,
                                    this.visualEngine.MapDrawboxTileSize.Y * this.visualEngine.TileSize,
                                    ScreenWidth - 30,
                                    (ScreenHeight - 30) - (this.visualEngine.MapDrawboxTileSize.Y * this.visualEngine.TileSize));
            
            this.messageLog = new MessageLog(logRect, this.logFont);

            this.actor = new Actor(
                "SCiENiDE",
                "@",
                new PropertyBag(),
                map, 
                Flags.IsPlayerControl | Flags.IsBlocked,
                85);
            
            this.actor.Position = new Point();
            map[this.actor.Position].AddObject(this.actor);
            this.actorQueue.Add(this.actor);

            #endregion
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
                    this.Exit();
                    break;

                default:
                    this.keyboardBuffer.Enqueue(key);
                    break;
            }

            if (!this.expectCommand)
            {
                this.actorQueue.AccumulateEnergy();
            }

            for (int x = 0; x < this.actorQueue.Count; x++)
            {
                IActor current = this.actorQueue[x];
                if (current.Properties["energy"] >= MinTurnCost)
                {
                    if (current.Flags.HasFlag(Flags.IsPlayerControl))
                    {
                        this.expectCommand = true;

                        switch (this.keyboardBuffer.Dequeue())
                        {
                        #region Energy-dependent keyboard input

                            case Keys.NumPad8:
                            case Keys.K:
                            case Keys.Up:
                                {
                                    current.Properties["energy"] -= current.Move(CardinalDirection.North);
                                    this.expectCommand = false;
                                    this.OnMove(current);
                                    break;
                                }

                            case Keys.NumPad2:
                            case Keys.J:
                            case Keys.Down:
                                {
                                    current.Properties["energy"] -= current.Move(CardinalDirection.South);
                                    this.expectCommand = false;
                                    this.OnMove(current);
                                    break;
                                }

                            case Keys.NumPad4:
                            case Keys.H:
                            case Keys.Left:
                                {
                                    current.Properties["energy"] -= current.Move(CardinalDirection.West);
                                    this.expectCommand = false;
                                    this.OnMove(current);
                                    break;
                                }

                            case Keys.NumPad6:
                            case Keys.L:
                            case Keys.Right:
                                {
                                    current.Properties["energy"] -= current.Move(CardinalDirection.East);
                                    this.expectCommand = false;
                                    this.OnMove(current);
                                    break;
                                }

                            case Keys.NumPad7:
                            case Keys.Y:
                                {
                                    current.Properties["energy"] -= current.Move(CardinalDirection.NorthWest);
                                    this.expectCommand = false;
                                    this.OnMove(current);
                                    break;
                                }

                            case Keys.NumPad9:
                            case Keys.U:
                                {
                                    current.Properties["energy"] -= current.Move(CardinalDirection.NorthEast);
                                    this.expectCommand = false;
                                    this.OnMove(current);
                                    break;
                                }

                            case Keys.NumPad1:
                            case Keys.B:
                                {
                                    current.Properties["energy"] -= current.Move(CardinalDirection.SouthWest);
                                    this.expectCommand = false;
                                    this.OnMove(current);
                                    break;
                                }

                            case Keys.NumPad3:
                            case Keys.N:
                                {
                                    current.Properties["energy"] -= current.Move(CardinalDirection.SouthEast);
                                    this.expectCommand = false;
                                    this.OnMove(current);
                                    break;
                                }

                            default:
                                break;

                            #endregion
                        }
                    }
                }
            }

            base.Update(gameTime);
        }

        protected void OnMove(IActor actor)
        {
            // this.messageLog.SendMessage(string.Format("player: [{0},{1}]", actor.Position.X, actor.Position.Y));
            var path = actor.GetPathTo(actor.CurrentMap[new Point(12, 19)]);
            this.visualEngine.HighlightPath(path);
            if (path == null)
            {
                this.messageLog.SendMessage("no path exists to given coordinates.");
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            this.graphics.GraphicsDevice.Clear(Color.Black);

            this.visualEngine.DrawGame(this.spriteBatch, this.actor.Position);

            // this.visualEngine.DrawGrid(this.GraphicsDevice, this.spriteBatch);
            this.messageLog.DrawLog(this.spriteBatch);

            base.Draw(gameTime);
        }
    }
}