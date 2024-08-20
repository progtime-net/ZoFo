using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GameManagers.CollisionManager;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;
using ZoFo.GameCore.GUI;

namespace ZoFo.GameCore.GameObjects;
public class Collectable : Interactable
{
    protected static readonly string _path = "Content/Textures/icons/Collectables/";
    public Collectable(Vector2 position) : base(position) {

        graphicsComponent.ObjectDrawRectangle.Width = 20;
        graphicsComponent.ObjectDrawRectangle.Height = 20;

        collisionComponent.triggerRectangle.Width = 20;
        collisionComponent.triggerRectangle.Height = 20;

        
        int size = 10;
        collisionComponent.triggerRectangle.X -= size;
        collisionComponent.triggerRectangle.Y -= size;
        collisionComponent.triggerRectangle.Width += 2*size;
        collisionComponent.triggerRectangle.Height += 2*size;
    }

    public override void OnInteraction(GameObject sender)
    {
        DebugHUD.DebugLog("collected");
        string lootname = this.GetType().ToString().ToLower().Split('.').Last(); 
        (sender as Player).lootData.AddLoot(lootname, 1, (sender as Player).Id);
        AppManager.Instance.server.DeleteObject(this);
        base.OnInteraction(sender);
    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        DrawDebugRectangle(spriteBatch, collisionComponent.triggerRectangle.SetOrigin(position), Color.Blue);
        base.Draw(spriteBatch);
    }

}
