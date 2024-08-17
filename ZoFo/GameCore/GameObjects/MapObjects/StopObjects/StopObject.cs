using Microsoft.Xna.Framework;
using System;
using ZoFo.GameCore.GameManagers.CollisionManager;
using ZoFo.GameCore.ZoFo_graphics;

namespace ZoFo.GameCore.GameObjects.MapObjects.StopObjects;

public class StopObject : MapObject
{
    CollisionComponent collisionComponent; 

    public StopObject(Vector2 position, Vector2 size, Rectangle sourceRectangle, string textureName) : base(position, size, sourceRectangle, textureName)
    {
        // TODO: Написать коллизию, пусть тразмер будет чисто таким же как и текстурка.
              // Поменяйте уровень защиты конструктора, после снимите в MapManager комментарий в методе LoadMap с создания StopObject-а
    }
}
