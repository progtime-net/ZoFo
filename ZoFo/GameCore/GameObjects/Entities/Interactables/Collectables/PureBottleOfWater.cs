using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.Graphics;

namespace ZoFo.GameCore.GameObjects.Entities.Interactables.Collectables
{
     class PureBottleOfWater:Collectable
    {
        public override GraphicsComponent graphicsComponent { get; } = new(new List<string> { "PureBottleOfWater" }, "PureBottleOfWater");

        public PureBottleOfWater(Vector2 position) : base(position)
        {
        }
    }
}
