﻿using Microsoft.Xna.Framework;
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

namespace ZoFo.GameCore.GameObjects.MapObjects
{
    public class MapObject : GameObject
    {
        public virtual bool IsColliderOn { get; protected set; } = true;//Who added that?
        public Rectangle sourceRectangle;
        public override GraphicsComponent graphicsComponent { get; } 
            = new ManualGraphicsComponent();

        /// <summary>
        /// Создается простой объект на карте - no animations, только где, насколько крупно рисовать, по какой сорс ректанглу рисовать и из какой текстуры
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="sourceRectangle"></param>
        /// <param name="textureName"></param>
        public MapObject(Vector2 position, Vector2 size, List<FrameContainer> frames, string textureName) : base(position)
        {
            (graphicsComponent as ManualGraphicsComponent).ObjectDrawRectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            (graphicsComponent as ManualGraphicsComponent).LoadContent();
            this.sourceRectangle = sourceRectangle;
            (graphicsComponent as ManualGraphicsComponent).BuildComponent(textureName, frames);

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            graphicsComponent.Draw(graphicsComponent.ObjectDrawRectangle, spriteBatch);
        }
        

    }
}
