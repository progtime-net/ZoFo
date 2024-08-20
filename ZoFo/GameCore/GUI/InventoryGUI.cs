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

public class InventoryGUI : AbstractGUI
{
    private List<ItemDisplayButton> ItemDisplayButtonsList;
    private int buttonIndex = 0;

    protected override void CreateUI()
    {
        ItemDisplayButtonsList = new List<ItemDisplayButton>();
        int width = AppManager.Instance.CurentScreenResolution.X;
        int height = AppManager.Instance.CurentScreenResolution.Y;

        DrawableUIElement inventoryBack = new DrawableUIElement(Manager)
        {
            rectangle = new Rectangle(width / 2 - height / 80 - width / 5 / 2,
                height / 2 - (int)(height / 1.5) / 2 - height / 10,
                height / 40 + width / 5, (int)(height / 1.5)),
            mainColor = Color.LightGray,
            textureName = "Textures/GUI/Back"
        };
        Elements.Add(inventoryBack);

        Button continueButton = new Button(Manager)
        {
            rectangle = new Rectangle(width / 2 - width / 5 / 2,
                height / 2 + (int)(height / 1.5) / 2 + height / 40 - height / 10, (int)(width / 5), (int)(height / 20)),
            text = "Continue",
            scale = 0.2f,
            fontColor = Color.White,
            mainColor = Color.Gray,
            fontName = "Fonts\\Font",
            textureName = "Textures/GUI/Button"
        };
        continueButton.LeftButtonPressed += () => { AppManager.Instance.SetGUI(new HUD()); };
        Elements.Add(continueButton);

        //player itams
        foreach (var item in AppManager.Instance.client.myPlayer.lootData.loots)
        {
            if (item.Value > 0)
            {
                ItemInfo itemInfo = AppManager.Instance.ItemManager.GetItemInfo(item.Key);
                var temp = new ItemDisplayButton(Manager)
                {
                    rectangle = new Rectangle(
                        width / 2 - width / 5 / 2,
                        height / 2 - (int)(height / 1.5) / 2 + height / 80 +
                        (height / 20 + height / 80) * (buttonIndex) - height / 10,
                        (int)(width / 5), (int)(height / 20)),
                    text1 = item.Key,
                    scale1 = 0.3f,
                    count = item.Value,
                    itemTextureName = itemInfo.textureName,
                    fontColor1 = Color.White,
                    mainColor = Color.Gray,
                    fontName1 = "Fonts\\Font4",
                    discriptions1 = itemInfo.description,
                    resourcesNeededToCraft1 = itemInfo.resourcesNeededToCraft,
                    textureName = "Texturs/GUI/Button"
                };
                Elements.Add(temp);
                temp.Initialize();
                temp.LoadTexture(AppManager.Instance.Content);
                ItemDisplayButtonsList.Add(temp);
                temp.LeftButtonPressed += () =>
                {
                    
                };

                buttonIndex++;
            }
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
}