using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;
using ZoFo.GameCore.Graphics;

namespace ZoFo.GameCore.GameObjects
{
    public class MapObject : GameObject
    {
        public virtual bool IsColliderOn { get; protected set; } = true;//Who added that?
        public Rectangle sourceRectangle;
        public override GraphicsComponent graphicsComponent { get; } 
            = new StaticGraphicsComponent();

        /// <summary>
        /// Создается простой объект на карте - no animations, только где, насколько крупно рисовать, по какой сорс ректанглу рисовать и из какой текстуры
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="sourceRectangle"></param>
        /// <param name="textureName"></param>
        public MapObject(Vector2 position, Vector2 size, Rectangle sourceRectangle, string textureName) : base(position)
        {
            (graphicsComponent as StaticGraphicsComponent)._textureName = textureName;
            (graphicsComponent as StaticGraphicsComponent).BuildComponent(textureName);
            (graphicsComponent as StaticGraphicsComponent).ObjectDrawRectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            (graphicsComponent as StaticGraphicsComponent).LoadContent();
            this.sourceRectangle = sourceRectangle;

        }
        /// <summary>
        /// Конструктор для объектов с карты с анимациями, REDO
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="sourceRectangle"></param>
        /// <param name="textureName"></param>
        /// <param name="sourceRectangles"></param>
        /// <param name="frameSize"></param>
        public MapObject(Vector2 position, Vector2 size, Rectangle sourceRectangle, string textureName, Rectangle sourceRectangles, int frameSize) : base(position)
        {
            //graphicsComponent for source
            (graphicsComponent as StaticGraphicsComponent)._textureName = textureName;
            (graphicsComponent as StaticGraphicsComponent).BuildComponent(textureName);
            (graphicsComponent as StaticGraphicsComponent).ObjectDrawRectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            (graphicsComponent as StaticGraphicsComponent).LoadContent();
            this.sourceRectangle = sourceRectangle;

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            graphicsComponent.Draw(graphicsComponent.ObjectDrawRectangle, spriteBatch, sourceRectangle);
        }

    }
}
