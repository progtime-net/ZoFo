
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.ZoFo_graphics;
using ZoFo.GameCore;

namespace ZoFo.GameCore.GameObjects;

public abstract class GameObject
{
    public Vector2 position;

    private Server server;
    public Vector2 rotation; //вектор направления объекта
    public abstract GraphicsComponent graphicsComponent { get; }

    #region ServerSide
    public GameObject(Vector2 position)
    {
        this.position = position; 

        graphicsComponent.LoadContent();
    }
    public virtual void UpdateLogic(GameTime gameTime)
    { 
        PlayAnimation_OnServer();
    }


    /// <summary>
    /// Это вызывается в логике игры, которая на сервере, здесь будет вызываться уведомление об анимации
    /// </summary>
    public void PlayAnimation_OnServer()
    {
        graphicsComponent.Update();
    }

    #endregion


    #region Client Side

    /// <summary>
    /// Для клиента
    /// Это вызывается в клиентской части игры
    /// </summary>
    public void PlayAnimation_OnClient()
    {
        graphicsComponent.Update();
    }

    /// <summary>
    /// Для клиента
    /// Загрузка графического компонента
    /// </summary>
    public void LoadContent()
    {
        graphicsComponent.LoadContent();
    }

    /// <summary>
    /// Для клиента
    /// Обновление, которое вызывается у клиента, для просмотра анимаций
    /// </summary>
    public virtual void UpdateAnimations(GameTime gameTime)
    {
        PlayAnimation_OnClient();
    }

    /// <summary>
    /// Для клиента 
    /// </summary>
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