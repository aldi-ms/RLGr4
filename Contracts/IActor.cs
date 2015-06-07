//
//  IActor.cs
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

namespace RLG.Contracts
{
    using Microsoft.Xna.Framework;
    using RLG.Enumerations;

    /// <summary>
    /// Interface for the in-game actors.
    /// </summary>
    public interface IActor : IGameObject
    {
        /// <summary>
        /// Gets or sets the current map on which the actor is residing.
        /// </summary>
        /// <value>The map.</value>
        IMap CurrentMap { get; set; }

        Point Position { get; set; }

        /// <summary>
        /// Gets or sets the statistics of the actor.
        /// </summary>
        /// <value>The statistics.</value>
        IPropertyBag Properties { get; set; }

        /// <summary>
        /// Move the actor in the specified direction on the map.
        /// </summary>
        /// <param name="direction">Direction.</param>
        uint Move(CardinalDirection direction);

        bool CheckTile(Point tileCoordinates, out string blockingObject);
    }
}