﻿using Microsoft.Xna.Framework.Graphics;
using MonogameLibrary.UI.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameLibrary.UI.Enums;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace MonogameLibrary.UI.Elements;

public class ItemDisplayButton : Button
{
    public int count;
    public string itemTextureName;
    private Texture2D itemTexture;
    public Color fontColor1;
    protected UIManager Manager;
    public string fontName1;
    public string text1;
    public float scale1;
    private DrawableUIElement icon;
    private HoverState presentState = HoverState.None;
    private HoverWindow hoverWindow = null;
    private ContentManager content;
    public string discriptions1;
    public Dictionary<string, int> resourcesNeededToCraft1;
    public TextAligment TextAligment = TextAligment.Left;
    

    public ItemDisplayButton(UIManager manager) : base(manager)
    {
        Manager = manager; 
    }

    public void Initialize()
    {
        icon = new DrawableUIElement(Manager)
        {
            rectangle = new Rectangle(rectangle.X + rectangle.Height / 3 / 2, rectangle.Y + rectangle.Height / 3 / 2, rectangle.Height / 3 * 2, rectangle.Height / 3 * 2), 
            mainColor = Color.White, textureName = itemTextureName
        };
        Label itemName = new Label(Manager)
        {
            rectangle = new Rectangle(rectangle.X + rectangle.Height / 3 / 2 + rectangle.Height / 3 * 2, rectangle.Y + rectangle.Height / 3 / 2, rectangle.Width / 3, rectangle.Height / 3 * 2),
            fontColor = fontColor1, text = text1, scale = scale1, fontName = fontName1, mainColor = Color.Transparent, textAligment = TextAligment
        };
        Label itemCount = new Label(Manager)
        {
            rectangle = new Rectangle(rectangle.X + rectangle.Width - (int)(rectangle.Height / 3 * 2.5), rectangle.Y + rectangle.Height / 3 / 2, rectangle.Height / 3 * 2, rectangle.Height / 3 * 2),
            fontColor = fontColor1, text = count.ToString(), scale = scale1, fontName = fontName1, mainColor = Color.Transparent
        };
    }

    public override void LoadTexture(ContentManager content)
    {
        this.content = content;
        icon.LoadTexture(content);
        base.LoadTexture(content);
        if (itemTextureName == "")
        {
            itemTexture = new Texture2D(Manager.GraphicsDevice, 1, 1);
            itemTexture.SetData<Color>(new Color[] { mainColor });
        }
        else
        {
            try
            {
                itemTexture = content.Load<Texture2D>(itemTextureName);
            }
            catch
            {
                itemTexture = new Texture2D(Manager.GraphicsDevice, 1, 1);
                itemTexture.SetData<Color>(new Color[] { mainColor });
            }
        }
    }


    public bool Update(GameTime gameTime)
    {
        
        if (hoverState == HoverState.Hovering)
        {
            if (presentState != hoverState)
            {
                if (resourcesNeededToCraft1 == null)
                {
                    resourcesNeededToCraft1 = new Dictionary<string, int>();
                }
                hoverWindow = new HoverWindow(Manager)
                {
                    rectangle = new Rectangle(Mouse.GetState().Position.X, Mouse.GetState().Position.Y, rectangle.Width, rectangle.Height * 10),
                    mainColor = Color.Gray,
                    tagItem = text1,
                    discriptions = discriptions1,
                    resourcesNeededToCraft = resourcesNeededToCraft1,
                    fontColor2 = fontColor1,
                    fontName2 = fontName1,
                    scale2 = scale1,
                    itemTextureName1 = itemTextureName,
                    textureName = "Textures/GUI/Back"
                };
                hoverWindow.Initialize(content);
                hoverWindow.LoadTexture(content);
            }

            hoverWindow.rectangle.X = Mouse.GetState().Position.X;
            hoverWindow.rectangle.Y = Mouse.GetState().Position.Y;
            hoverWindow.Update(gameTime);
        }
        else if (hoverState == HoverState.None)
        {
            if (presentState != hoverState)
            {
                return true;
            }
            
        }

        presentState = hoverState;
        return false;
    }

    public override void Draw(SpriteBatch _spriteBatch)
    {
        base.Draw(_spriteBatch);
    }
}