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
        public override GraphicsComponent graphicsComponent { get; } = new AnimatedGraphicsComponent("Textures/icons/8");
        public Zombie(Vector2 position) : base(position)
        {
            health = 5;
            speed =2;
            collisionComponent.stopRectangle = new Rectangle(0, 0, 100, 100);
            graphicsComponent.ObjectDrawRectangle = new Rectangle(0, 0, 100, 100);
        }
        
        public override void Update()
        {
            Vector2 duration = Vector2.Normalize(
                AppManager.Instance.server.players[0].position - position
                );
            velocity=new Vector2(duration.X * speed, duration.Y*speed);
            if(position.X>595 && 605>position.X && position.Y>495 && 505>position.Y)
            {
                velocity = Vector2.Zero;
            }
            //position.X += velocity.X*t;
            //position.Y += velocity.Y * t;
        }
    }
}
