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
using ZoFo.GameCore.GameObjects.Entities;
using ZoFo.GameCore.GameObjects.Entities.LivingEntities;

namespace ZoFo.GameCore.GameManagers.CollisionManager
{
    public class CollisionManager
    {
        //листики

        public List<CollisionComponent> ObjectsWithCollisions;
        public List<CollisionComponent> EntitiesWithMovements;
        public List<CollisionComponent> ObjectsWithTriggers;

        
        //чекаем коллизии в листе
        public void CheckComponentCollision(LivingEntity entity)
        {
            //for (int i = 0; i < ObjectsWithCollisions.Count; i++)
            //{
                var currentRect = entity.Rectangle;//задаём РЕК
                var newRect = currentRect; // задаём значение старого РЕК новому РЕК
                bool flagRemovedObject = false; //флаг удаления 


                var collidedX = false; // соприкосновение
                var tryingRectX = currentRect;//переменная для попытки перемещения по X

                tryingRectX.Offset((int)(entity.velocity.X), 0);//задаём значения для tryingRectX по X и по Y 

                foreach (var item in ObjectsWithCollisions)//фильтрация 
                {
                    if (Math.Abs(item.Pos.X - entity.Pos.X) < 550 
                        && Math.Abs(item.Pos.Y - entity.Pos.Y) < 550
                        && tryingRectX.Intersect(item.Rectangle))

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
                    newRect.X = tryingRectX.X;//значение по X для нового РЕК приравниваем к значению испытуемого РЕК
                }

                //==ПОВТОРЯЕМ ТОЖЕ САМОЕ ДЛЯ Y==

            var collidedY = false; // соприкосновение
            var tryingRectY = currentRect;//переменная для попытки перемещения по X

            tryingRectY.Offset((int)(0, entity.velocity.Y));//задаём значения для tryingRectX по X и по Y 

            foreach (var item in ObjectsWithCollisions)//фильтрация 
            {
                if (Math.Abs(item.Pos.X - entity.Pos.X) < 550
                    && Math.Abs(item.Pos.Y - entity.Pos.Y) < 550
                    && tryingRectY.Intersect(item.Rectangle))

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
                newRect.Y = tryingRectY.Y;//значение по X для нового РЕК приравниваем к значению испытуемого РЕК
            }
        }

        //обновление позиции объекта
        public void UpdateObjectsPositions()
        {

        }


        //регистрация компонента(его коллизии)
        public void Register(CollisionComponent component)
        {
            ObjectsWithCollisions.Add(component);
            if (component.gameObject is Entity)
            {
                EntitiesWithMovements.Add(component);
            }
        }
        


    }
}
