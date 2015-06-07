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
    using System.Linq;
    using Microsoft.Xna.Framework;

    using RLG.Contracts;
    using RLG.Enumerations;
    using RLG.Utilities;

    public class Tile : ITile
    {
        public const byte MaxVolume = 125;
        private List<IGameObject> objectsContained;
        private Flags flags;

        public Tile(Point position)
        {
            this.Position = position;
            this.objectsContained = new List<IGameObject>();
            this.flags = new Flags();
        }

        public Tile(Point position, params IGameObject[] objectsToFill)
            : this(position)
        {
            foreach (IGameObject gameObject in objectsToFill)
            {
                this.AddObject(gameObject);
            }
        }

        #region Properties

        public Point Position { get; private set; }

        public IEnumerable<ITile> Neighboors { get; set; }

        public IEnumerable<IGameObject> ObjectsContained
        {
            get
            {
                return this.objectsContained;
            }
        }

        public Flags Flags
        {
            get
            {
                Flags cumulativeFlags = this.flags;

                foreach (var gameObject in this.ObjectsContained)
                {
                    cumulativeFlags |= gameObject.Flags;
                }

                return cumulativeFlags;
            }

            set
            {
                this.flags = value;
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
                return this.Flags.HasFlag(Flags.IsTransparent);
            }
        }

        public bool IsVisible
        {
            get
            {
                return this.Flags.HasFlag(Flags.IsVisible); 
            }

            set
            {
                if (value)
                {
                    this.Flags |= Flags.IsVisible;
                }
                else
                {
                    this.Flags &= ~Flags.IsVisible;
                }
            }
        }

        public string DrawString
        {
            get
            {
                IActor tileActor = this.ObjectsContained.GetActor();
                if (tileActor != null)
                {
                    return tileActor.DrawString;
                }

                return this.ObjectsContained
                    .OrderByDescending(x => x.Volume)
                    .FirstOrDefault()
                    .DrawString;
            }

            set
            {
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
            if (this.Volume + gameObject.Volume > Tile.MaxVolume)
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

            this.flags = new Flags();
            bool result = this.objectsContained.Remove(gameObject);
            return result;
        }
    }
}