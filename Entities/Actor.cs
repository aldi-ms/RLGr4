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

    public class Actor : IActor
    {
        private byte volume;
        private string drawString;

        #region Constructors

        public Actor(string name, IPropertyBag statistics, IMap map, byte volume)
        { 
            this.Name = name;
            this.Properties = statistics;
            this.CurrentMap = map;

            this.Volume = volume;
        }

        public Actor(string name, IPropertyBag statistics, IMap map)
            : this(name, statistics, map, 100)
        {
        }

        public Actor(string name, IPropertyBag statistics)
            : this(name, statistics, null, 100)
        {
        }

        #endregion

        #region Properties

        public string Name { get; set; }

        public Point Position { get; set; }

        public IMap CurrentMap { get; set; }

        public IPropertyBag Properties { get; set; }

        public Flags PropertyFlags { get; set; }

        public string DrawString
        {
            get
            {
                return this.drawString;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(
                        "drawString", 
                        "Actor drawString cannot be null!");
                }

                this.drawString = value;
            }
        }

        public byte Volume
        {
            get
            {
                return this.volume;
            }
            set
            {
                if (value > 100)
                {
                    throw new ArgumentException("Actor.Volume cannot be > 100!");
                }

                this.volume = value;
            }
        }

        #endregion

        public int Move(CardinalDirection direction)
        {
            if (this.CurrentMap == null)
            {
                throw new ArgumentNullException(
                    "map",
                    "When calling Move() method on an IActor, IActor.Map cannot be null!");
            }

            Point newPosition = this.Position + direction.GetDeltaCoordinate();

            string blockingObj = string.Empty;
            if (this.CurrentMap.CheckTile(newPosition, out blockingObj))
            {
                this.CurrentMap[this.Position].RemoveObject(this);
                this.CurrentMap[newPosition].AddObject(this);
                this.Position = newPosition;

                return this.CurrentMap[newPosition].ObjectsContained.GetTerrain().MoveCost;
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

