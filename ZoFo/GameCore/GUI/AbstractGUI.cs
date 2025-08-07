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
    private Texture2D mouse_pressed;
    private MouseState mouseState;
    private AbstractGUI _overlayGUI;
    public AbstractGUI overlayGUI { get { return _overlayGUI; } set { _overlayGUI = value; _overlayGUI?.SetParent(this); } } //main child
    public AbstractGUI parent; //This AbstractGUI can be put in any other abstractGUI, so it needs the ability to get to the parent
    bool initialized = false;
    bool loadedContent = false;
    public AbstractGUI()
    {
    }
    public AbstractGUI(AbstractGUI startOverlay)
    {
        overlayGUI = startOverlay;
    }

    protected abstract void CreateUI();
    private GraphicsDevice graphicsDevice;
    public virtual void Initialize()
    {
        if (initialized) return;
        
        Manager.Initialize(AppManager.Instance.GraphicsDevice);
        CreateUI();
        initialized = true;

        overlayGUI?.Initialize();
    }

    public virtual void LoadContent()
    {
        if (loadedContent) return;
        Manager.LoadContent(AppManager.Instance.Content, "Fonts/Font");
        mouse = AppManager.Instance.Content.Load<Texture2D>("Textures/GUI/mouse");
        mouse_pressed = AppManager.Instance.Content.Load<Texture2D>("Textures/GUI/mouse_pressed");
        
        overlayGUI?.LoadContent();
        loadedContent = true;
    }

    public virtual void Update(GameTime gameTime)
    {
        Manager.Update(gameTime);
        mouseState = Mouse.GetState();

        overlayGUI?.Update(gameTime);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        if (!initialized) return;
        if (!loadedContent) return;
        Manager.Draw(spriteBatch);
        spriteBatch.Begin();
        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            spriteBatch.Draw(mouse_pressed, new Rectangle(mouseState.Position.X, mouseState.Position.Y, 20, 40), Color.White);
        else
            spriteBatch.Draw(mouse, new Rectangle(mouseState.Position.X, mouseState.Position.Y, 20, 40), Color.White);

        spriteBatch.End();
        
        overlayGUI?.Draw(spriteBatch);
    }

    #region Overlay
    public void SetOverlay(AbstractGUI abstractGUI, bool cancelLoad = false, bool cancelInitializing = false)
    {
        overlayGUI = abstractGUI;
        if (!cancelInitializing) overlayGUI?.Initialize();
        if (!cancelLoad) overlayGUI?.LoadContent();
    }
    public void SetOverlay_WithoutLoading(AbstractGUI newOverlay)
    {
        overlayGUI = newOverlay;
        overlayGUI?.Initialize();
        overlayGUI?.LoadContent();
    }
    /// <summary>
    /// In Case Overlay is not initialized or loaded
    /// </summary>
    public void InitializeAndLoadOverlay()
    {
        overlayGUI?.Initialize();
        overlayGUI?.LoadContent();
    }
    public void RemoveOverlay() => overlayGUI = null;
    public void DeleteOverlayInParent() => parent?.RemoveOverlay();

    public AbstractGUI StartTransition(AbstractGUI TransitionScreen)
    {
        SetOverlay(TransitionScreen);
        return TransitionScreen;
    }
    public void SetParent(AbstractGUI abstractGUI)
    {
        parent = abstractGUI;
    }
    #endregion
}