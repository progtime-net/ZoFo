
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore;
using ZoFo.GameCore.Graphics;

namespace ZoFo.GameCore.GameObjects;

public abstract class GameObject
{
    public Vector2 position;

    public Vector2 rotation; //вектор направления объекта
    public virtual GraphicsComponent graphicsComponent { get; }

    #region ServerSide
    public GameObject(Vector2 position)
    {
        this.position = position; 
        graphicsComponent.LoadContent();

        graphicsComponent.ObjectDrawRectangle.X = (int)position.X;
        graphicsComponent.ObjectDrawRectangle.Y = (int)position.Y;

    }
    public virtual void UpdateLogic()
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

    public void Instantiate(GameObject gameObject)
    {
        if (AppManager.Instance.gamestate == GameState.HostPlaying)
        {
            AppManager.Instance.server.RegisterGameObject(gameObject);
        }
    }
    #endregion


    #region Client Side

    public static Texture2D debugTexture;
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
    /// for smooth client draw
    /// </summary>
    public Vector2 positionDraw;
    /// <summary>
    /// Для клиента
    /// Обновление, которое вызывается у клиента, для просмотра анимаций
    /// </summary>
    public virtual void UpdateAnimations()
    {
        positionDraw = (position * 0.15f + positionDraw*0.85f);
        graphicsComponent.ObjectDrawRectangle.X = (int)positionDraw.X; //Move To place where Updates Sets your position
        graphicsComponent.ObjectDrawRectangle.Y = (int)positionDraw.Y;
        PlayAnimation_OnClient();
    }

    /// <summary>
    /// Для клиента 
    /// </summary>
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        graphicsComponent.Draw(graphicsComponent.ObjectDrawRectangle, spriteBatch);
        //debug
        if (AppManager.Instance.InputManager.CollisionsCheat)
            DrawDebugRectangle(spriteBatch, graphicsComponent.ObjectDrawRectangle);

    }
    public void DrawDebugRectangle(SpriteBatch spriteBatch, Rectangle _rectangle, Nullable<Color> color = null)
    {
        if (color is null) color = new Color(1, 0, 0, 0.1f);
        if (color.Value.A == 255) color = new Color(color.Value, 0.25f);
        spriteBatch.Draw(debugTexture,
                             new Rectangle((_rectangle.X - GraphicsComponent.CameraPosition.X) * GraphicsComponent.scaling,
                             (_rectangle.Y - GraphicsComponent.CameraPosition.Y) * GraphicsComponent.scaling,
                             _rectangle.Width * GraphicsComponent.scaling,
                             _rectangle.Height * GraphicsComponent.scaling), color.Value);
     
        //TODO: debugTexture
    }
    #endregion 
}