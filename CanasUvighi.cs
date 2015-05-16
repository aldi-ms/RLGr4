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
    using RLG.Contracts;
    using RLG.Enumerations;
    using RLG.Framework;
    using RLG.Utilities;
    using System;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class CanasUvighi : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        // Custom defined fields
        private VisualEngine visEngine;
        private SpriteFont sFont;

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
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(GraphicsDevice);

            Point size = new Point(10, 10);
            sFont = this.Content.Load<SpriteFont>("Fonts/BPmono40Bold");
            Entities.MapContainer map = new RLG.Entities.MapContainer(
                Utilities.MapUtilities.GenerateRandomMap(size, VisualMode.ASCII));
            
            this.visEngine = new VisualEngine(
                VisualMode.ASCII,
                32,
                size,
                map,
                null,
                sFont);
            this.visEngine.DeltaTileDrawCoordinates = new Point(4, -4);
            this.visEngine.ASCIIScale = 0.7f;

            //TODO: use this.Content to load your game content here 
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // For Mobile devices, this logic will close the Game when the Back button is pressed
            // Exit() is obsolete on iOS
            #if !__IOS__
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            #endif
            // TODO: Add your update logic here			
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);
            this.visEngine.DrawGame(this.spriteBatch, new Point(3,3));
            //this.visEngine.DrawTileBoxes(this.GraphicsDevice, this.spriteBatch);

            /*
            // Font testing
            spriteBatch.Begin();
            spriteBatch.DrawString(sFont, "ABCDEFGHIJKLMOPQRSTUYWXZ@#.,", new Vector2(100, 100), Color.White, 0f, new Vector2(100, 100), 0.7f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(sFont, "abcdefghijklmopqrstuywxz!?%$)_()[]{}", new Vector2(100, 150), Color.White, 0f, new Vector2(100, 150), 0.5f, SpriteEffects.None, 0f);
            spriteBatch.End();
            */
            base.Draw(gameTime);
        }
    }
}

