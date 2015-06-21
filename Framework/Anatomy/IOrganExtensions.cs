//
//  IOrganExtensions.cs
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

namespace RLG.Framework.Anatomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class IOrganExtensions
    {
        public static IOrgan GetOrganByName(this IEnumerable<IOrgan> organs, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(
                    "name", 
                    "When looking up organ by name, string name cannot be null or empty!");
            }

            return organs.FirstOrDefault(x => x.Properties["name"] == name);
        }
    }
}

