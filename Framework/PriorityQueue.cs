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

    public abstract class PriorityQueue<T> : IEnumerable<T>
    {
        private List<T> queue;

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue" /> class.
        /// </summary>
        public PriorityQueue()
        {
            this.queue = new List<T>();
        }

        public List<T> Queue
        {
            get { return this.queue; }
            set { this.queue = value; }
        }

        public int Count
        {
            get { return this.queue.Count; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Add an element the the PriorityQueue and sorts the list.
        /// </summary>
        /// <param name="actor">The element to add.</param>
        public void Add(T element)
        {
            this.queue.Add(element);

            this.SortList();
        }

        /// <summary>
        /// Remove an element by reference and sort the list.
        /// </summary>
        /// <param name="actor">The element to remove.</param>
        /// <returns>True if the element is successfully removed; otherwise false.
        /// <remarks>This method also returns false if the element was not found in the PriorityQueue.</remarks></returns>
        public bool Remove(T element)
        {
            if (this.queue.Remove(element))
            {
                this.SortList();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sorts the list.
        /// </summary>
        public abstract void SortList();
    }
}
