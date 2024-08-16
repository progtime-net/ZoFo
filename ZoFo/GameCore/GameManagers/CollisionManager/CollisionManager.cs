using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.GameObjects;
using ZoFo.GameCore.GameManagers.CollisionManager;
using Microsoft.Xna.Framework;
using ZoFo.GameCore.GameManagers.MapManager.MapElements;

namespace ZoFo.GameCore.GameManagers.CollisionManager
{
    public class CollisionManager
    {
        public List<CollisionComponent> CollisionComponent;
        public List<CollisionComponent> TriggerComponent;

        

        public bool CheckComponentCollision(List<CollisionComponent> collisionComponents, CollisionComponent component)
        {
            foreach (var collisionComponent in collisionComponents)
            {
                if (component.Bounds.IntersectsWith(collisionComponent.Bounds))
                {
                    Register(component);
                    return true; 
                }
            }

            return false;
        }

        public void UpdateComponentCollision(List<CollisionComponent> collisionComponents)
        {

        }

        public void UpdatePositions()
        {

        }

        //public void GetObjectInArea(Rectangle area)
        //{

        //}

        public void Register(CollisionComponent component)
        {
            CollisionComponent.Add(component);
        }
        


    }
}
