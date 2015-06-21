//
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
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    using RLG.Contracts;
    using RLG.Entities;
    using RLG.Enumerations;
    using RLG.Framework;

    public static class MapUtilities
    {
        private static readonly Point[] Directions = new Point[]
        {
            new Point(-1, -1),
            new Point(-1, 1),
            new Point(-1, 0),
            new Point(0, -1),
            new Point(0, 1),
            new Point(1, -1),
            new Point(1, 0),
            new Point(1, 1)
        };
        
        private static Random rng = new Random();

        public static void LoadTileNeighboors(this IMap map)
        {
            for (int x = 0; x < map.Tiles.Width; x++)
            {
                for (int y = 0; y < map.Tiles.Height; y++)
                {
                    Point currentPosition = new Point(x, y);

                    string str;
                    if (CheckTile(map, currentPosition, out str))
                    {
                        List<ITile> neighboors = new List<ITile>();
                        foreach (var dir in Directions)
                        {
                            Point newPosition = currentPosition + dir;

                            if (CheckTile(map, newPosition, out str))
                            {
                                neighboors.Add(map[newPosition]);
                            }
                        }

                        map[currentPosition].Neighboors = neighboors ?? new List<ITile>();
                    }
                }
            }
        }

        public static FlatArray<ITile> GenerateRandomMap(int x, int y, VisualMode mode)
        {
            FlatArray<ITile> resultTiles = new FlatArray<ITile>(x, y);

            switch (mode)
            {
                case VisualMode.ASCII:
                    {
                        for (int i = 0; i < x; i++)
                        {
                            for (int j = 0; j < y; j++)
                            {
                                int val = rng.Next(0, 100);
                                if (val > 70)
                                {
                                    Terrain wall = new Terrain("wall", "#", Tile.MaxVolume);
                                    resultTiles[i, j] = new Tile(new Point(i, j), wall);
                                }
                                else
                                {
                                    Terrain grass = new Terrain("grass", ",", 5, Flags.IsTransparent);
                                    resultTiles[i, j] = new Tile(new Point(i, j), grass);
                                }
                            }
                        }

                        break;
                    }

                case VisualMode.Sprites:                    
                    break;
            }

            return resultTiles;
        }

        private static bool CheckTile(IMap map, Point position, out string blocking)
        {
            IActor pupet = new Actor("а", "/", new PropertyBag<int>(), map, new Flags(), 0);

            return pupet.CheckTile(position, out blocking);
        }
    }
}