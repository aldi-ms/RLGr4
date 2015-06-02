//
//  GameObject.cs
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
    using RLG.Contracts;
    using RLG.Enumerations;

    public abstract class GameObject : IGameObject
    {
        private string name;
        private string drawString;
        private byte volume;

        public GameObject(string name, string drawString, byte volume)
        {
            this.Name = name;
            this.DrawString = drawString;
            this.Volume = volume;
        }

        public GameObject(string name, string drawString, byte volume, Flags flags)
            : this(name, drawString, volume)
        {
            this.Flags = flags;
        }

        #region Properties

        public virtual string Name
        {
            get
            {
                return this.Name;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(
                        "name", 
                        "GameObject name cannot be null or empty string!");
                }

                this.name = value;
            }
        }

        public virtual byte Volume
        {
            get
            {
                return this.volume;
            }

            set
            {
                if (value > 125)
                {
                    throw new ArgumentException("Actor.Volume cannot be > 125!");
                }

                this.volume = value;
            }
        }

        public virtual string DrawString
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
                        "GameObject drawString cannot be null!");
                }

                this.drawString = value;
            }
        }

        public virtual Flags Flags { get; set; }

        #endregion
    }
}