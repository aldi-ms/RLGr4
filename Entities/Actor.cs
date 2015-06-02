//
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

namespace RLG.Entities
{
    using Microsoft.Xna.Framework;
    using System;
    using RLG.Contracts;
    using RLG.Enumerations;
    using RLG.Utilities;

    public class Actor : GameObject, IActor
    {
        #region Constructors

        public Actor(string name, string drawStr, IPropertyBag properties, IMap map, Flags flags, byte volume)
            : base(name, drawStr, volume, flags)
        {
            this.Properties = properties;
            this.CurrentMap = map;

            // Default properties to set
            this.Properties["speed"] = 10;
        }

        public Actor(string name, string drawStr, byte volume)
            : this(name, drawStr, null, null, new Flags(), volume)
        {
        }

        #endregion

        #region Properties

        //TO DO: Implement getters and setters to check values
     
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
            if (this.CurrentMap.CheckTile(newPosition, out blockingObj))
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
    }
}

