using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.Graphics;

namespace ZoFo.GameCore.GameObjects
{
    internal class EntittyForAnimationTests : Entity
    {
        
        //public override GraphicsComponent graphicsComponent { get; } = new GraphicsComponent(new List<string> { "тут пишите название анимации" }, "сдублируйте " +
 

 
        public override GraphicsComponent graphicsComponent { get; } = new AnimatedGraphicsComponent(new List<string> { "zombie_idle" }, "zombie_idle");
 
        public EntittyForAnimationTests(Vector2 position) : base(position)
        {
            graphicsComponent.ObjectDrawRectangle = new Rectangle(0,0,16*20, 16 * 20);
 
            position = new Vector2(10, 10);
            
        }



    }
}
