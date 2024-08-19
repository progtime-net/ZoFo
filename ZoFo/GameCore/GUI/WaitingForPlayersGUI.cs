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

public class WaitingForPlayersGUI : AbstractGUI
{
    private DrawableUIElement menuBackground;
    private bool isHost;
    private Label ip;

    public WaitingForPlayersGUI(bool isHost)
    {
        this.isHost = isHost;
    }
    protected override void CreateUI()
    {
        int width = AppManager.Instance.CurentScreenResolution.X;
        int height = AppManager.Instance.CurentScreenResolution.Y;

        menuBackground = new DrawableUIElement(Manager) { rectangle = new Rectangle(0, 0, width, height), mainColor = Color.White, textureName = "Textures/GUI/background/Waiting" };
        Elements.Add(menuBackground);
        menuBackground.LoadTexture(AppManager.Instance.Content); 
        //   string pcIp = 
 
     //   string pcIp = 
  //      ip = new Label(Manager) { rectangle = new Rectangle(width / 2 - (int)(width / 8), height / 7, (int)(width / 4), (int)(height / 20)), text = AppManager.Instance.server.MyIp.ToString(), fontColor = Color.White, mainColor = Color.Transparent, scale = 0.9f, fontName = "Fonts/Font3" };
    //    Elements.Add(ip);  
        if (isHost)
        {
            ip = new Label(Manager) { rectangle = new Rectangle(width / 2 - (int)(width / 8), height / 7, (int)(width / 4), (int)(height / 20)), text = AppManager.Instance.server.MyIp.ToString(), fontColor = Color.White, mainColor = Color.Transparent, scale = 0.9f, fontName = "Fonts\\Font3" };
            Elements.Add(ip);
            Button startButton = new Button(Manager)
            {
                rectangle = new Rectangle(width / 2 - (width / 15) / 2, height / 2 + height / 4, (int)(width / 15), (int)(height / 20)),
                text = "Start",
                scale = 0.3f,
                fontColor = Color.White,
                mainColor = Color.Gray,
                fontName = "Fonts/Font"
            };
            startButton.LeftButtonPressed += () =>
            {
                // start
                AppManager.Instance.ChangeState(GameState.HostPlaying);
                AppManager.Instance.SetGUI(new HUD());
                AppManager.Instance.server.StartGame();
                // ваш код здесь 
            };
            Elements.Add(startButton);
        }
        else {
            ip = new Label(Manager) { rectangle = new Rectangle(width / 2 - (int)(width / 8), height / 7, (int)(width / 4), (int)(height / 20)), text = AppManager.Instance.client.InfoConnect.ToString(), fontColor = Color.White, mainColor = Color.Transparent, scale = 0.9f, fontName = "Fonts\\Font3" };
            Elements.Add(ip);
            Button waitButton = new Button(Manager)
            {
                rectangle = new Rectangle(width / 2 - (width / 15) / 2, height / 2 + height / 4, (int)(width / 15), (int)(height / 20)),
                text = "WAITING",
                scale = 0.3f,
                fontColor = Color.White,
                mainColor = Color.Gray,
                fontName = "Fonts/Font"
            };
            waitButton.LeftButtonPressed += () =>
            {
                // start
                AppManager.Instance.client.SendData();
                AppManager.Instance.ChangeState(GameState.ClientPlaying);
                AppManager.Instance.SetGUI(new HUD());
                // ваш код здесь 
            };
            Elements.Add(waitButton);
        }
        
        Button bTExit = new Button(Manager)
            { fontName = "Fonts/Font3", scale = 0.4f, text = "<-", fontColor = Color.Black, mainColor = Color.Transparent, rectangle = new Rectangle(width / 30, height / 30, width / 40, width / 40), textureName = "Textures/GUI/checkboxs_off"};
        Elements.Add(bTExit);
        bTExit.LeftButtonPressed += () =>
        {
            AppManager.Instance.SetGUI(new SelectingServerGUI());
        };
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
}