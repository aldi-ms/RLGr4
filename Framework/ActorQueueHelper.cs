//
//  GameEngine.cs
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
using System;

namespace RLG.Framework
{
    using Microsoft.Xna.Framework;

    using RLG.Contracts;
    using RLG.Entities;
    using RLG.Enumerations;
    using RLG.Utilities;

    public class ActorQueueHelper
    {
        public ActorQueueHelper(IMap map)
        {
            this.CurrentMap = map;
            this.ActorQueue = new ActorPriorityQueue();
        }

        #region Properties

        public IMap CurrentMap { get; set; }

        public ActorPriorityQueue ActorQueue { get; set; }

        #endregion

        public bool AddActor(IActor actor)
        {
            if (actor == null)
            {
                throw new ArgumentNullException(
                "actor",
                "Actor cannot be null on AddActor(actor).");
            }

            if (this.CurrentMap[actor.Position].AddObject(actor))
            {
                this.ActorQueue.Add(actor);
                return true;
            }

            return false;
        }

        public bool RemoveActor(IActor actor)
        {
            if (actor == null)
            {
                throw new ArgumentNullException(
                    "actor",
                    "Actor cannot be null on AddActor(actor).");
            }

            return this.ActorQueue.Remove(actor);
        }
    }
}

