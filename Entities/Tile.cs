//
//  Tile.cs
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
    using System;
    using System.Collections.Generic;
    using RLG.Contracts;
    using RLG.Enumerations;

    public class Tile : ITile
    {
        private string drawString;
        private List<IGameObject> objectsContained;
        private Flags flags;

        public Tile(string drawString, Flags flags)
        {
            if (string.IsNullOrEmpty(drawString))
            {
                throw new System.ArgumentNullException(
                    "drawString",
                    "Tile drawString cannot be null or empty string!");
            }

            this.drawString = drawString;
            this.flags |= flags;
            this.objectsContained = new List<IGameObject>();
        }

        #region Properties

        public IEnumerable<ITile> Neighboors { get; set; }

        public string Name { get; set; }

        public IEnumerable<IGameObject> ObjectsContained
        {
            get
            {
                return this.objectsContained;
            }
        }

        public Flags PropertyFlags
        {
            get
            {
                Flags cumulativeFlags = this.flags;

                foreach (var gameObject in this.ObjectsContained)
                {
                    cumulativeFlags |= gameObject.PropertyFlags;
                }

                return cumulativeFlags;
            }
            set
            {
                this.flags = value;
            }
        }

        public string DrawString
        {
            get
            {
                return this.drawString;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(
                        "drawString", 
                        "Tile.DrawString cannot be null!");
                }

                this.drawString = value;
            }
        }

        public byte Volume
        {
            get
            {
                byte totalVolume = 0;
                foreach (var obj in this.ObjectsContained)
                {
                    totalVolume += obj.Volume;
                }

                return totalVolume;
            }
            set
            {                 
            }
        }

        public bool IsTransparent
        {
            get
            {
                return this.PropertyFlags.HasFlag(Flags.IsTransparent);
            }
        }

        public bool IsVisible
        {
            get
            {
                return this.PropertyFlags.HasFlag(Flags.IsVisible); 
            }
            set
            {
                if (value)
                {
                    this.PropertyFlags |= Flags.IsVisible;
                }
                else
                {
                    this.PropertyFlags &= ~Flags.IsVisible;
                }
            }
        }

        #endregion

        public bool AddObject(IGameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new System.ArgumentNullException(
                    "gameObject",
                    "On adding game object to a tile, the object cannot be null!");
            }

            // Volume check. Each Tile has 100 volume available, and each object
            // has a volume property.
            if (this.Volume + gameObject.Volume > 100)
            {
                return false;
            }


            this.objectsContained.Add(gameObject);
            return true;
        }

        public bool RemoveObject(IGameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new System.ArgumentNullException(
                    "gameObject",
                    "On removing game object from a tile, the object cannot be null!");
            }

            return this.objectsContained.Remove(gameObject);
        }
    }
}

