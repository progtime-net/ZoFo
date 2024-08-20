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
    internal class Particle : GameObject
    {
        public override GraphicsComponent graphicsComponent { get; } = new AnimatedGraphicsComponent(new List<string> { "explosion_1" }, "explosion_1");
        
        public Particle(Vector2 position) : base(position)
        {
            graphicsComponent.ObjectDrawRectangle = new Rectangle(-30, -30,60,60).SetOrigin(position);
            (graphicsComponent as AnimatedGraphicsComponent).actionOfAnimationEnd += _ => { 
                 
                    AppManager.Instance.client.DeleteObject(this);
            
            };
        } 
    }
}
