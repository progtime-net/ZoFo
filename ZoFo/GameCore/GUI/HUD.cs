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

namespace ZoFo.GameCore.GUI;

public class HUD
{
    protected UIManager Manager = new();
    protected List<DrawableUIElement> Elements = new();
    private List<DrawableUIElement> ActiveElements;
    protected DrawableUIElement SelectedElement;
    private bool isStartedPrint = false;
    private bool isPressed = false;

    private GraphicsDevice graphicsDevice;
    public virtual void Initialize()
    {
        
    }

    public virtual void LoadContent()
    {
        
    }

    public virtual void Update(GameTime gameTime)
    {
        
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        Manager.Draw(spriteBatch);
    }
}