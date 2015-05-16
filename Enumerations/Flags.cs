/* *
* Canas Uvighi, a RogueLike Game / RPG project.
* Copyright (C) 2015 Aleksandar Dimitrov (screen name SCiENiDE)
* 
* This program is free software: you can redistribute it and/or modify
* it under the terms of the GNU General Public License as published by
* the Free Software Foundation, either version 3 of the License, or
* (at your option) any later version.
* 
* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.
* 
* You should have received a copy of the GNU General Public License
* along with this program.  If not, see <http://www.gnu.org/licenses/>.
* */

namespace RLG.Enumerations
{
    using System;

    /// <summary>
    /// Existing flags for game objects / elements.
    /// </summary>
    [Flags]
    public enum Flags : uint
    {
        /// <summary>
        /// Indicates do not have any flags active.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates whether the actor is under player control.
        /// </summary>
        IsPlayerControl = 1 << 0,

        /// <summary>
        /// Indicates whether the game object is transparent.
        /// <remarks>Used by Field of View.</remarks>
        /// </summary>
        IsTransparent = 1 << 1,

        /// <summary>
        /// Indicates whether the game object (eg. Tile) is visible.
        /// <remarks>Result of Field of View.</remarks>
        /// </summary>
        IsVisible = 1 << 2,

        /// <summary>
        /// Indicates whether the game object is blocked,
        /// meaning an actor cannot move through it.
        /// </summary>
        IsBlocked = 1 << 3,

        /// <summary>
        /// Indicates whether the Tile has been already seen
        /// by the Field of View.
        /// </summary>
        HasBeenSeen = 1 << 4
    }
}
