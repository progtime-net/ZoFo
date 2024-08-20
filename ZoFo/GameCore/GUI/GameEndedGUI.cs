using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogameLibrary.UI.Base;
using MonogameLibrary.UI.Elements;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GameManagers.ItemManager;

namespace ZoFo.GameCore.GUI;

public class GameEndedGUI : AbstractGUI
{
    private DrawableUIElement menuBackground;
    protected override void CreateUI()
    {
        int width = AppManager.Instance.CurentScreenResolution.X;
        int height = AppManager.Instance.CurentScreenResolution.Y;
        
        menuBackground = new DrawableUIElement(Manager) { rectangle = new Rectangle(0, 0, width, height), mainColor = Color.White, textureName = "Textures/GUI/background/endGame" };
        Elements.Add(menuBackground);
        menuBackground.LoadTexture(AppManager.Instance.Content);
        
        Elements.Add(new Label(Manager) { rectangle = new Rectangle(width / 2 - (int)(width / 8), height / 5, (int)(width / 4), (int)(height / 20)), text = "The End", fontColor = Color.Black, mainColor = Color.Transparent, scale = 0.9f, fontName = "Fonts/Font"});

        Button endButton = new Button(Manager)
        {
            rectangle = new Rectangle(width / 2 - (width / 15) / 2, height / 2 + height / 4, (int)(width / 15), (int)(height / 20)),
            text = "End",
            scale = 0.3f,
            fontColor = Color.White,
            mainColor = Color.Gray,
            fontName = "Fonts/Font",
            textureName = "Textures/GUI/Button"
        };
        endButton.LeftButtonPressed += () =>
        {
            AppManager.Instance.SetGUI(new MainMenuGUI());
        };
        Elements.Add(endButton);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
}