using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogameLibrary.UI.Base;
using MonogameLibrary.UI.Elements;
using ZoFo.GameCore.GameManagers;

namespace ZoFo.GameCore.GUI;

public abstract class AbstractGUI
{
    protected UIManager Manager = new();
    protected List<DrawableUIElement> Elements = new();
    private List<DrawableUIElement> ActiveElements;
    protected DrawableUIElement SelectedElement;
    private bool isStartedPrint = false;
    private bool isPressed = false;
    private Texture2D mouse;
    private MouseState mouseState;

    public AbstractGUI()
    {
    }

    protected abstract void CreateUI();
    private GraphicsDevice graphicsDevice;
    public virtual void Initialize()
    {
        Manager.Initialize(AppManager.Instance.GraphicsDevice);
        CreateUI();
    }

    public virtual void LoadContent()
    {
        Manager.LoadContent(AppManager.Instance.Content, "Font");
        mouse = AppManager.Instance.Content.Load<Texture2D>("Textures\\GUI\\mouse");
    }

    public virtual void Update(GameTime gameTime)
    {
        Manager.Update(gameTime);
        mouseState = Mouse.GetState();
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        Manager.Draw(spriteBatch);
        spriteBatch.Begin();
        spriteBatch.Draw(mouse, new Rectangle(mouseState.Position.X, mouseState.Position.Y, 20, 40), Color.White);
        spriteBatch.End();
    }
}