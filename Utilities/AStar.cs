//
//  AStar.cs
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
namespace RLG.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;

    using RLG.Contracts;
    using RLG.Entities;

    public static class AStar
    {
        public static List<Point> GetPathTo(this IActor actor, ITile goal)
        {
            List<ITile> closedSet = new List<ITile>();
            List<ITile> openSet = new List<ITile>();
            Dictionary<ITile, ITile> cameFrom = new Dictionary<ITile, ITile>();
            Dictionary<ITile, int> gScore = new Dictionary<ITile, int>();
            Dictionary<ITile, int> fScore = new Dictionary<ITile, int>();

            ITile start = actor.CurrentMap[actor.Position];
            openSet.Add(start);
            cameFrom[start] = null;
            gScore[start] = 0;
            fScore[start] = gScore[start] + HeuristicCostEstimate(start, goal);

            while (openSet.Count > 0)
            {
                ITile current = GetLowestFScoreTile(fScore);

                if (current == goal)
                {
                    return ReconstructPath(cameFrom, start, goal);
                }

                openSet.Remove(current);
                closedSet.Add(current);

                if (current.Neighboors == null)
                {
                    return null;
                }

                foreach (var neighboor in current.Neighboors)
                {
                    if (closedSet.Contains(neighboor))
                    {
                        continue;
                    }

                    int tentativeGScore = gScore[current] + neighboor.ObjectsContained.GetTerrain().Volume;

                    if (!openSet.Contains(neighboor) || tentativeGScore < gScore[neighboor])
                    {
                        cameFrom[neighboor] = current;
                        gScore[neighboor] = tentativeGScore;
                        fScore[neighboor] = gScore[neighboor] + HeuristicCostEstimate(neighboor, goal);

                        if (!openSet.Contains(neighboor))
                        {
                            openSet.Add(neighboor);
                        }                       
                    }
                }
            }

            return null;    
        }

        private static int HeuristicCostEstimate(ITile current, ITile goal)
        {
            // Diagonal distance (a.k.a. Chebyshev distance)
            int dx = Math.Abs(current.Position.X - goal.Position.X);
            int dy = Math.Abs(current.Position.Y - goal.Position.Y);
            return Math.Max(dx, dy);
        }

        private static List<Point> ReconstructPath(Dictionary<ITile, ITile> cameFrom, ITile start, ITile goal)
        {
            List<ITile> path = new List<ITile>();
            ITile current = goal;
            path.Add(current);
            while (current != start)
            {
                current = cameFrom[current];
                path.Add(current);
            }

            return path.Select(x => x.Position).Reverse().ToList();
        }

        private static ITile GetLowestFScoreTile(Dictionary<ITile, int> f)
        {
            int min = int.MaxValue;
            ITile minFTile = null;

            foreach (var score in f.Keys)
            {
                if (f[score] < min)
                {
                    min = f[score];
                    minFTile = score;
                }                
            }

            f.Remove(minFTile);
            return minFTile;    
        }
    }
}