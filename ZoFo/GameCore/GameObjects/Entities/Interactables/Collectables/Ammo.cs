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
        public Ammo(Vector2 position) : base(position) { }
         
    }
}
