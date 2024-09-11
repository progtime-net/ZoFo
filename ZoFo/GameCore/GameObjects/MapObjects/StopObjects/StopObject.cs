using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ZoFo.GameCore.GameManagers.CollisionManager;

namespace ZoFo.GameCore.GameObjects.MapObjects.StopObjects;

public class StopObject : MapObject
{
    public CollisionComponent[] collisionComponents;


    public StopObject(Vector2 position, Vector2 size, Rectangle sourceRectangle, string textureName, Rectangle[] collisions) : base(position, size, sourceRectangle, textureName)
    {
        var cols = collisions.ToList();
        for (int i = 0; i < cols.Count; i++)
        {
            if (cols[i].Width<1)
            {
                cols.RemoveAt(i);
                i--;
            }
        } 
        collisionComponents = new CollisionComponent[cols.Count];
        
        for (int i = 0; i < cols.Count; i++)
        {
            collisionComponents[i] = new CollisionComponent(this, true, cols[i]);
        }
    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        foreach (var item in collisionComponents)
        {
            DrawDebugRectangle(spriteBatch, new Rectangle((int)position.X, (int)position.Y, item.stopRectangle.Width, item.stopRectangle.Height));
        }
    }
}
