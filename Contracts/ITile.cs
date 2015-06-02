//
//  ITile.cs
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

namespace RLG.Contracts
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using RLG.Enumerations;
    using RLG.Framework.FieldOfView;

    public interface ITile : IFovCell, IDrawable
    {
        IEnumerable<IGameObject> ObjectsContained { get; }

        IEnumerable<ITile> Neighboors { get; set; }

        Point Position { get; }

        Flags Flags { get; set; }

        byte Volume { get; }

        bool AddObject(IGameObject gameObject);

        bool RemoveObject(IGameObject gameObject);
    }
}