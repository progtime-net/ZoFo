using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZoFo.GameCore.Graphics;

public interface IGraphicsComponent
{
    public Rectangle ObjectDrawRectangle { get; set; }
    public static int scaling = 1;
    public string mainTextureName { get; set; }//TODO костыль - пофиксить

    public abstract void LoadContent();
    public abstract void Update();
    public abstract void Draw(Rectangle destinationRectangle, SpriteBatch _spriteBatch);
    public abstract void Draw(Rectangle destinationRectangle, SpriteBatch _spriteBatch, Rectangle sourceRectangle);
}