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

namespace ZoFo.GameCore.GameObjects
{
     class PureBottleOfWater:Collectable
    {
        public override StaticGraphicsComponent graphicsComponent { get; } = new(_path + "PureBottleOfWater");

        public PureBottleOfWater(Vector2 position) : base(position)
        {
            graphicsComponent.ObjectDrawRectangle.Width = 20;
            graphicsComponent.ObjectDrawRectangle.Height = 20;
        }
        public override void OnInteraction(GameObject sender)
        {
            AppManager.Instance.server.AddData(new UpdateLoot("PureBottleOfWater"));
            AppManager.Instance.server.DeleteObject(this);
        }
    }
}
