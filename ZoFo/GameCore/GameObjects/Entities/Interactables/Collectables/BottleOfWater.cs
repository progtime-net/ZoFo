using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.Graphics;

using Microsoft.Xna.Framework;

namespace ZoFo.GameCore.GameObjects.Entities.Interactables.Collectables
{
    public class BottleOfWater : Collectable
    {
        public override GraphicsComponent graphicsComponent { get; } = new(new List<string> { "BottleOfWater" }, "BottleOfWater");
        public BottleOfWater(Vector2 position) : base(position)
        {

        }

    }
}
