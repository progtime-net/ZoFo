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

public class HUD : AbstractGUI
{
    private Bar hpBar;
    private Bar radBar;
    private AbstractGUI overlayGUI;
    protected override void CreateUI() 
    {
        int width = AppManager.Instance.CurentScreenResolution.X;
        int height = AppManager.Instance.CurentScreenResolution.Y;
        
        Button pauseButton = new Button(Manager)
            { fontName = "Fonts\\Font3", scale = 0.4f, text = "| |", fontColor = Color.Black, mainColor = Color.Transparent, rectangle = new Rectangle(width - width / 30 - width / 40, height / 30, width / 40, width / 40), textureName = "Textures\\GUI\\checkboxs_off"};
        Elements.Add(pauseButton);
        pauseButton.LeftButtonPressed += () =>
        {
            AppManager.Instance.SetGUI(new FinishingGUI());
            //overlayGUI = new PauseGUI();
            //overlayGUI.Initialize();
            //overlayGUI.LoadContent();
        };
        Button invButton = new Button(Manager)
            { fontName = "Fonts\\Font3", scale = 0.4f, text = "inv", fontColor = Color.Black, mainColor = Color.Transparent, rectangle = new Rectangle(width - width / 30 - width / 40, height / 15 + width / 40, width / 40, width / 40), textureName = "Textures\\GUI\\checkboxs_off"};
        Elements.Add(invButton);
        invButton.LeftButtonPressed += () =>
        {
            overlayGUI = new InventoryGUI();
            overlayGUI.Initialize();
            overlayGUI.LoadContent();
        };

        hpBar = new Bar(Manager)
        {
            rectangle = new Rectangle(width / 2 - width / 8 - width / 16, height - height / 10, width / 8, height / 25),
            percent = 1f,
            mainColor = Color.LightGray,
            inColor = Color.Red
        };
        hpBar.Initialize();
        hpBar.LoadTexture(AppManager.Instance.Content);
        radBar = new Bar(Manager)
        {
            rectangle = new Rectangle(width / 2 + width / 16, height - height / 10, width / 8, height / 25),
            percent = 0f,
            mainColor = Color.LightGray,
            inColor = Color.GreenYellow
        };
        radBar.Initialize();
        radBar.LoadTexture(AppManager.Instance.Content);
        
    }
    public override void Update(GameTime gameTime)
    {
        overlayGUI?.Update(gameTime);
        //hpBar.Update(gameTime, AppManager.Instance.client.myPlayer.health / 100f);
        //radBar.Update(gameTime, AppManager.Instance.client.myPlayer.rad / 100f);
        radBar.Update(gameTime, gameTime.TotalGameTime.Seconds / 100f);
        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        overlayGUI?.Draw(spriteBatch);
        base.Draw(spriteBatch);
    }
}