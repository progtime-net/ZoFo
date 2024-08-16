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
        
        menuBackground = new DrawableUIElement(Manager) { rectangle = new Rectangle(0, 0, width, height), mainColor = Color.White, textureName = "Textures\\GUI\\background\\selectMode" };
        Elements.Add(menuBackground);
        menuBackground.LoadTexture(AppManager.Instance.Content);
        
        Elements.Add(new Label(Manager) { rectangle = new Rectangle(width / 2 - (int)(width / 8), height / 6, (int)(width / 4), (int)(height / 20)), text = "Select mode", fontColor = Color.White, mainColor = Color.Transparent, scale = 0.9f, fontName = "Fonts\\Font"});
        
        Button singleButton = new Button(Manager) 
        {
            rectangle = new Rectangle(width / 4 - (width / 7) / 2, height / 2, (int)(width / 7), (int)(height / 20)),
            text = "singleplayer",
            scale = 0.3f,
            fontColor = Color.White,
            mainColor = Color.Gray,
            fontName = "Fonts\\Font"
        };
        singleButton.LeftButtonPressed += () => 
        {
            // single
            Server server = new Server();
            Client client = new Client();
            server.CreateRoom(1);
            client.JoinYourself();
            
            AppManager.Instance.SetServer(server);
            AppManager.Instance.SetClient(client);
            AppManager.Instance.ChangeState(GameState.HostPlaying);
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
            fontName = "Fonts\\Font"
        };
        optionButton.LeftButtonPressed += () => 
        {
            AppManager.Instance.SetGUI(new SelectingServerGUI());
            // multi 
            Server server = new Server();   //Server Logic MultiPlayer
            Client client = new Client();
            server.CreateRoom(5);
            client.JoinRoom("127.0.0.1");   //указать айпишник
            AppManager.Instance.SetServer(server);
            AppManager.Instance.SetClient(client);
            string key = client.IsConnected.ToString();
            AppManager.Instance.debugHud.Set(key, "MultiPlayer");
            // ваш код здесь 
        };
        Elements.Add(optionButton);
        
        Button bTExit = new Button(Manager)
            { fontName = "Fonts\\Font3", scale = 0.4f, text = "<-", fontColor = Color.Black, mainColor = Color.Transparent, rectangle = new Rectangle(width / 30, height / 30, width / 40, width / 40), textureName = "Textures\\GUI\\checkboxs_off"};
        Elements.Add(bTExit);
        bTExit.LeftButtonPressed += () =>
        {
            AppManager.Instance.SetGUI(new MainMenuGUI());
        };
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
}