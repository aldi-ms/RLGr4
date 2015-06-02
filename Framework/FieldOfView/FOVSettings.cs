//
//  FOVSettings.cs
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

namespace RLG.Framework.FieldOfView
{
    using System;

    public class FOVSettings
    {
        private int maxRange;

        public FOVSettings(
            int maxRange = 4, 
            bool lightWalls = true,
            FOVMethod method = FOVMethod.MRPAS,
            RangeLimitShape shape = RangeLimitShape.Circle)
        {
            this.MaxRange = maxRange;
            this.LightWalls = lightWalls;
            this.Method = method;
            this.Shape = shape;
        }

        #region Properties

        public int MaxRange
        {
            get
            { 
                return this.maxRange;
            }

            set
            {
                if (value < 0 || value > 50)
                {
                    throw new ArgumentException(
                        "FOV maxRange should be a number between 0 and 50 inclusive!",
                        "maxRange");
                }

                this.maxRange = value;
            }
        }

        public bool LightWalls { get; set; }

        public FOVMethod Method { get; set; }

        public RangeLimitShape Shape { get; set; }

        #endregion
    }
}