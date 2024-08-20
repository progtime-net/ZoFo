using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.Graphics;

using Microsoft.Xna.Framework;
using ZoFo.GameCore.GameManagers.CollisionManager;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;
using ZoFo.GameCore.GameManagers;

namespace ZoFo.GameCore.GameObjects
{
    public class BottleOfWater : Collectable
    {
        public override StaticGraphicsComponent graphicsComponent { get; } = new(_path + "BottleOfWater");
        public BottleOfWater(Vector2 position) : base(position) { }
    
    }
}
