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

namespace ZoFo.GameCore.GUI;

public class MainMenuGUI : AbstractGUI
{
    private DrawableUIElement menuBackground;
    protected override void CreateUI()
    {
        int width = AppManager.Instance.CurentScreenResolution.X;
        int height = AppManager.Instance.CurentScreenResolution.Y;
        
        menuBackground = new DrawableUIElement(Manager) { rectangle = new Rectangle(0, 0, width, height), mainColor = Color.White, textureName = "Textures\\GUI\\background\\mainMenu" };
        Elements.Add(menuBackground);
        menuBackground.LoadTexture(AppManager.Instance.Content);
        
        Elements.Add(new Label(Manager) { rectangle = new Rectangle(width / 2 - (int)(width / 8), height / 5, (int)(width / 4), (int)(height / 20)), text = "ZoFo", fontColor = Color.Black, mainColor = Color.Transparent, scale = 0.9f, fontName = "Fonts\\Font"});
        
        
        Button playButton = new Button(Manager) 
        {
            rectangle = new Rectangle(width / 2 - (int)(width / 10), height / 3 + height / 20 + height / 40, (int)(width / 5), (int)(height / 20)),
            text = "Play",
            scale = 0.2f,
            fontColor = Color.White,
            mainColor = Color.Gray,
            fontName = "Fonts\\Font"
        };
        playButton.LeftButtonPressed += () =>
        {
            AppManager.Instance.SetGUI(new SelectModeMenu());
        };
        Elements.Add(playButton);
        Button baseButton = new Button(Manager) 
        {
            rectangle = new Rectangle(width / 2 - (int)(width / 10), height / 3 + (height / 20 + height / 40) * 2, (int)(width / 5), (int)(height / 20)),
            text = "Base",
            scale = 0.2f,
            fontColor = Color.White,
            mainColor = Color.Gray,
            fontName = "Fonts\\Font"
        };
        baseButton.LeftButtonPressed += () =>
        {
            AppManager.Instance.SetGUI(new BaseGUI());
        };
        Elements.Add(baseButton);
        Button optionButton = new Button(Manager)
        {
            rectangle = new Rectangle(width / 2 - (int)(width / 10), height / 3 + (height / 20 + height / 40) * 3, (int)(width / 5), (int)(height / 20)),
            text = "Options",
            scale = 0.2f,
            fontColor = Color.White,
            mainColor = Color.Gray,
            fontName = "Fonts\\Font"
        };
        optionButton.LeftButtonPressed += () =>
        {
            AppManager.Instance.SetGUI(new OptionsGUI());
        };
        Elements.Add(optionButton);
        Button exitButton = new Button(Manager)
        {
            rectangle = new Rectangle(width / 2 - (int)(width / 10), height / 3 + (height / 20 + height / 40) * 4, (int)(width / 5), (int)(height / 20)),
            text = "Exit",
            scale = 0.2f,
            fontColor = Color.White,
            mainColor = Color.Gray,
            fontName = "Fonts\\Font"
        };
        exitButton.LeftButtonPressed += () =>
        {
            AppManager.Instance.Exit();
        };
        Elements.Add(exitButton);


    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
}