using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.ZoFo_graphics;

namespace ZoFo.GameCore.GameObjects.MapObjects
{
    internal class MapObject : GameObject
    {
        public virtual bool IsColliderOn { get; protected set; } = true;
        private Rectangle _sourceRectangle;
        protected override GraphicsComponent graphicsComponent => new("tiles");

        public MapObject(Vector2 position, Vector2 size, Rectangle sourceRectangle) : base(position)
        {
            _sourceRectangle = sourceRectangle;
            graphicsComponent.ObjectDrawRectangle = new Rectangle(0,0, (int)size.X, (int)size.Y);
        }
         
    }
}
