using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogameLibrary.UI.Base;
using MonogameLibrary.UI.Elements;
using ZoFo.GameCore.GameManagers;

namespace ZoFo.GameCore.GUI;

public class SelectingServerGUI : AbstractGUI
{
    private DrawableUIElement menuBackground;
    protected override void CreateUI()
    {
        int width = AppManager.Instance.CurentScreenResolution.X;
        int height = AppManager.Instance.CurentScreenResolution.Y;

        menuBackground = new DrawableUIElement(Manager) { rectangle = new Rectangle(0, 0, width, height), mainColor = Color.White, textureName = "Textures/GUI/background/join" };
        Elements.Add(menuBackground);
        menuBackground.LoadTexture(AppManager.Instance.Content);

        Elements.Add(new Label(Manager) { rectangle = new Rectangle(width / 2 - (int)(width / 8), height / 7, (int)(width / 4), (int)(height / 20)), text = "Select server", fontColor = Color.White, mainColor = Color.Transparent, scale = 0.9f, fontName = "Fonts/Font" });

        TextBox ipBox = new TextBox(Manager)
        {
            rectangle = new Rectangle(width / 4 - (width / 4) / 2, height / 4, (int)(width / 4), (int)(height / 20)),
            text = "ip",
            scale = 0.5f,
            fontColor = Color.White,
            mainColor = Color.Gray,
            textAligment = MonogameLibrary.UI.Enums.TextAligment.Left,
            fontName = "Fonts/Font3"
        };
        ipBox.TextChanged += input =>
        {
            if (input == "ip")
            {
                ipBox.text = ""; ipBox.fontColor = Color.White;
            }
        };
        ipBox.StopChanging += input =>
        {
            if (input.Length == 0)
            {
                ipBox.fontColor = Color.White;
                ipBox.text = "ip";
            }
        };
        Elements.Add(ipBox);
        Button joinButton = new Button(Manager)
        {
            rectangle = new Rectangle(width / 4 + (width / 4) / 2, height / 4, (int)(width / 15), (int)(height / 20)),
            text = "Join",
            scale = 0.3f,
            fontColor = Color.White,
            mainColor = Color.Gray,
            fontName = "Fonts/Font"
        };
        joinButton.LeftButtonPressed += () =>
        {

            // join
            Client client = new Client();
            var endpoint = ipBox.text.Split(':');
            int port;
            try
            {
                if (int.TryParse(endpoint[1], out port))
                {
                    client.JoinRoom(endpoint[0], port);
                    AppManager.Instance.SetClient(client);
                    AppManager.Instance.SetGUI(new WaitingForPlayersGUI(false));
                }
            }
            catch (Exception)
            {

                //  throw;
            }

            // ваш код здесь
        };
        Elements.Add(joinButton);
        Button hostButton = new Button(Manager)
        {
            rectangle = new Rectangle(width / 4 + (width / 4) / 2 + (width / 15), height / 4, (int)(width / 15), (int)(height / 20)),
            text = "Host",
            scale = 0.3f,
            fontColor = Color.White,
            mainColor = Color.Gray,
            fontName = "Fonts/Font"
        };
        hostButton.LeftButtonPressed += () =>
        {

            // host
            Server server = new Server();   //Server Logic MultiPlayer
            server.CreateRoom(1);
            AppManager.Instance.SetServer(server);
            string key = server.MyIp.ToString();
            AppManager.Instance.debugHud.Set(key, "MultiPlayer");
            // ваш код здесь 
            AppManager.Instance.SetGUI(new WaitingForPlayersGUI(true));
        };
        Elements.Add(hostButton);

        Button bTExit = new Button(Manager)
        { fontName = "Fonts/Font3", scale = 0.4f, text = "<-", fontColor = Color.Black, mainColor = Color.Transparent, rectangle = new Rectangle(width / 30, height / 30, width / 40, width / 40), textureName = "Textures/GUI/checkboxs_off" };
        Elements.Add(bTExit);
        bTExit.LeftButtonPressed += () =>
        {
            AppManager.Instance.SetGUI(new SelectModeMenu());
        };
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
}