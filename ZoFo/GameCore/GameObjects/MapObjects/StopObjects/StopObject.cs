using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using ZoFo.GameCore.GameManagers.CollisionManager;

namespace ZoFo.GameCore.GameObjects.MapObjects.StopObjects;

public class StopObject : MapObject
{
    CollisionComponent[] collisionComponent;


    public StopObject(Vector2 position, Vector2 size, Rectangle sourceRectangle, string textureName, Rectangle[] collisions) : base(position, size, sourceRectangle, textureName)
    {
        collisionComponent = new CollisionComponent[collisions.Length];
        for (int i = 0; i < collisionComponent.Length; i++)
        {
            collisionComponent[i] = new CollisionComponent(this, true, collisions[i]);
        }
        // TODO: Написать коллизию, пусть тразмер будет чисто таким же как и текстурка.
              // Поменяйте уровень защиты конструктора, после снимите в MapManager комментарий в методе LoadMap с создания StopObject-а
    }
}
