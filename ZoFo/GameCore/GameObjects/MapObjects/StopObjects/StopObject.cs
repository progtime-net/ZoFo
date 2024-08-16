using Microsoft.Xna.Framework;
using System;
using ZoFo.GameCore.GameManagers.CollisionManager;
using ZoFo.GameCore.ZoFo_graphics;

namespace ZoFo.GameCore.GameObjects.MapObjects.StopObjects;

public class StopObject : MapObject
{
    CollisionComponent collisionComponent; 

    protected StopObject(Vector2 position, Vector2 size, Rectangle sourceRectangle, string textureName) : base(position, size, sourceRectangle, textureName)
    {
    }
}
