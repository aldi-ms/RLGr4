//
//  TileExtensions.cs
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
    using RLG.Contracts;
    using RLG.Entities;
    
    public static class TileExtensions
    {
        public static bool Try(this ITile tile, IGameObject gameObject)
        {
            return (tile.Volume + gameObject.Volume <= Tile.MaxVolume);
        }
    }
}

