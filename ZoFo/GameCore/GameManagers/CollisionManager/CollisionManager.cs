using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.GameObjects;
using ZoFo.GameCore.GameManagers.CollisionManager;
using Microsoft.Xna.Framework;
using ZoFo.GameCore.GameManagers.MapManager.MapElements;
using ZoFo.GameCore.GameObjects.Entities;
using ZoFo.GameCore.GameObjects.Entities.LivingEntities;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;
using ZoFo.GameCore.Graphics;

namespace ZoFo.GameCore.GameManagers.CollisionManager
{
    public class CollisionManager
    {
        //листики

        public List<CollisionComponent> ObjectsWithCollisions;
        public List<CollisionComponent> EntitiesWithMovements;
        public List<CollisionComponent> ObjectsWithTriggers;


        //чекаем коллизии в листе
        public void CheckComponentCollision(CollisionComponent componentOfEntity)
        {
            var entity = componentOfEntity.gameObject as LivingEntity;
            //for (int i = 0; i < ObjectsWithCollisions.Count; i++)
            //{
            var currentRect = entity.collisionComponent.stopRectangle;//задаём РЕК
            currentRect.X+=(int)entity.position.X;
            currentRect.Y+=(int)entity.position.Y;

            var newRect = currentRect; // задаём значение старого РЕК новому РЕК 


            var collidedX = false; // соприкосновение
            var tryingRectX = currentRect;//переменная для попытки перемещения по X

            tryingRectX.Offset((int)(entity.velocity.X), 0);//задаём значения для tryingRectX по X и по Y 

            foreach (var item in ObjectsWithCollisions)//фильтрация 
            {
                if (Math.Abs(item.stopRectangle.X - entity.collisionComponent.stopRectangle.X) < 550
                    && Math.Abs(item.stopRectangle.Y - entity.collisionComponent.stopRectangle.Y) < 550
                    && tryingRectX.Intersects(item.stopRectangle))

                {
                    collidedX = true;// меняем значение соприкосновения на true
                    entity.OnCollision(item);//подписываем entity на ивент коллизии

                    break;// выход
                }
            }

            if (collidedX)// срабатывает, если перемещение блокируется
            {
                entity.velocity.X = 0;// задаём значение смещения entity на 0
            }
            else
            {
                entity.position.X += entity.velocity.X; //update player position
                newRect.X = tryingRectX.X;//значение по X для нового РЕК приравниваем к значению испытуемого РЕК
            }

            //==ПОВТОРЯЕМ ТОЖЕ САМОЕ ДЛЯ Y==

            var collidedY = false; // соприкосновение
            var tryingRectY = currentRect;//переменная для попытки перемещения по X

            tryingRectY.Offset(new Point(0, (int)entity.velocity.Y));//задаём значения для tryingRectX по X и по Y 

            foreach (var item in ObjectsWithCollisions)//фильтрация 
            {
                if (Math.Abs(item.stopRectangle.X - entity.collisionComponent.stopRectangle.X) < 550
                    && Math.Abs(item.stopRectangle.Y - entity.collisionComponent.stopRectangle.Y) < 550
                    && tryingRectY.Intersects(item.stopRectangle))

                {
                    collidedY = true;// меняем значение соприкосновения на true
                    entity.OnCollision(item);//подписываем entity на ивент коллизии

                    break;// выход
                }
            }

            if (collidedY)// срабатывает, если перемещение блокируется
            {
                entity.velocity.Y = 0;// задаём значение смещения entity на 0
            }
            else
            {
                entity.position.X += entity.velocity.Y;
                newRect.Y = tryingRectY.Y;//значение по X для нового РЕК приравниваем к значению испытуемого РЕК
            }
             
            entity.graphicsComponent.ObjectDrawRectangle.X = (int)entity.position.X;
            entity.graphicsComponent.ObjectDrawRectangle.Y = (int)entity.position.Y;
            AppManager.Instance.server.AddData(new UpdatePosition() { NewPosition = entity.position, IdEntity = entity.Id });
            AppManager.Instance.debugHud.Set("testPos", entity.position.ToString()); //TODO remove
            entity.velocity = Vector2.Zero;
        }

        //обновление позиции объекта 

        public void UpdatePositions()
        {
            foreach (var item in EntitiesWithMovements)
            {
                CheckComponentCollision(item);
            }
        }


        public CollisionManager()
        {
            //graphicsComponent
            //.ObjectDrawRectangle = new Rectangle(0, 0, 16 * 12, 16 * 16);
            EntitiesWithMovements = new List<CollisionComponent>();
            ObjectsWithCollisions = new List<CollisionComponent>();
        }
        //регистрация компонента(его коллизии)
        public void Register(CollisionComponent component)
        {
            ObjectsWithCollisions.Add(component);
            if (component.gameObject is LivingEntity)
            {
                EntitiesWithMovements.Add(component);
            }
        }



    }
}
