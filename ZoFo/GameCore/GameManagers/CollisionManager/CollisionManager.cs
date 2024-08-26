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
using ZoFo.GameCore.GameManagers.NetworkManager.SerializableDTO; 
using ZoFo.GameCore.GameObjects.Entities.LivingEntities.Player; 

namespace ZoFo.GameCore.GameManagers.CollisionManager
{
    public class CollisionManager
    {
        //листики

        public List<CollisionComponent> ObjectsWithCollisions;
        public List<CollisionComponent> EntitiesWithMovements;
        public List<CollisionComponent> ObjectsWithTriggers;
 
        public List<CollisionComponent> GetEntitiesToUpdate(Player player)
        {
            float ViewDistance = 500;

            List<CollisionComponent> EntitiesInPlayerArea = new List<CollisionComponent>();

            Rectangle ViewArea = new Rectangle((int)(player.position.X), (int)(player.position.Y), 
                (int)(ViewDistance), (int)(ViewDistance));

            for (int i = 0; i < ObjectsWithCollisions.Count; i++)
            {
                if (ViewArea.Contains((float)ObjectsWithCollisions[i].gameObject.position.X, (float)ObjectsWithCollisions[i].gameObject.position.Y));
                {
                    EntitiesInPlayerArea.Add(ObjectsWithCollisions[i]);
                }
            }
            return EntitiesInPlayerArea;
        }

 
        //чекаем коллизии в листе
        
        /// <summary>
        /// минимальный накоп изменения перед перевдижением
        /// </summary>
        const float minimalValueToChange = 0;
        public void CheckComponentCollision(CollisionComponent componentOfEntity)
        {
            //ADD CHECKSPEED  TODO
            var entity = componentOfEntity.gameObject as LivingEntity;
            //for (int i = 0; i < ObjectsWithCollisions.Count; i++)
            //{
            var currentRect = entity.collisionComponent.stopRectangle;//задаём РЕК
            currentRect.X += (int)entity.position.X;
            currentRect.Y += (int)entity.position.Y;

            var newRect = currentRect; // задаём значение старого РЕК новому РЕК 


            if (Math.Abs((int)entity.velocity.X) > minimalValueToChange ) //TODO
            {
                var collidedX = false; // соприкосновение
                var tryingRectX = currentRect;//переменная для попытки перемещения по X

                tryingRectX.Offset((int)(entity.velocity.X), 0);//задаём значения для tryingRectX по X и по Y 

                foreach (var item in ObjectsWithCollisions)//фильтрация 
                {
                    if (item == componentOfEntity) continue;

                    Rectangle rectChecking = item.stopRectangle.SetOrigin(item.gameObject.position);
                    if (Math.Abs(item.gameObject.position.X - componentOfEntity.gameObject.position.X) < 550
                        && Math.Abs(item.gameObject.position.Y - componentOfEntity.gameObject.position.Y) < 550
                        && tryingRectX.Intersects(rectChecking))

                    {
                        collidedX = true;// меняем значение соприкосновения на true
                                         //entity.OnCollision(item);//подписываем entity на ивент коллизии
                        item.OnCollisionWithObject(entity);
                        entity.collisionComponent.OnCollisionWithObject(item.gameObject);
                        break;// выход
                    }
                }

                if (collidedX)// срабатывает, если перемещение блокируется
                {
                    entity.velocity.X = 0;// задаём значение смещения entity на 0
                }
                else
                {
                    entity.position.X += (int)entity.velocity.X; //update player position
                    //entity.position.X = (float)Math.Round(entity.position.X, 2);
                    newRect.X = tryingRectX.X;//значение по X для нового РЕК приравниваем к значению испытуемого РЕК
                }

                entity.velocity.X = 0;
            }
            //==ПОВТОРЯЕМ ТОЖЕ САМОЕ ДЛЯ Y==

            var collidedY = false; // соприкосновение
            var tryingRectY = currentRect;//переменная для попытки перемещения по X

            if (Math.Abs((int)entity.velocity.Y)> minimalValueToChange) //TODO
            {

                tryingRectY.Offset(new Point(0, (int)entity.velocity.Y));//задаём значения для tryingRectX по X и по Y 

                foreach (var item in ObjectsWithCollisions)//фильтрация 
                {
                    if (item == componentOfEntity) continue;
                    Rectangle rectChecking = item.stopRectangle.SetOrigin(item.gameObject.position);
                    if (Math.Abs(item.gameObject.position.X - componentOfEntity.gameObject.position.X) < 550
                        && Math.Abs(item.gameObject.position.Y - componentOfEntity.gameObject.position.Y) < 550
                        && tryingRectY.Intersects(rectChecking))

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
                    entity.position.Y += (int)entity.velocity.Y;
                    //entity.position.Y = (float)Math.Round(entity.position.Y, 2);
                    newRect.Y = tryingRectY.Y;//значение по X для нового РЕК приравниваем к значению испытуемого РЕК
                }
                entity.velocity.Y = 0;
            }


            entity.graphicsComponent.ObjectDrawRectangle.X = (int)entity.position.X;
            entity.graphicsComponent.ObjectDrawRectangle.Y = (int)entity.position.Y;
            AppManager.Instance.server.AddData(new UpdatePosition() { NewPosition = new SerializableVector2(entity.position), IdEntity = entity.Id });
            AppManager.Instance.debugHud.Set("testPos", entity.position.ToString()); //TODO remove
        }

