using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.Graphics;
using Microsoft.Xna.Framework;


namespace ZoFo.GameCore.GameObjects.Entities.Interactables.Collectables
{
    public class Peeble:Collectable
    {
        public override GraphicsComponent graphicsComponent { get; } = new(new List<string> { "Peeble" }, "Peeble");

        public Peeble(Vector2 position) : base(position)
        {

        }
    }
}
