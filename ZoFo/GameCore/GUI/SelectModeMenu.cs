﻿using System;
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
        
        menuBackground = new DrawableUIElement(Manager) { rectangle = new Rectangle(0, 0, width, height), mainColor = Color.White, textureName = "Textures/GUI/background/selectMode" };
        Elements.Add(menuBackground);
        menuBackground.LoadTexture(AppManager.Instance.Content);
        
        Elements.Add(new Label(Manager) { rectangle = new Rectangle(width / 2 - (int)(width / 8), height / 6, (int)(width / 4), (int)(height / 20)), text = "Select mode", fontColor = Color.White, mainColor = Color.Transparent, scale = 0.9f, fontName = "Fonts/Font"});
        
        Button singleButton = new Button(Manager) 
        {
            rectangle = new Rectangle(width / 4 - (width / 7) / 2, height / 2, (int)(width / 7), (int)(height / 20)),
            text = "singleplayer",
            scale = 0.3f,
            fontColor = Color.White,
            mainColor = Color.Gray,
            fontName = "Fonts/Font",
            textureName = "Textures/GUI/Button"
        };
        singleButton.LeftButtonPressed += () => 
        {
            AppManager.Instance.SoundManager.StopAllSounds();
            AppManager.Instance.SoundManager.StartAmbientSound("Background music");
            AppManager.Instance.SoundManager.StartAmbientSound("Button click");
            // single
            Server server = new Server();
            Client client = new Client();
            AppManager.Instance.SetServer(server);
            AppManager.Instance.SetClient(client);
            server.CreateRoom(false);
            client.JoinYourself(server.MyIp.Port);
            //AppManager.Instance.ChangeState(GameState.HostPlaying);
            AppManager.Instance.SetGUI(new HUD());

            //server.CreateRoom(1);
            //client.JoinYourself();
            server.StartGame();

            string key = client.IsConnected.ToString();
            AppManager.Instance.debugHud.Set(key,"SinglePlayer");
            // ваш код здесь 
        };
        Elements.Add(singleButton);
        Button optionButton = new Button(Manager) 
        {
            rectangle = new Rectangle(width / 2 + width / 4 - (width / 7) / 2, height / 2, (int)(width / 7), (int)(height / 20)),
            text = "multiplayer",
            scale = 0.3f,
            fontColor = Color.White,
            mainColor = Color.Gray,
            fontName = "Fonts/Font",
            textureName = "Textures/GUI/Button"
        };
        optionButton.LeftButtonPressed += () => 
        {
            AppManager.Instance.SoundManager.StartAmbientSound("Button click");
            AppManager.Instance.SetGUI(new SelectingServerGUI());
            // multi 
           
            // ваш код здесь 
        };
        Elements.Add(optionButton);
        
        Button bTExit = new Button(Manager)
        {
            fontName = "Fonts/Font3", scale = 0.4f, text = "<-", fontColor = Color.Black, mainColor = Color.Transparent, 
            rectangle = new Rectangle(width / 30, height / 30, width / 40, width / 40),
            textureName = "Textures/GUI/Button2"
        };
        Elements.Add(bTExit);
        bTExit.LeftButtonPressed += () =>
        {
            
            AppManager.Instance.SoundManager.StartAmbientSound("Button click");
            
            AppManager.Instance.SetGUI(new MainMenuGUI());
        };
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
}