//
//  KeyboardBuffer.cs
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
    using Microsoft.Xna.Framework.Input;

    public sealed class KeyboardBuffer : Buffer<Keys>
    {
        private KeyboardState
            prevKeyState,
            currentKeyState;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardBuffer"/> class.
        /// </summary>
        public KeyboardBuffer()
            : base()
        {
            this.prevKeyState = Keyboard.GetState();
        }

        /// <summary>
        /// Get the pressed Keys and send them to the buffer
        /// awaiting to be processed.
        /// </summary>
        public void Update()
        {
            this.currentKeyState = Keyboard.GetState();

            foreach (Keys key in this.currentKeyState.GetPressedKeys())
            {
                if (this.CheckKey(key))
                {
                    Enqueue(key);
                }
            }

            this.prevKeyState = this.currentKeyState;
        }

        /// <summary>
        /// Check if any of the keys was pressed. Returns true once per key press.
        /// </summary>
        /// <param name="keysDown">The key to check for.</param>
        /// <returns>True if any of the keys is down for the first time. Otherwise false.</returns>
        private bool CheckKey(Keys key)
        {
            if (this.prevKeyState.IsKeyUp(key) &&
                this.currentKeyState.IsKeyDown(key))
            {
                return true;
            }

            return false;
        }
    }
}