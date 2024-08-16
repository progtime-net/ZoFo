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

public class OptionsGUI : AbstractGUI
{
    private DrawableUIElement menuBackground;
    protected override void CreateUI()
    {
        int width = AppManager.Instance.CurentScreenResolution.X;
        int height = AppManager.Instance.CurentScreenResolution.Y;
        
        menuBackground = new DrawableUIElement(Manager) { rectangle = new Rectangle(0, 0, width, height), mainColor = Color.White, textureName = "Textures\\GUI\\background\\options" };
        Elements.Add(menuBackground);
        menuBackground.LoadTexture(AppManager.Instance.Content);
        
        Elements.Add(new Label(Manager) { rectangle = new Rectangle(width / 2 - (int)(width / 8), height / 5, (int)(width / 4), (int)(height / 20)), text = "Options", fontColor = Color.Black, mainColor = Color.Transparent, scale = 0.9f, fontName = "Fonts\\Font"});

        

            Label label_OverallVolume = new Label(Manager)
            { fontName = "Fonts\\Font", scale = 0.2f, text = "All Volume", fontColor = Color.Black, rectangle = new Rectangle(width / 3, height / 3, width / 40, height / 20), mainColor = Color.Transparent, textAligment = MonogameLibrary.UI.Enums.TextAligment.Left };
            Elements.Add(label_OverallVolume);
            
            Label label_OverallVolume_Percent = new Label(Manager) 
            { fontName = "Fonts\\Font3", scale = 0.4f, text = "", fontColor = Color.Black, rectangle = new Rectangle(width / 2 + width / 10, height / 3, width / 40, height / 20), mainColor = Color.Transparent, textAligment = MonogameLibrary.UI.Enums.TextAligment.Left };
            Elements.Add(label_OverallVolume_Percent);

            var slider_OverallVolume = new Slider(Manager)
            { rectangle = new Rectangle(width / 2, height / 3, width / 10, height / 20), indentation = 7, textureName = "Textures\\GUI\\Switch_backgrownd", MinValue = 0, MaxValue = 1 };
            slider_OverallVolume.SliderChanged += (newVal) =>
            {
                label_OverallVolume_Percent.text = Math.Round(slider_OverallVolume.GetSliderValue * 100) + "%";
                AppManager.Instance.SettingsManager.SetMainVolume(newVal);
            };
            Elements.Add(slider_OverallVolume);

            //--------------------------------------
            
            Label label_MusicVolume = new Label(Manager)
            { fontName = "Fonts\\Font", scale = 0.2f, text = "Music Volume", fontColor = Color.Black, rectangle = new Rectangle(width / 3, height / 3 + (height / 20 + height / 40) * 1, width / 40, height / 20), mainColor = Color.Transparent, textAligment = MonogameLibrary.UI.Enums.TextAligment.Left };
            Elements.Add(label_MusicVolume);
            
            Label label_MusicVolume_Percent = new Label(Manager)
                { fontName = "Fonts\\Font3", scale = 0.4f, text = "", fontColor = Color.Black, rectangle = new Rectangle(width / 2 + width / 10, height / 3 + (height / 20 + height / 40) * 1, width / 40, height / 20), mainColor = Color.Transparent, textAligment = MonogameLibrary.UI.Enums.TextAligment.Left };
            Elements.Add(label_MusicVolume_Percent);
            
            var slider_MusicVolume = new Slider(Manager)
            { rectangle = new Rectangle(width / 2, height / 3 + (height / 20 + height / 40) * 1, width / 10, height / 20), indentation = 7, textureName = "Textures\\GUI\\Switch_backgrownd", MinValue = 0, MaxValue = 1 };
            slider_MusicVolume.SliderChanged += (newVal) =>
            {
                label_MusicVolume_Percent.text = Math.Round(slider_MusicVolume.GetSliderValue * 100) + "%";
                AppManager.Instance.SettingsManager.SetMusicVolume(newVal);
            }; 
            Elements.Add(slider_MusicVolume);

            //--------------------------------------

            Label label_EffectsVolume = new Label(Manager)
            { fontName = "Fonts\\Font", scale = 0.2f, text = "Effects Volume", fontColor = Color.Black, rectangle = new Rectangle(width / 3, height / 3 + (height / 20 + height / 40) * 2, width / 40, height / 20), mainColor = Color.Transparent, textAligment = MonogameLibrary.UI.Enums.TextAligment.Left };
            Elements.Add(label_EffectsVolume);
            
            Label label_EffectsVolume_Percent = new Label(Manager)
                { fontName = "Fonts\\Font3", scale = 0.4f, text = "", fontColor = Color.Black, rectangle = new Rectangle(width / 2 + width / 10, height / 3 + (height / 20 + height / 40) * 2, width / 40, height / 20), mainColor = Color.Transparent, textAligment = MonogameLibrary.UI.Enums.TextAligment.Left };
            Elements.Add(label_EffectsVolume_Percent);

            var slider_EffectsVolume = new Slider(Manager)
            { rectangle = new Rectangle(width / 2, height / 3 + (height / 20 + height / 40) * 2, width / 10, height / 20), indentation = 7, textureName = "Textures\\GUI\\Switch_backgrownd", MinValue = 0, MaxValue = 1 };
            slider_EffectsVolume.SliderChanged += (newVal) =>
            {
                label_EffectsVolume_Percent.text = Math.Round(slider_EffectsVolume.GetSliderValue * 100) + "%";
                AppManager.Instance.SettingsManager.SetSoundEffectsVolume(newVal);
            };
            Elements.Add(slider_EffectsVolume);

            //--------------------------------------

            Label lblSwitchMode = new Label(Manager)
            { fontName = "Fonts\\Font", scale = 0.2f, text = "Resolution set", fontColor = Color.Black, rectangle = new Rectangle(width / 3, height / 3 + (height / 20 + height / 40) * 3, width / 40, height / 20), mainColor = Color.Transparent, textAligment = MonogameLibrary.UI.Enums.TextAligment.Left };
            Elements.Add(lblSwitchMode);

            //var button_left_right_mode = new CheckBox(Manager) { rectangle = new Rectangle(rightBorder - checkboxlength, lblSwitchMode.rectangle.Y - 12, checkboxlength, checkboxlength) };
            //button_left_right_mode.Checked += (newCheckState) => { };
            //Elements.Add(button_left_right_mode);


            Label label_IsFullScreen = new Label(Manager)
            { fontName = "Fonts\\Font", scale = 0.2f, text = "Full Screen", fontColor = Color.Black, rectangle = new Rectangle(width / 3, height / 3 + (height / 20 + height / 40) * 4, width / 40, width / 40), mainColor = Color.Transparent, textAligment = MonogameLibrary.UI.Enums.TextAligment.Left };
            Elements.Add(label_IsFullScreen);

            var button_FullScreen = new CheckBox(Manager) { rectangle = new Rectangle(width / 2, height / 3 + (height / 20 + height / 40) * 4, width / 40, width / 40) };
            button_FullScreen.SetIsChecked(AppManager.Instance.SettingsManager.IsFullScreen);
            button_FullScreen.Checked += (newCheckState) =>
            {
                AppManager.Instance.SettingsManager.SetIsFullScreen(newCheckState);
            };
            Elements.Add(button_FullScreen);

            //--------------------------------------
            
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