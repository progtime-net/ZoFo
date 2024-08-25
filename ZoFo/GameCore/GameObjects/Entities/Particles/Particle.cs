using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GameManagers.CollisionManager;
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
    public class Explosion : Particle
    {
        public override GraphicsComponent graphicsComponent { get; } = new AnimatedGraphicsComponent(new List<string> { "explosion_1" }, "explosion_1");

        public Explosion(Vector2 position) : base(position)
        {
            graphicsComponent.ObjectDrawRectangle = new Rectangle(-30, -30, 60, 60).SetOrigin(position);
            AppManager.Instance.SoundManager.StartSound("gun-gunshot-01", Vector2.Zero, Vector2.Zero, 0.5f, (float)(Random.Shared.NextDouble() * 2 - 1));
            (graphicsComponent as AnimatedGraphicsComponent).actionOfAnimationEnd += _ => {

                Delete_OnClient(this);

            };
        }
    }
}
