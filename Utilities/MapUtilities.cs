﻿//
//  MapUtilities.cs
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

namespace RLG.Utilities
{
    using Microsoft.Xna.Framework;
    using RLG.Contracts;
    using RLG.Enumerations;
    using RLG.Entities;
    using RLG.Framework;

    public static class MapUtilities
    {
        private static System.Random RNG = new System.Random();

        public static FlatArray<ITile> GenerateRandomMap(Point size, VisualMode mode)
        {
            FlatArray<ITile> resultTiles = new FlatArray<ITile>(size.X, size.Y);

            switch (mode)
            {
                case VisualMode.ASCII:
                    // Testing foreach on FlatArray<T>
                    for (int i = 0; i < size.X; i++)
                    {
                        for (int j = 0; j < size.Y; j++)
                        {
                            int val = RNG.Next(0, 100);
                            if (val > 80)
                            {
                                // blocked tile
                                Flags flags = Flags.IsBlocked;
                                resultTiles[i, j] = new Tile("#", flags);
                            }
                            else
                            {
                                Flags flags = Flags.IsTransparent;
                                resultTiles[i, j] = new Tile(".", flags);
                            }
                        }
                    }
                    break;

                case VisualMode.Sprites:                    
                    break;
            }

            return resultTiles;
        }
    }
}
