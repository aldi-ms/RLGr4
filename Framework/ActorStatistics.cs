//
//  ActorStatistics.cs
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

namespace RLG.Framework
{
    using System.Collections.Generic;

    public class ActorStatistics
    {
        private Dictionary<string, uint> statistics;

        public ActorStatistics()
        {
            this.statistics = new Dictionary<string, uint>();
        }

        public uint this [string index]
        {
            get
            {
                uint val;
                if (this.statistics.TryGetValue(index, out val))
                {
                    return val;
                }

                return 0;
            }

            set
            {
                if (this.statistics.ContainsKey(index))
                {
                    if (this.statistics[index] + value < 0)
                    {
                        this.statistics[index] = 0;
                    }
                    else 
                    {
                        this.statistics[index] += value;
                    }
                }
                else
                {
                    this.statistics.Add(index, value);
                }
            }
        }
    }
}

