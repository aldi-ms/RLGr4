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

namespace RLG.Framework
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using RLG.Contracts;
    using RLG.Entities;

    public class PriorityQueue<T> : IEnumerable<T>
             where T : IActor
    {
        private List<T> actorList;

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue" /> class.
        /// </summary>
        public PriorityQueue()
        {
            this.actorList = new List<T>();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.actorList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Add an Actor the the PriorityQueue.
        /// </summary>
        /// <param name="actor">The Actor to add.</param>
        public void Add(T actor)
        {
            this.actorList.Add(actor);

            SortList();
        }

        /// <summary>
        /// Remove an Actor by reference.
        /// </summary>
        /// <param name="actor">The Actor to remove.</param>
        /// <returns>True if the Actor is successfully removed; otherwise false.
        /// <remarks>This method also returns false if the Actor was not found in the PriorityQueue.</remarks></returns>
        internal bool Remove(T actor)
        {
            if (this.actorList.Remove(actor))
            {
                SortList();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sums all Actors' energy with their speed.
        /// </summary>
        /// <remarks>Call each turn to make Actors accumulate energy.</remarks>
        internal void AccumulateEnergy()
        {
            foreach (T actor in actorList)
            {
                actor.Properties["energy"] += actor.Properties["speed"];
            }

            SortList();
        }

        /// <summary>
        /// Sorts the list in descending order by Actor.Energy.
        /// </summary>
        private void SortList()
        {
            this.actorList.Sort(
                (x, y) => y.Properties["energy"].CompareTo(x.Properties["energy"])
            );
        }
    }
}
