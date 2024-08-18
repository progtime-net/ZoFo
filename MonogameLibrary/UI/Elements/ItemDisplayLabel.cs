using Microsoft.Xna.Framework.Graphics;
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

namespace MonogameLibrary.UI.Elements;

public class ItemDisplayLabel : DrawableUIElement
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
    

    public ItemDisplayLabel(UIManager manager) : base(manager)
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
            fontColor = fontColor1, text = text1, scale = scale1, fontName = fontName1, mainColor = Color.Transparent
        };
        Label itemCount = new Label(Manager)
        {
            rectangle = new Rectangle(rectangle.X + rectangle.Width - (int)(rectangle.Height / 3 * 2.5), rectangle.Y + rectangle.Height / 3 / 2, rectangle.Height / 3 * 2, rectangle.Height / 3 * 2),
            fontColor = fontColor1, text = count.ToString(), scale = scale1, fontName = fontName1, mainColor = Color.Transparent
        };
    }

    public override void LoadTexture(ContentManager content)
    {
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

    public override void Draw(SpriteBatch _spriteBatch)
    {
        base.Draw(_spriteBatch);
        _spriteBatch.Draw(texture, rectangle, Color.White);
    }
}