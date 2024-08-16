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

public class OptionsGUI : AbstractGUI
{
    private DrawableUIElement menuBackground;
    protected override void CreateUI()
    {
        int width = AppManager.Instance.CurentScreenResolution.X;
        int height = AppManager.Instance.CurentScreenResolution.Y;
        
        menuBackground = new DrawableUIElement(Manager) { rectangle = new Rectangle(0, 0, width, height), mainColor = Color.White, textureName = "Textures\\GUI\\MenuBackground" };
        Elements.Add(menuBackground);
        menuBackground.LoadTexture(AppManager.Instance.Content);
        
        Elements.Add(new Label(Manager) { rectangle = new Rectangle(width / 2 - (int)(width / 8), height / 5, (int)(width / 4), (int)(height / 20)), text = "Options", fontColor = Color.White, mainColor = Color.Transparent, scale = 0.9f, fontName = "Fonts\\Font"});

        

            Label label_OverallVolume = new Label(Manager)
            { fontName = "Fonts\\Font", scale = 0.2f, text = "All Volume", fontColor = Color.White, rectangle = new Rectangle(width / 3, height / 3, 50, 50), mainColor = Color.Transparent, textAligment = MonogameLibrary.UI.Enums.TextAligment.Left };
            Elements.Add(label_OverallVolume);

            var slider_OverallVolume = new Slider(Manager)
            { rectangle = new Rectangle(width / 2, height / 3, width / 10, height / 20), indentation = 4, textureName = "Textures\\GUI\\checkbox_on", MinValue = 0, MaxValue = 1 };
            slider_OverallVolume.SliderChanged += (newVal) =>
            {
                
            };
            Elements.Add(slider_OverallVolume);

            Label label_MusicVolume = new Label(Manager)
            { fontName = "Fonts\\Font", scale = 0.2f, text = "Music Volume", fontColor = Color.White, rectangle = new Rectangle(width / 3, height / 3 + (height / 20 + height / 40) * 1, 50, 50), mainColor = Color.Transparent, textAligment = MonogameLibrary.UI.Enums.TextAligment.Left };
            Elements.Add(label_MusicVolume);
            
            var slider_MusicVolume = new Slider(Manager)
            { rectangle = new Rectangle(width / 2, height / 3 + (height / 20 + height / 40) * 1, width / 10, height / 20), indentation = 4, textureName = "Textures\\GUI\\checkboxs_on", MinValue = 0, MaxValue = 1 };
            slider_MusicVolume.SliderChanged += (newVal) =>
            {
                
            }; 
            Elements.Add(slider_MusicVolume);


            Label label_EffectsVolume = new Label(Manager)
            { fontName = "Fonts\\Font", scale = 0.2f, text = "Effects Volume", fontColor = Color.White, rectangle = new Rectangle(width / 3, height / 3 + (height / 20 + height / 40) * 2, 50, 50), mainColor = Color.Transparent, textAligment = MonogameLibrary.UI.Enums.TextAligment.Left };
            Elements.Add(label_EffectsVolume);

            var slider_EffectsVolume = new Slider(Manager)
            { rectangle = new Rectangle(width / 2, height / 3 + (height / 20 + height / 40) * 2, width / 10, height / 20), indentation = 4, textureName = "Textures\\GUI\\checkboxs_on", MinValue = 0, MaxValue = 1 };
            slider_EffectsVolume.SliderChanged += (newVal) =>
            {
                
            };
            Elements.Add(slider_EffectsVolume);

            Label lblSwitchMode = new Label(Manager)
            { fontName = "Fonts\\Font", scale = 0.2f, text = "Left/Right Mode", fontColor = Color.White, rectangle = new Rectangle(width / 3, height / 3 + (height / 20 + height / 40) * 3, 50, 50), mainColor = Color.Transparent};
            Elements.Add(lblSwitchMode);

            //var button_left_right_mode = new CheckBox(Manager) { rectangle = new Rectangle(rightBorder - checkboxlength, lblSwitchMode.rectangle.Y - 12, checkboxlength, checkboxlength) };
            //button_left_right_mode.Checked += (newCheckState) => { };
            //Elements.Add(button_left_right_mode);


            Label label_IsFullScreen = new Label(Manager)
            { fontName = "Fonts\\Font", scale = 0.2f, text = "Full Screen", fontColor = Color.White, rectangle = new Rectangle(width / 3, height / 3 + (height / 20 + height / 40) * 4, 50, 50), mainColor = Color.Transparent};
            Elements.Add(label_IsFullScreen);

            var button_FullScreen = new CheckBox(Manager) { rectangle = new Rectangle(width / 2, height / 3 + (height / 20 + height / 40) * 4, width / 30, width / 30) };
            button_FullScreen.Checked += (newCheckState) =>
            {
                
            };
            Elements.Add(button_FullScreen);


            Button bTExit = new Button(Manager)
            { fontName = "Fonts\\Font", scale = 0.2f, text = "<-", rectangle = new Rectangle(width / 30, height / 30, 40, 40), textureName = "Textures\\GUI\\checkboxs_off" };
            Elements.Add(bTExit);
            bTExit.LeftButtonPressed += () =>
            {
                
            };
        
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
}