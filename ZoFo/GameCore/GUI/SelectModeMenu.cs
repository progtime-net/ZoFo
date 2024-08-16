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

public class SelectModeMenu : AbstractGUI
{
    private DrawableUIElement menuBackground;
    protected override void CreateUI()
    {
        int width = AppManager.Instance.CurentScreenResolution.X;
        int height = AppManager.Instance.CurentScreenResolution.Y;
        
        menuBackground = new DrawableUIElement(Manager) { rectangle = new Rectangle(0, 0, width, height), mainColor = Color.White, textureName = "Textures\\GUI\\MenuBackground" };
        Elements.Add(menuBackground);
        menuBackground.LoadTexture(AppManager.Instance.Content);
        
        Elements.Add(new Label(Manager) { rectangle = new Rectangle(width / 2 - (int)(width / 8), height / 6, (int)(width / 4), (int)(height / 20)), text = "Select mode", fontColor = Color.White, mainColor = Color.Transparent, scale = 0.9f, fontName = "Fonts\\Font"});
        
        Button playButton = new Button(Manager) 
        {
            rectangle = new Rectangle(width / 4 - (width / 7) / 2, height / 2, (int)(width / 7), (int)(height / 20)),
            text = "singleplayer",
            scale = 0.3f,
            fontColor = Color.White,
            mainColor = Color.Gray,
            fontName = "Fonts\\Font"
        };
        playButton.LeftButtonPressed += () => 
        {
            // single
            
            // ваш код здесь 
        };
        Elements.Add(playButton);
        Button optionButton = new Button(Manager) 
        {
            rectangle = new Rectangle(width / 2 + width / 4 - (width / 7) / 2, height / 2, (int)(width / 7), (int)(height / 20)),
            text = "multiplayer",
            scale = 0.3f,
            fontColor = Color.White,
            mainColor = Color.Gray,
            fontName = "Fonts\\Font"
        };
        optionButton.LeftButtonPressed += () => 
        {
            // multi 
            
            // ваш код здесь 
        };
        Elements.Add(optionButton);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
}