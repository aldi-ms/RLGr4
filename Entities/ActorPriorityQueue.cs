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

namespace RLG.Entities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using RLG.Contracts;
    using RLG.Entities;
    using RLG.Framework;

    public class ActorPriorityQueue : PriorityQueue<IActor>
    {
        public IActor this [int index]
        {
            get { return base.Queue[index]; }
        }

        /// <summary>
        /// Sums all Actors' energy with their speed.
        /// </summary>
        /// <remarks>Call each turn to make Actors accumulate energy.</remarks>
        public void AccumulateEnergy()
        {
            foreach (IActor actor in base.Queue)
            {
                actor.Properties["energy"] += actor.Properties["speed"];
            }

            SortList();
        }

        /// <summary>
        /// Sorts the list in descending order by Actor.Energy.
        /// </summary>
        public override void SortList()
        {
            base.Queue.Sort(
                (x, y) => y.Properties["energy"].CompareTo(x.Properties["energy"])
            );
        }
    }
}
