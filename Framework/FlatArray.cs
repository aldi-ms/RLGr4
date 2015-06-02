/* *
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
    /// Wrapper that flattens a 2-dimensional array to increase performance.
    /// </summary>
    /// <typeparam name="T">Array elements type.</typeparam>
    public class FlatArray<T> : IEnumerable<T>
    {
        /// <summary>
        /// Data kept by our array.
        /// </summary>
        private T[] data;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlatArray"/> class
        /// of <typeparamref name="T"/> elements.
        /// <para>A wrapper that flattens a 2-dimensional array.</para>
        /// </summary>
        /// <param name="width">Width of the array.</param>
        /// <param name="height">Height of the array.</param>
        public FlatArray(int width, int height)
        {
            this.Width = width;
            this.Height = height;

            this.data = new T[width * height];
        }
                
        /// <summary>
        /// Gets the width of the array - the "X" dimension or horizontal axis.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the height of the array - the "Y" dimension vertical axis.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Access the elements like you would a 2-Dimensional array.
        /// </summary>
        /// <param name="x">Horizontal axis / column.</param>
        /// <param name="y">Vertical axis / row.</param>
        /// <returns>The <typeparamref name="T"/> element at given position.</returns>
        public T this[int x, int y]
        {
            get { return this.data[x + (y * this.Width)]; }

            set { this.data[x + (y * this.Width)] = value; }
        }

        /// <summary>
        /// Get the index of the element,
        /// in the inner one-dimensional array.
        /// </summary>
        /// <param name="x">"X" dimension of the element.</param>
        /// <param name="y">"Y" dimension of the element.</param>
        /// <returns>Index of the element in the one-dimensional array.</returns>
        public int GetRealIndex(int x, int y)
        {
            return (y * this.Width) + x;
        }

        /// <summary>
        /// Gets the enumerator. Implements IEnumerable<T>.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator. Implements IEnumerable<T>.
        /// </summary>
        /// <returns>The enumerator.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.data.GetEnumerator();
        }
    }
}