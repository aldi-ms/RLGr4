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

namespace RLG.Utilities
{
    using Microsoft.Xna.Framework;
    using RLG.Enumerations;

    internal static class CardinalDirectionExtensions
    {
        public static Point GetDeltaCoordinate(this CardinalDirection direction)
        {
            int dX = 0;
            int dY = 0;

            switch (direction)
            {
                case CardinalDirection.North:
                    dY = -1;
                    break;

                case CardinalDirection.South:
                    dY = 1;
                    break;

                case CardinalDirection.West:
                    dX = -1;
                    break;

                case CardinalDirection.East:
                    dX = 1;
                    break;

                case CardinalDirection.NorthWest:
                    dX = -1;
                    dY = -1;
                    break;

                case CardinalDirection.NorthEast:
                    dX = 1;
                    dY = -1;
                    break;

                case CardinalDirection.SouthEast:
                    dX = 1;
                    dY = 1;
                    break;

                case CardinalDirection.SouthWest:
                    dX = -1;
                    dY = 1;
                    break;
            }

            return new Point(dX, dY);
        }
    }
}
