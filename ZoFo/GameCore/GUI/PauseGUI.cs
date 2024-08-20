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

public class PauseGUI : AbstractGUI
{
    protected override void CreateUI()
    {
        int width = AppManager.Instance.CurentScreenResolution.X;
        int height = AppManager.Instance.CurentScreenResolution.Y;
        
        Button continueButton = new Button(Manager) 
        {
            rectangle = new Rectangle(width / 2 - (int)(width / 10), height / 3 + height / 20 + height / 40, (int)(width / 5), (int)(height / 20)),
            text = "Continue",
            scale = 0.2f,
            fontColor = Color.White,
            mainColor = Color.Gray,
            fontName = "Fonts\\Font"
        };
        continueButton.LeftButtonPressed += () =>
        {
            AppManager.Instance.SoundManager.StartAmbientSound("Button click");
            AppManager.Instance.SetGUI(new HUD());
        };
        Elements.Add(continueButton);
        Button exitButton = new Button(Manager) 
        {
            rectangle = new Rectangle(width / 2 - (int)(width / 10), height / 3 + (height / 20 + height / 40) * 2, (int)(width / 5), (int)(height / 20)),
            text = "Exit",
            scale = 0.2f,
            fontColor = Color.White,
            mainColor = Color.Gray,
            fontName = "Fonts\\Font"
        };
        exitButton.LeftButtonPressed += () =>
        {
            AppManager.Instance.SoundManager.StopAllSounds();
            AppManager.Instance.SoundManager.StartAmbientSound("Button click");
            AppManager.Instance.SoundManager.StartAmbientSound("Background menu music");
            AppManager.Instance.SetGUI(new MainMenuGUI());
        };
        Elements.Add(exitButton);
    }
    
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
}