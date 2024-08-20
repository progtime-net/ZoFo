using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers.CollisionManager;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.Graphics;
using Microsoft.Xna.Framework.Graphics;
using ZoFo.GameCore.GUI;

namespace ZoFo.GameCore.GameObjects
{
    class Ammo : Collectable
    {
        public override StaticGraphicsComponent graphicsComponent { get; } = new(_path + "Ammo");
        public Ammo(Vector2 position) : base(position)
        {
            graphicsComponent.ObjectDrawRectangle.Width = 20;
            graphicsComponent.ObjectDrawRectangle.Height = 20;

            collisionComponent.triggerRectangle = new Rectangle(0, 0, 20, 20);
        }
        public override void OnInteraction(GameObject sender)
        {
            DebugHUD.DebugLog("collected");
            if (AppManager.Instance.client.myPlayer.lootData.loots.Keys.Contains("Ammo"))
            {
                AppManager.Instance.client.myPlayer.lootData.loots["Ammo"] += 1;
            }
            else
            {
                AppManager.Instance.client.myPlayer.lootData.loots.Add("Ammo", 1);
            }
            AppManager.Instance.server.AddData(new UpdateLoot("Ammo"));
            AppManager.Instance.server.DeleteObject(this);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawDebugRectangle(spriteBatch, collisionComponent.triggerRectangle.SetOrigin(position), Color.Blue);
            base.Draw(spriteBatch);
        }
    }
}
