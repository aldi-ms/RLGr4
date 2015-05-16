﻿﻿/* *
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
    using System.Collections.Generic;

    /// <summary>
    /// Implementation of a FIFO (First In First Out) generic collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Buffer<T>
    {
        private Queue<T> buffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Buffer"/> class.
        /// </summary>
        public Buffer()
        {
            this.buffer = new Queue<T>();
        }   

        /// <summary>
        /// Gets the count of elements in the buffer.
        /// </summary>
        public int Count
        {
            get { return this.buffer.Count; }
        }

        /// <summary>
        /// Gets the first (top) element, and removes it from the buffer.
        /// </summary>
        /// <returns>The first <see cref="T"/> element in the buffer or the 
        /// default (null/0/none) type value if such is not present.</returns>
        public T Dequeue()
        {
            if (this.buffer.Count > 0)
            {
                return this.buffer.Dequeue();
            }

            return default(T);
        }

        /// <summary>
        /// Enqueue an element to the buffer manually.
        /// </summary>
        /// <param name="element">Element to push.</param>
        public void Enqueue(T element)
        {
            this.buffer.Enqueue(element);
        }

        /// <summary>
        /// Removes all elements contained in the buffer.
        /// </summary>
        public void ClearBuffer()
        {
            this.buffer.Clear();
        }

        /// <summary>
        /// Parse all elements contained in the buffer to a String object, each separated with a space.
        /// </summary>
        /// <returns>The string values of all element in the buffer to a String.</returns>
        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (T element in this.buffer)
            {
                sb.Append(element.ToString());
            }

            return sb.ToString();
        }
    }
}