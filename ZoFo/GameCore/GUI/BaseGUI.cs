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
using ZoFo.GameCore.GameManagers.ItemManager;


namespace ZoFo.GameCore.GUI;

public class BaseGUI : AbstractGUI
{
    private DrawableUIElement menuBackground;
    private List<ItemDisplayLabel> ItemDisplayButtonsList;
    private int buttonIndex = 0;
    private string textureName;

    protected override void CreateUI()
    {
        ItemDisplayButtonsList = new List<ItemDisplayLabel>();
        int width = AppManager.Instance.CurentScreenResolution.X;
        int height = AppManager.Instance.CurentScreenResolution.Y;
        Dictionary<string, int> playerItems = AppManager.Instance.playerData.items;
        Dictionary<string, ItemInfo> items = AppManager.Instance.ItemManager.tagItemPairs;

        menuBackground = new DrawableUIElement(Manager)
        {
            rectangle = new Rectangle(0, 0, width, height), mainColor = Color.White,
            textureName = "Textures\\GUI\\background\\base"
        };
        Elements.Add(menuBackground);
        menuBackground.LoadTexture(AppManager.Instance.Content);

        Elements.Add(new Label(Manager)
        {
            rectangle = new Rectangle(width / 2 - (int)(width / 8), height / 15, (int)(width / 4), (int)(height / 20)),
            text = "Base", fontColor = Color.Black, mainColor = Color.Transparent, scale = 0.9f,
            fontName = "Fonts\\Font"
        });

        DrawableUIElement baseHudBack = new DrawableUIElement(Manager)
        {
            rectangle = new Rectangle(width / 2 - (int)(width / 1.5) / 2, height / 2 - (int)(height / 1.5) / 2,
                (int)(width / 1.5), (int)(height / 1.5)),
            mainColor = Color.LightGray
        };
        Elements.Add(baseHudBack);

        //player itams
        foreach (var item in playerItems)
        {
            textureName = AppManager.Instance.ItemManager.GetItemInfo(item.Key).textureName;
            var temp = new ItemDisplayLabel(Manager)
            {
                rectangle = new Rectangle(width / 2 - (int)(width / 1.5) / 2 + height / 80,
                    height / 2 - (int)(height / 1.5) / 2 + height / 80 + (height / 20 + height / 80) * (buttonIndex),
                    (int)(width / 5), (int)(height / 20)),
                text1 = item.Key,
                scale1 = 0.4f,
                count = item.Value,
                itemTextureName = textureName,
                fontColor1 = Color.White,
                mainColor = Color.Gray,
                fontName1 = "Fonts\\Font3"
            };
            Elements.Add(temp);
            temp.Initialize();
            temp.LoadTexture(AppManager.Instance.Content);
            ItemDisplayButtonsList.Add(temp);

            buttonIndex++;
        }

        // craftable items
        buttonIndex = 0;
        foreach (var item in items)
        {
            ItemInfo itemInfo = AppManager.Instance.ItemManager.GetItemInfo(item.Key);

            if (itemInfo.isCraftable)
            {
                var temp = new ItemDisplayLabel(Manager)
                {
                    rectangle = new Rectangle(width / 2 - (int)(width / 1.5) / 2 + height / 40 + width / 5,
                        height / 2 - (int)(height / 1.5) / 2 + height / 80 +
                        (height / 20 + height / 80) * (buttonIndex),
                        (int)(width / 5), (int)(height / 20)),
                    text1 = item.Key,
                    scale1 = 0.4f,
                    count = 0,
                    itemTextureName = itemInfo.textureName,
                    fontColor1 = Color.White,
                    mainColor = Color.Gray,
                    fontName1 = "Fonts\\Font3"
                };
                Elements.Add(temp);
                temp.Initialize();
                temp.LoadTexture(AppManager.Instance.Content);
                ItemDisplayButtonsList.Add(temp);

                buttonIndex++;
            }
        }

        Button bTExit = new Button(Manager)
        {
            fontName = "Fonts\\Font3", scale = 0.4f, text = "<-", fontColor = Color.Black,
            mainColor = Color.Transparent, rectangle = new Rectangle(width / 30, height / 30, width / 40, width / 40),
            textureName = "Textures\\GUI\\checkboxs_off"
        };
        Elements.Add(bTExit);
        bTExit.LeftButtonPressed += () => { AppManager.Instance.SetGUI(new MainMenuGUI()); };
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
}