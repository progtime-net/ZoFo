using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZoFo.GameCore.Graphics;

public abstract class GraphicsComponent
{
    public Rectangle ObjectDrawRectangle;
    public static int scaling = 3;
    public string mainTextureName;//TODO костыль - пофиксить

    public SpriteEffects Flip = SpriteEffects.None;
    public float Rotation;

    public abstract void LoadContent();
    public abstract void Update();
    public abstract void Draw(Rectangle destinationRectangle, SpriteBatch _spriteBatch);
    public abstract void Draw(Rectangle destinationRectangle, SpriteBatch _spriteBatch, Rectangle sourceRectangle);
    
    protected Rectangle Scaling(Rectangle destinationRectangle)
    {
        destinationRectangle.X *= scaling;
        destinationRectangle.Y *= scaling;
        destinationRectangle.Width *= scaling;
        destinationRectangle.Height *= scaling;
        return destinationRectangle;
    }
    
    public static void SetCameraPosition(Vector2 playerPosition)
    {
        CameraPosition = (playerPosition).ToPoint();
        CameraPosition.X -= 200;
        CameraPosition.Y -= 120;
            
        // TODO
        /*
        if (CameraPosition.X > AppManager.Instance.GameManager.CameraBorder.Y - 460)
        {
            CameraPosition.X = (int)AppManager.Instance.GameManager.CameraBorder.Y - 460;
        }

        if (CameraPosition.Y < AppManager.Instance.GameManager.CameraBorder.Z)
        {
            CameraPosition.Y = (int)AppManager.Instance.GameManager.CameraBorder.Z;
        }
        if (CameraPosition.X < AppManager.Instance.GameManager.CameraBorder.X)
        {
            CameraPosition.X = (int)AppManager.Instance.GameManager.CameraBorder.X;
        }
        if (CameraPosition.Y > AppManager.Instance.GameManager.CameraBorder.W - 240)
        {
            CameraPosition.Y = (int)AppManager.Instance.GameManager.CameraBorder.W - 240;
        }

        AppManager.Instance.DebugHUD.Set("CameraPosition", $"{CameraPosition.X}, {CameraPosition.Y}");
    */
    }
    public static Point CameraPosition = new Point(0, 0);
}