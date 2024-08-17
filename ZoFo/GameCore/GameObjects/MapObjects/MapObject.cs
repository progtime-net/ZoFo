using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;
using ZoFo.GameCore.ZoFo_graphics;

namespace ZoFo.GameCore.GameObjects.MapObjects
{
    public class MapObject : GameObject
    {
        public virtual bool IsColliderOn { get; protected set; } = true;//Who added that?
        public Rectangle sourceRectangle;
        public override GraphicsComponent graphicsComponent { get; } =  new();

        /// <summary>
        /// Создается простой объект на карте - no animations, только где, насколько крупно рисовать, по какой сорс ректанглу рисовать и из какой текстуры
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="sourceRectangle"></param>
        /// <param name="textureName"></param>
        public MapObject(Vector2 position, Vector2 size, Rectangle sourceRectangle, string textureName) : base(position)
        {
            this.sourceRectangle = sourceRectangle;
            graphicsComponent.ObjectDrawRectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            graphicsComponent.BuildComponent(textureName);

            graphicsComponent.LoadContent();

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            graphicsComponent.DrawAnimation(graphicsComponent.ObjectDrawRectangle, spriteBatch, sourceRectangle);
        }

    }
}
