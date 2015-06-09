﻿//
//  Actor.cs
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
using System.Text;

namespace RLG.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using RLG.Contracts;
    using RLG.Enumerations;
    using RLG.Framework;
    using RLG.Utilities;

    public class Actor : GameObject, IActor
    {
        #region Constructors

        public Actor(string name, string drawStr, IPropertyBag properties, IMap map, Flags flags, byte volume)
            : base(name, drawStr, volume, flags)
        {
            this.Properties = properties;
            this.CurrentMap = map;

            // Set defaults
            this.Position = new Point();
            this.Properties["speed"] = 10;
        }

        public Actor(string name, string drawStr, byte volume)
            : this(name, drawStr, new PropertyBag(), null, new Flags(), volume)
        {
        }

        #endregion

        #region Properties

        public Point Position { get; set; }

        public IMap CurrentMap { get; set; }

        public IPropertyBag Properties { get; set; }

        #endregion

        public uint Move(CardinalDirection direction)
        {
            if (this.CurrentMap == null)
            {
                throw new ArgumentNullException(
                    "map",
                    "When calling Move() on an IActor, IActor.Map cannot be null!");
            }

            Point newPosition = this.Position + direction.GetDeltaCoordinate();

            string blockingObj = string.Empty;
            if (this.CheckTile(newPosition, out blockingObj))
            {
                this.CurrentMap[this.Position].RemoveObject(this);
                this.CurrentMap[newPosition].AddObject(this);
                this.Position = newPosition;

                return this.CurrentMap[this.Position].Volume;
            }
            else
            {
                // unsuccessful move
                // message log block object
                return 0;
            }
        }

        public bool CheckTile(Point tileCoordinates, out string blockingObject)
        {
            blockingObject = string.Empty;

            if (tileCoordinates.X >= this.CurrentMap.Tiles.Width || tileCoordinates.X < 0
                || tileCoordinates.Y >= this.CurrentMap.Tiles.Height || tileCoordinates.Y < 0)
            {
                return false;
            }

            // Try to get in the tile (returns true if this.Volume + prev.Tile.Volume < Tile.MaxVolume
            if (this.CurrentMap[tileCoordinates].Try(this))
            {
                return true;
            }

            // Otherwise get send a information message
            StringBuilder sb = new StringBuilder();

            sb.Append("There doesn't appear to be enough space for you. There is ");

            string[] gameObjInTile = this.CurrentMap[tileCoordinates]
                .ObjectsContained
                .OrderBy(x => x.Volume)
                .Select(x => x.Name)
                .ToArray();
            
            for (int i = 0; i < gameObjInTile.Length; i++)
            {
                // TODO Improve message for beter coherence.
                if (gameObjInTile[i] != null)
                {
                    sb.AppendFormat("a {0}", gameObjInTile[i]);

                    if (i > gameObjInTile.Length)
                    {
                        // There are more elements in the array, put comma.
                        sb.Append(", ");
                    }
                    else
                    {
                        // This is the last element in the array, put stop.
                        sb.Append(".");
                    }
                }
            }

            blockingObject = sb.ToString();

            return false;
        }
    }
}