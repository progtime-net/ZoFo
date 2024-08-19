using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GUI;

namespace ZoFo.GameCore.Graphics
{
    public class StaticGraphicsComponent : GraphicsComponent
    {
        private Texture2D texture;
        public string _textureName;

        public StaticGraphicsComponent()
        {
            LoadContent();
        }

        public StaticGraphicsComponent(string textureName)
        {
            BuildComponent(textureName);
            LoadContent();
        }

        public void BuildComponent(string textureName)
        {
            _textureName = textureName;
        }


        public override void LoadContent()
        {
            if (_textureName is null)
            {
                return;
            }

            texture = AppManager.Instance.Content.Load<Texture2D>(Path.Combine(AppContext.BaseDirectory, _textureName));
        }

        public override void Update()
        {
            
        }

        public override void Draw(Rectangle destinationRectangle, SpriteBatch _spriteBatch)
        {
            DebugHUD.Instance.Log("draw ");

            destinationRectangle.X -= CameraPosition.X;
            destinationRectangle.Y -= CameraPosition.Y;
            destinationRectangle = Scaling(destinationRectangle);
            _spriteBatch.Draw(texture, destinationRectangle, texture.Bounds, Color.White, Rotation,
                Vector2.Zero, Flip, 0);
        }

        public override void Draw(Rectangle destinationRectangle, SpriteBatch _spriteBatch, Rectangle sourceRectangle)
        {
            // Uncomment to go brrrr
            //Rotation = new Random().Next(1, 365);
            DebugHUD.Instance.Log("draw ");

            destinationRectangle.X -= CameraPosition.X;
            destinationRectangle.Y -= CameraPosition.Y;

            destinationRectangle = Scaling(destinationRectangle);
            _spriteBatch.Draw(texture,
                destinationRectangle, sourceRectangle, Color.White, Rotation,
                Vector2.Zero, Flip, 0);
        }
    }
}