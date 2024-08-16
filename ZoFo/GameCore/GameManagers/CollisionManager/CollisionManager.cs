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
        //листики

        public List<CollisionComponent> ObjectsWithCollisions;
        public List<CollisionComponent> EntitiesWithMovements;
        public List<CollisionComponent> ObjectsWithTriggers;

        
        //чекаем коллизии в листе
        public bool CheckComponentCollision(List<CollisionComponent> collisionComponents, CollisionComponent component)
        {
            foreach (var collision in collisionComponents)
            {
                if (component.Bounds.IntersectsWith(collision.Bounds))
                {
                    //Register(component, );
                    return true; 
                }
            }

            return false;
        }

        //обновление позиций
        public void UpdateObjectPosition(List<CollisionComponent> collisionComponents, CollisionComponent component)
        {
            
        }


        //получение объекта на поле(карте)
        //public void GetObjectInArea(Rectangle area)
        //{

        //}


        //регистрация компонента(его коллизии)
        public void Register(CollisionComponent component, GameObject gameObject)
        {
            if (component.gameObject is Entity)
            {
                ObjectsWithCollisions.Add(component);

            }
        }
        


    }
}