        public void UpdateTriggerZones(Player player)
        {

            var entity = player as LivingEntity;
            var currentRect = entity.collisionComponent.stopRectangle;//задаём РЕК
            currentRect.X += (int)entity.position.X;
            currentRect.Y += (int)entity.position.Y;


            for (int i = 0; i < ObjectsWithTriggers.Count; i++)
            {
                int c = ObjectsWithTriggers.Count;

                if (ObjectsWithTriggers[i].triggerRectangle.SetOrigin(ObjectsWithTriggers[i].gameObject.position).Intersects(currentRect))
                {
                    ObjectsWithTriggers[i].PlayerInZone(player);
                }
                i -= c - ObjectsWithTriggers.Count;
            }
        }

        //обновление позиции объекта 

        public void ResolvePhysics()
        {
            foreach (var item in EntitiesWithMovements)
            {
                CheckComponentCollision(item);
            }
            foreach (var item in AppManager.Instance.server.players)
            {
                UpdateTriggerZones(item);
            }
        }


        public CollisionManager()
        {
            EntitiesWithMovements = new List<CollisionComponent>();
            ObjectsWithCollisions = new List<CollisionComponent>();
            ObjectsWithTriggers = new List<CollisionComponent>();
        }
        //регистрация компонента(его коллизии)
        public void Register(CollisionComponent component)
        {
            if (component.hasCollision)
                ObjectsWithCollisions.Add(component);
            if (component.isTrigger)
                ObjectsWithTriggers.Add(component);
            if (component.gameObject is LivingEntity)
            {
                EntitiesWithMovements.Add(component);
            }
        }
        public void Deregister(CollisionComponent component)
        {
            if (ObjectsWithCollisions.Contains(component))
                ObjectsWithCollisions.Remove(component);
            if (ObjectsWithTriggers.Contains(component))
                ObjectsWithTriggers.Remove(component);
            if (component.gameObject is LivingEntity)
            {
                if (EntitiesWithMovements.Contains(component))
                    EntitiesWithMovements.Remove(component);
            }
        }

        public Player[] GetPlayersInZone(Rectangle rectangle)
        {

            List<Player> players = new List<Player>();
            foreach (var item in AppManager.Instance.server.players)//фильтрация 
            {
                if (item.collisionComponent.stopRectangle.SetOrigin(item.position).Intersects(rectangle))
                {
                    players.Add(item);
                }
            }
            return players.ToArray();
        }
        public Entity[] GetEntities(Rectangle rectangle, Entity entityToIgnore = null)
        {

            List<Entity> entities = new List<Entity>();
            foreach (var item in AppManager.Instance.server.entities)//фильтрация 
            {
                if (item.collisionComponent.stopRectangle.SetOrigin(item.position).Intersects(rectangle))
                {
                    entities.Add(item);
                }
            }
            if (entityToIgnore!= null)
            {
                if (entities.Contains(entityToIgnore))
                    entities.Remove(entityToIgnore);
            }
            return entities.ToArray();
        }

    }
    public static class ExtentionClass
    {
        public static Rectangle SetOrigin(this Rectangle rectangle, Vector2 origin)
        {
            rectangle.X += (int)origin.X;
            rectangle.Y += (int)origin.Y;
            return rectangle;
        }
        public static SerializableVector2 Serialize(this Vector2 vector) => new SerializableVector2(vector);
        public static Vector2 RandomVector()
        {  
            return new Vector2((float)Random.Shared.NextDouble() - 0.5f, (float)Random.Shared.NextDouble() - 0.5f)*2;
        }
        
    }

}
