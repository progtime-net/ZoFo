using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.Graphics;

namespace ZoFo.GameCore.GameObjects.Entities.Interactables.Collectables
{
    class Steel:Collectable
    {
        public override GraphicsComponent graphicsComponent { get; } = new(new List<string> { "Steel" }, "Steel");

        public Steel(Vector2 position) : base(position)
        {
        }
    }
}
