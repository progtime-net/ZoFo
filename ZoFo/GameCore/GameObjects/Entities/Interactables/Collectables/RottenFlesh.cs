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
    class RottenFlesh:Collectable
    {
        public override StaticGraphicsComponent graphicsComponent { get; } = new(_path + "RottenFlesh");
        public RottenFlesh(Vector2 position) : base(position)
        {

        }
        public override void OnInteraction(GameObject sender)
        {
            AppManager.Instance.server.AddData(new UpdateLoot("RottenFlesh"));
            AppManager.Instance.server.DeleteObject(this);
        }
    }
}
