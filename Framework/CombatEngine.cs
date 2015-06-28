//
//  CombatEngine.cs
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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using RLG.Contracts;

    public class CombatEngine
    {
        private const string IN = @"[A-z]\w+.?[A-z]\w+<|>[A-z]\w+.?[A-z]\w+";
        private const string OUT = @"[A-z]\w+.?[A-z]\w+=|=[A-z]\w+.?[A-z]\w+";
        //private Regex regexOut = new Regex(OUT);
        //private Regex regexIn = new Regex(IN);

        private IMap battlefield;

        public CombatEngine(IMap battlefield, byte scanSize = 0)
        {
            this.battlefield = battlefield;

        }

        public List<IActor> Units
        {
            get
            {
                List<IActor> result = new List<IActor>();
                foreach (ITile tile in this.battlefield.Tiles)
                {
                    result.AddRange(
                        tile.ObjectsContained
                        .Where(x => x is IActor)
                        .Cast<IActor>());
                }

                return result;
            }
        }

        // Example: "a1.att=>map[a2]";
        // "actor.action=>target" - "actor" (by name) does "action" to "
        // where "target" may be another actor, tile or game object targetted;
        // "actor.action" - "actor" (name) does "action" (which is not targeted
        // / has no target / it's target is explicit).
        // ('=' is output, '>' - target).
        public void TakeAction(IActor attacker, List<IActor> targets)
        {
        }
    }
}