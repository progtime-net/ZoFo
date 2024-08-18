using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.Graphics;

namespace ZoFo.GameCore.GameObjects.Entities.LivingEntities.Enemies
{
    class Zombie : Enemy
    {
        public override GraphicsComponent graphicsComponent { get; } = new AnimatedGraphicsComponent(new List<string> { "zombie_damaged", "zombie_walk", "zombie_idle", "zombie_attack", "zombie_death" }, "zombie_walk");
        public Zombie(Vector2 position) : base(position)
        {
            health = 5;
            speed = 2;
            graphicsComponent.ObjectDrawRectangle = new Rectangle(0, 0, 30, 30);
            collisionComponent.stopRectangle = new Rectangle(10, 20, 10, 10);
        }

        public override void Update()
        {
            Vector2 duration = Vector2.Normalize(
                AppManager.Instance.server.players[0].position - position
                );
            velocity += new Vector2(duration.X * speed, duration.Y * speed);
            if (Random.Shared.NextDouble() > 0.9)
            {

                StartAnimation("zombie_damaged");
            }
            if (Random.Shared.NextDouble() > 0.9)
            {

                StartAnimation("zombie_idle");
            }

        }
    }
}
