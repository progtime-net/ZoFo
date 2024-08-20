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

public class FinishingGUI : AbstractGUI
{
    private List<ItemDisplayLabel> ItemDisplayLabelsList;
    private int labelIndex = 0;
    private DrawableUIElement menuBackground;

    protected override void CreateUI()
    {
        ItemDisplayLabelsList = new List<ItemDisplayLabel>();
        int width = AppManager.Instance.CurentScreenResolution.X;
        int height = AppManager.Instance.CurentScreenResolution.Y;
        
        menuBackground = new DrawableUIElement(Manager) { rectangle = new Rectangle(0, 0, width, height), mainColor = Color.White, textureName = "Textures/GUI/background/endGame" };
        Elements.Add(menuBackground);
        menuBackground.LoadTexture(AppManager.Instance.Content);
        
        Elements.Add(new Label(Manager) { rectangle = new Rectangle(width / 2 - (int)(width / 8), height / 15, (int)(width / 4), (int)(height / 20)), text = "Mission completed", fontColor = Color.Black, mainColor = Color.Transparent, scale = 0.9f, fontName = "Fonts/Font"});

        DrawableUIElement inventoryBack = new DrawableUIElement(Manager)
        {
            rectangle = new Rectangle(width / 2 - height / 80 - width / 5 / 2,
                height / 2 - (int)(height / 1.5) / 2,
                height / 40 + width / 5, (int)(height / 1.5)),
            mainColor = Color.LightGray
        };
        Elements.Add(inventoryBack);

        Button ExitButton = new Button(Manager)
        {
            rectangle = new Rectangle(width / 2 - width / 15 / 2,
                height / 2 + (int)(height / 1.5) / 2 + height / 40, (int)(width / 15), (int)(height / 20)),
            text = "Exit",
            scale = 0.2f,
            fontColor = Color.White,
            mainColor = Color.Gray,
            fontName = "Fonts\\Font"
        };
        ExitButton.LeftButtonPressed += () => { AppManager.Instance.SetGUI(new MainMenuGUI()); };
        Elements.Add(ExitButton);

        //player itams
        foreach (var item in AppManager.Instance.client.myPlayer.lootData.loots)
        {
            if (item.Value > 0)
            {
                ItemInfo itemInfo = AppManager.Instance.ItemManager.GetItemInfo(item.Key);
                var temp = new ItemDisplayLabel(Manager)
                {
                    rectangle = new Rectangle(
                        width / 2 - width / 5 / 2,
                        height / 2 - (int)(height / 1.5) / 2 + height / 80 +
                        (height / 20 + height / 80) * (labelIndex),
                        (int)(width / 5), (int)(height / 20)),
                    text1 = item.Key,
                    scale1 = 0.4f,
                    count = item.Value,
                    itemTextureName = itemInfo.textureName,
                    fontColor1 = Color.White,
                    mainColor = Color.Gray,
                    fontName1 = "Fonts\\Font4",
                    discriptions1 = itemInfo.description,
                    resourcesNeededToCraft1 = itemInfo.resourcesNeededToCraft
                };
                Elements.Add(temp);
                temp.Initialize();
                temp.LoadTexture(AppManager.Instance.Content);
                ItemDisplayLabelsList.Add(temp);

                labelIndex++;
            }
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
}