﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.GameObjects;
using ZoFo.GameCore.GameManagers.CollisionManager;

namespace ZoFo.GameCore.GameManagers.CollisionManager
{
    public class CollisionManager
    {
        Player player;

        public List<CollisionComponent> CollisionComponent;
        public List<CollisionComponent> TriggerComponent;

        public static bool CheckComponentCollision(List<CollisionComponent> collisionComponents, CollisionComponent component)
        {
            foreach (var collisionComponent in collisionComponents)
            {
                if (component.Bounds.IntersectsWith(collisionComponent.Bounds))
                {
                    return true; 
                }
            }

            return false;
        }

        public void UpdatePositions()
        {

        }

        public void GetObjectInArea(Rectangle area)
        {

        }

        public void Register(Rectangle rectangle)
        {

        }


    }
}
