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

public class HUD : AbstractGUI
{
    protected override void CreateUI()
    {
        int width = AppManager.Instance.CurentScreenResolution.X;
        int height = AppManager.Instance.CurentScreenResolution.Y;
        
        Button pauseButton = new Button(Manager)
            { fontName = "Fonts\\Font3", scale = 0.4f, text = "| |", fontColor = Color.Black, mainColor = Color.Transparent, rectangle = new Rectangle(width / 30, height / 30, width / 40, width / 40), textureName = "Textures\\GUI\\checkboxs_off"};
        Elements.Add(pauseButton);
        pauseButton.LeftButtonPressed += () =>
        {
            AppManager.Instance.SetGUI(new PauseGUI());
        };
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
}