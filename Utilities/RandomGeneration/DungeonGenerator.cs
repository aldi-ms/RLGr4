//
//  DungeonGenerator.cs
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
namespace RLG.Utilities.RandomGeneration
{
    using System;
    using Microsoft.Xna.Framework;
    using RLG.Contracts;
    using RLG.Entities;
    using RLG.Enumerations;
    using RLG.Framework;
    using RLG.Utilities;

    public class DungeonGenerator
    {
        private FlatArray<ITile> generatedMapTiles;

        public DungeonGenerator()
        {
        }

        public IMap GenerateDungeonMap(int width, int height)
        {
            this.generatedMapTiles = new FlatArray<ITile>(width, height);

            // Fill the whole map solid
            ITerrain rock = new Terrain("rock", "[]", 125);
            this.FillMap(rock);
            
            // . . .

            return new MapContainer(this.generatedMapTiles);
        }

        private bool CreateRoom(int x, int y, int width, int height, ITerrain terrain)
        {
            this.FillRect(x, y, width, height, terrain);

            return false;
        }

        private void GenerateWalls()
        {
            foreach (ITile tile in this.generatedMapTiles)
            {
                if (tile.Volume <= 25)
                {
                    foreach (ITile neighboorTile in tile.Neighboors)
                    {
                        if (neighboorTile.Volume > 100)
                        {
                            ITerrain wall = new Terrain("wall", "#", 125);
                            ITerrain currentTerrain = neighboorTile.ObjectsContained.GetTerrain();
                            neighboorTile.RemoveObject(currentTerrain);
                            neighboorTile.AddObject(wall);
                        }
                    }
                }
            }
        }

        private bool CheckRect(int x, int y, int width, int height)
        {
            for (int row = y; row < y + height; row++)
            {
                for (int col = x; col < x + width; col++)
                {
                    if (this.generatedMapTiles[row, col].Volume < 125)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void FillRect(int x, int y, int width, int height, ITerrain terrain)
        {
            
            for (int row = y; row < y + height; row++)
            {
                for (int col = x; col < x + width; col++)
                {
                    ITerrain tileTerrain = new Terrain(terrain.Name, terrain.DrawString, terrain.Volume, terrain.Flags);

                    this.generatedMapTiles[row, col] = new Tile(new Point(row, col), tileTerrain);
                }
            }
        }

        public void FillMap(ITerrain terrain)
        {
            this.FillRect(0, 0, this.generatedMapTiles.Width, this.generatedMapTiles.Height, terrain);
        }
    }
}