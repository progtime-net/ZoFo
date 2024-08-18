using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.Graphics;

namespace ZoFo.GameCore.GameObjects.Entities
{
    internal class EntittyForAnimationTests : Entity
    {
        
        //public override GraphicsComponent graphicsComponent { get; } = new GraphicsComponent(new List<string> { "тут пишите название анимации" }, "сдублируйте " +
 
        public override GraphicsComponent graphicsComponent { get; } = new AnimatedGraphicsComponent(new List<string> { "player_idle_rotate_weapon" }, "player_idle_rotate_weapon");
 
        public EntittyForAnimationTests(Vector2 position) : base(position)
        {
            graphicsComponent.ObjectDrawRectangle = new Rectangle(0,0,16*12, 16 * 16);
            position = new Vector2(10, 10);
            
        }



    }
}
