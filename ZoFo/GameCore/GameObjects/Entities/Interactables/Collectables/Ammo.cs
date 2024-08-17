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

namespace ZoFo.GameCore.GameObjects.Entities.Interactables.Collectables
{
    class Ammo:Collectable
    {
        public override GraphicsComponent graphicsComponent { get; } = new("Textures/icons/8");
        public Ammo(Vector2 position) : base(position)
        {
            graphicsComponent.ObjectDrawRectangle.Width = 20;
            graphicsComponent.ObjectDrawRectangle.Height = 20;
        }
        public override void OnInteraction(object sender, CollisionComponent e)
        {
            AppManager.Instance.server.AddData(new UpdateLoot("Ammo"));
            AppManager.Instance.server.DeleteObject(this);
        }
    }
}
