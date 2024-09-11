using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.Graphics;

namespace ZoFo.GameCore.GameObjects
{
    public class Particle : GameObject
    {
        public override GraphicsComponent graphicsComponent { get; } = new AnimatedGraphicsComponent(new List<string> { "explosion_1" }, "explosion_1");

        public Particle(Vector2 position) : base(position)
        {
        }
    }
}
