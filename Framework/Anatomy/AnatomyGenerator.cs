﻿//
//  AnatomyGenerator.cs
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

namespace RLG.Framework.Anatomy
{
    using System;
    using System.Collections.Generic;
    using RLG.Enumerations;
    using RLG.Framework.Anatomy;

    public static class AnatomyGenerator
    {
        public static IAnatomy GenerateHumanAnatomy()
        {
            ActorAnatomy humanAnatomy = new ActorAnatomy(Species.Human);

            #region Create Organs

            IOrgan head = new Organ("head", 10);
            IOrgan hair = new Organ("hair", 1);
            IOrgan leftEar = new Organ("left-ear", 2);
            IOrgan rightEar = new Organ("right-ear", 2);
            IOrgan nose = new Organ("nose", 2);
            IOrgan mouth = new Organ("mouth", 3);  //20

            IOrgan leftArm = new Organ("left-arm", 6);
            IOrgan rightArm = new Organ("right-arm", 6);
            IOrgan leftHand = new Organ("left-hand", 4);
            IOrgan rightHand = new Organ("right-hand", 4); //20

            IOrgan torso = new Organ("torso", 10); //10

            IOrgan leftLeg = new Organ("left-leg", 6);
            IOrgan rightLeg = new Organ("right-leg", 6);
            IOrgan leftFoot = new Organ("left-foot", 4);
            IOrgan rightFoot = new Organ("right-foot", 4);  //20

            #endregion

            #region Make Organ child-connections

            torso.ChildOrgans = new List<IOrgan>()
            { 
                head, leftHand, rightHand, leftLeg, rightLeg 
            };

            head.ChildOrgans = new List<IOrgan>()
            {
                hair, leftEar, rightEar, nose, mouth
            };

            leftArm.ChildOrgans = new List<IOrgan>() { leftHand };

            rightArm.ChildOrgans = new List<IOrgan>() { rightHand };

            leftLeg.ChildOrgans = new List<IOrgan>() { leftFoot };

            rightLeg.ChildOrgans = new List<IOrgan>() { rightFoot };

            #endregion

            #region Add Parent Organs

            foreach (var parent in humanAnatomy.Anatomy)
            {
                if (parent.ChildOrgans == null)
                {
                    parent.ChildOrgans = new List<IOrgan>();
                }
                if (parent.ParentOrgans == null)
                {
                    parent.ParentOrgans = new List<IOrgan>();
                }

                foreach (var child in parent.ChildOrgans)
                {
                    if (child.ChildOrgans == null)
                    {
                        child.ChildOrgans = new List<IOrgan>();
                    }
                    if (child.ParentOrgans == null)
                    {
                        child.ParentOrgans = new List<IOrgan>();
                    }

                    child.ParentOrgans.Add(parent);
                }
            }

            #endregion

            return humanAnatomy;
        }
    }
}