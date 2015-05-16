//
//  TileObjectHelper.cs
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
    using RLG.Contracts;
    using RLG.Enumerations;
    using System.Collections.Generic;
    using System.Linq;
    
    public static class GameObjectExtensions
    {
        public static IActor GetActor(this IEnumerable<IGameObject> objects) 
        {
            return objects
                .Where(x => x as IActor != null)
                .FirstOrDefault()
                as IActor;
        }

        public static IEnumerable<IFringe> GetFringes(this IEnumerable<IGameObject> objects) 
        {
            return objects
                .Where(x => x as IFringe != null)
                .Cast<IFringe>();
        }

        public static IEnumerable<IItem> GetItems(this IEnumerable<IGameObject> objects)
        {
            return objects
                .Where(x => x as IItem != null)
                .Cast<IItem>();
        }

        public static ITerrain GetTerrain(this IEnumerable<IGameObject> objects)
        {
            return objects
                .Where(x => x as ITerrain != null)
                .FirstOrDefault()
                as ITerrain;
        }
    }
}
