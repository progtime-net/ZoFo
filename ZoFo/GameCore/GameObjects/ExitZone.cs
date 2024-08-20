using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GameManagers.CollisionManager;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;
using ZoFo.GameCore.Graphics;

namespace ZoFo.GameCore.GameObjects
{
    public class ExitZone : Entity
    {


        public override GraphicsComponent graphicsComponent { get; } = new StaticGraphicsComponent("Content/Textures/icons/ExitZone");
        public ExitZone(Vector2 position) : base(position)
        {
            collisionComponent.OnTriggerZone += Exit;
            graphicsComponent.ObjectDrawRectangle.Width = 100;
            graphicsComponent.ObjectDrawRectangle.Height = 100;
            position = new Vector2(500f, 500f);
            collisionComponent.isTrigger = true;
            collisionComponent.triggerRectangle = new Rectangle(0, 0, 100, 100);
        }


        public void Exit(GameObject sender)
        {
            if (sender is Player &&
                AppManager.Instance.server.collisionManager.GetPlayersInZone(collisionComponent.triggerRectangle.SetOrigin(position)).Length == AppManager.Instance.server.players.Count)
            {
                sender.position = new Vector2(0f, 0f);
                AppManager.Instance.server.EndGame();
                AppManager.Instance.debugHud.Set("Exit", sender.position.ToString());
            }
        }
    }
}
