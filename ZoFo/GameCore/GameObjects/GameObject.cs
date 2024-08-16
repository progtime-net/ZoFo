
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.ZoFo_graphics;

namespace ZoFo.GameCore.GameObjects;

public abstract class GameObject
{
    public Vector2 position;
    public Vector2 rotation; //вектор направления объекта
    protected abstract GraphicsComponent graphicsComponent { get; }

    #region ServerSide
    public GameObject(Vector2 position)
    {
        this.position = position;
    }
     

    /// <summary>
    /// Это вызывается в логике игры, которая на сервере, здесь будет вызываться уведомление об анимации
    /// </summary>
    public void PlayAnimation_OnServer()
    {
        //TODO
    }

    #endregion


    #region Client Side

    /// <summary>
    /// Это вызывается в клиентской части игры
    /// </summary>
    public void PlayAnimation_OnClient()
    {
        graphicsComponent.Update();
    }
    public void LoadContent()
    {
        graphicsComponent.LoadContent();
    }

    public virtual void Update(GameTime gameTime)
    {
        //PlayAnimation();
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        graphicsComponent.DrawAnimation(graphicsComponent.ObjectDrawRectangle, spriteBatch);
        //debug
        if (AppManager.Instance.InputManager.CollisionsCheat)
            DrawDebugRectangle(spriteBatch, graphicsComponent.ObjectDrawRectangle);

    }
    public void DrawDebugRectangle(SpriteBatch spriteBatch, Rectangle _rectangle, Nullable<Color> color = null)
    {
        if (color is null) color = new Color(1, 0, 0, 0.25f);
        if (color.Value.A == 255) color = new Color(color.Value, 0.25f);
        //spriteBatch.Draw(debugTexture,
        //                     new Rectangle((_rectangle.X - GraphicsComponent.CameraPosition.X) * GraphicsComponent.scaling,
        //                     (_rectangle.Y - GraphicsComponent.CameraPosition.Y) * GraphicsComponent.scaling,
        //                     _rectangle.Width * GraphicsComponent.scaling,
        //                     _rectangle.Height * GraphicsComponent.scaling), color.Value);
     
        //TODO: debugTexture
    }
    #endregion 
}