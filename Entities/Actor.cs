//
//  Actor.cs
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

namespace RLG.Entities
{
    using RLG.Contracts;
    
    public class Actor : IActor
    {
        public Actor()
        {
        }

        #region IActor implementation

        int IActor.Move(RLG.Enumerations.CardinalDirection direction)
        {
            throw new System.NotImplementedException();
        }

        IMap IActor.Map
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        IStatistics IActor.Statistics
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        #endregion

        #region IGameObject implementation

        RLG.Enumerations.Flags IGameObject.PropertyFlags
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        #endregion

        #region IDrawable implementation

        string IDrawable.DrawString
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        #endregion

        #region ITileContainable implementation

        int ITileContainable.Volume
        {
            get
            {
                return 100;
            }
        }

        #endregion
    }
}

