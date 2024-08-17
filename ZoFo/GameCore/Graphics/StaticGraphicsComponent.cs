using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZoFo.GameCore.GameManagers;

namespace ZoFo.GameCore.Graphics
{

    public class StaticGraphicsComponent : GraphicsComponent
    {
        private Texture2D texture;
        private string textureName;


        public StaticGraphicsComponent()
        {
           
        }

        public StaticGraphicsComponent(string textureName)
        {
            BuildComponent(textureName);
        }

        public void BuildComponent(string textureName)
        {
            this.textureName = textureName;
        }
        

        public override void LoadContent()
        {
            texture = AppManager.Instance.Content.Load<Texture2D>(textureName);
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public override void Draw(Rectangle destinationRectangle, SpriteBatch _spriteBatch)
        {
            destinationRectangle.X -= CameraPosition.X;
            destinationRectangle.Y -= CameraPosition.Y;
            destinationRectangle = Scaling(destinationRectangle);
            _spriteBatch.Draw(texture, destinationRectangle, Color.White);
        }

        public override void Draw(Rectangle destinationRectangle, SpriteBatch _spriteBatch, Rectangle sourceRectangle)
        {
            destinationRectangle.X -= CameraPosition.X;
            destinationRectangle.Y -= CameraPosition.Y;

            destinationRectangle = Scaling(destinationRectangle);
            _spriteBatch.Draw(texture,
                destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
