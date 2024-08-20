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
    private List<ItemDisplayLabel> ItemDisplayLabelsList;
    private List<ItemDisplayButton> ItemDisplayButtonsList;
    private int buttonIndex = 0;
    private string textureName;

    protected override void CreateUI()
    {
        ItemDisplayLabelsList = new List<ItemDisplayLabel>();
        ItemDisplayButtonsList = new List<ItemDisplayButton>();
        int width = AppManager.Instance.CurentScreenResolution.X;
        int height = AppManager.Instance.CurentScreenResolution.Y;
        Dictionary<string, int> playerItems = AppManager.Instance.playerData.items;
        Dictionary<string, ItemInfo> items = AppManager.Instance.ItemManager.tagItemPairs;
        int numberCraftItem = 0;

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

        DrawableUIElement baseItemBack = new DrawableUIElement(Manager)
        {
            rectangle = new Rectangle(width / 2 - (height / 16 + (int)(width / 2.5)) / 2,
                height / 2 - (int)(height / 1.5) / 2,
                height / 40 + width / 5, (int)(height / 1.5)),
            mainColor = Color.LightGray,
            textureName = "Textures/GUI/Back"
        };
        Elements.Add(baseItemBack);
        DrawableUIElement baseCraftBack = new DrawableUIElement(Manager)
        {
            rectangle = new Rectangle(width / 2 + height / 160, height / 2 - (int)(height / 1.5) / 2,
                height / 40 + width / 5, (int)(height / 1.5)),
            mainColor = Color.LightGray,
            textureName = "Textures/GUI/Back"
        };
        Elements.Add(baseCraftBack);

        //player itams
        foreach (var item in playerItems)
        {
            if (item.Value > 0)
            {
                ItemInfo itemInfo = AppManager.Instance.ItemManager.GetItemInfo(item.Key);
                var temp = new ItemDisplayLabel(Manager)
                {
                    rectangle = new Rectangle(width / 2 - (height / 16 + (int)(width / 2.5)) / 2 + height / 80,
                        height / 2 - (int)(height / 1.5) / 2 + height / 80 +
                        (height / 20 + height / 80) * (buttonIndex),
                        (int)(width / 5), (int)(height / 20)),
                    text1 = item.Key,
                    scale1 = 0.4f,
                    count = item.Value,
                    itemTextureName = itemInfo.textureName,
                    fontColor1 = Color.White,
                    mainColor = Color.Gray,
                    fontName1 = "Fonts\\Font4",
                    discriptions1 = itemInfo.description,
                    resourcesNeededToCraft1 = itemInfo.resourcesNeededToCraft,
                    textureName = "Textures/GUI/Button"
                };
                Elements.Add(temp);
                temp.Initialize();
                temp.LoadTexture(AppManager.Instance.Content);
                ItemDisplayLabelsList.Add(temp);

                buttonIndex++;
            }
        }

        // craftable items
        buttonIndex = 0;
        foreach (var item in items)
        {
            try
            {
                ItemInfo itemInfo = AppManager.Instance.ItemManager.GetItemInfo(item.Key);
                if (itemInfo.isCraftable)
                {
                    Dictionary<string, int> needToCraft =
                        AppManager.Instance.ItemManager.GetItemInfo(item.Key).resourcesNeededToCraft;
                    numberCraftItem = playerItems[needToCraft.Keys.First()] / needToCraft.Values.First();
                    foreach (var itemNeedToCraft in needToCraft)
                    {
                        int xValue = playerItems[itemNeedToCraft.Key] / itemNeedToCraft.Value;
                        if (numberCraftItem > xValue)
                        {
                            numberCraftItem = xValue;
                        }
                    }

                    if (numberCraftItem > 0)
                    {
                        var temp = new ItemDisplayButton(Manager)
                        {
                            rectangle = new Rectangle(
                                width / 2 - (height / 16 + (int)(width / 2.5)) / 2 + height / 20 + width / 5,
                                height / 2 - (int)(height / 1.5) / 2 + height / 80 +
                                (height / 20 + height / 80) * (buttonIndex),
                                (int)(width / 5), (int)(height / 20)),
                            text1 = item.Key,
                            scale1 = 0.4f,
                            count = numberCraftItem,
                            itemTextureName = itemInfo.textureName,
                            fontColor1 = Color.White,
                            mainColor = Color.Gray,
                            fontName1 = "Fonts\\Font4",
                            discriptions1 = itemInfo.description,
                            resourcesNeededToCraft1 = itemInfo.resourcesNeededToCraft,
                            textureName = "Textures/GUI/Button"
                        };
                        Elements.Add(temp);
                        temp.Initialize();
                        temp.LoadTexture(AppManager.Instance.Content);
                        ItemDisplayButtonsList.Add(temp);
                        temp.LeftButtonPressed += () =>
                        {
                            AppManager.Instance.playerData.CraftItem(item.Key);
                            AppManager.Instance.SetGUI(new BaseGUI());
                        };

                        buttonIndex++;
                    }
                }
            }
            catch
            {
                
            }
        }

        Button bTExit = new Button(Manager)
        {
            fontName = "Fonts\\Font3", scale = 0.4f, text = "<-", fontColor = Color.Black,
            mainColor = Color.Transparent, rectangle = new Rectangle(width / 30, height / 30, width / 40, width / 40),
            textureName = "Textures/GUI/Button2"
        };
        Elements.Add(bTExit);
        bTExit.LeftButtonPressed += () => { AppManager.Instance.SetGUI(new MainMenuGUI()); };
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        foreach (var item in ItemDisplayButtonsList)
        {
            if (item.Update(gameTime))
            {
                AppManager.Instance.SetGUI(new BaseGUI());
            }
        }
        foreach (var item in ItemDisplayLabelsList)
        {
            if (item.Update(gameTime))
            {
                AppManager.Instance.SetGUI(new BaseGUI());
            }
        }
    }
}