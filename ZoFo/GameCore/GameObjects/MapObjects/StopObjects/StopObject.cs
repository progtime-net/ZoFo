using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using ZoFo.GameCore.GameManagers.CollisionManager;

namespace ZoFo.GameCore.GameObjects.MapObjects.StopObjects;

public class StopObject : MapObject
{
    public CollisionComponent[] collisionComponents;


    public StopObject(Vector2 position, Vector2 size, Rectangle sourceRectangle, string textureName, Rectangle[] collisions) : base(position, size, sourceRectangle, textureName)
    {
        
        collisionComponents = new CollisionComponent[collisions.Length];
        for (int i = 0; i < collisionComponents.Length; i++)
        {
            collisionComponents[i] = new CollisionComponent(this, true, new Rectangle(0,0, (int)size.X, (int)size.Y)/*collisions[i]*/);
        }
        //REDO
        // TODO: Написать коллизию, пусть тразмер будет чисто таким же как и текстурка.
              // Поменяйте уровень защиты конструктора, после снимите в MapManager комментарий в методе LoadMap с создания StopObject-а
    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
            DrawDebugRectangle(spriteBatch, new Rectangle((int)position.X, (int)position.Y, collisionComponents[0].stopRectangle.Width, collisionComponents[0].stopRectangle.Height));
    }
}
