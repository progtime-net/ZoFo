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

public class HoverWindow : DrawableUIElement
{
    public string tagItem;
    public string discriptions;
    public Dictionary<string, int> resourcesNeededToCraft;
    private List<Label> Labels = new List<Label>();
    public float scale2;
    public string fontName2;
    public Color fontColor2;
    public string itemTextureName1;
    private DrawableUIElement icon;
    private Label itemName;
    private Label discription;
    private int labelIndex = 0;
    
    
    public HoverWindow(UIManager manager, int layerIndex = 0, string textureName = "") : base(manager, layerIndex, textureName)
    {
    }

    public void Initialize(ContentManager content)
    {
        icon = new DrawableUIElement(Manager)
        {
            rectangle = new Rectangle(rectangle.X + rectangle.Width / 20, rectangle.Y + rectangle.Width / 20, rectangle.Width / 5, rectangle.Width / 5), 
            mainColor = Color.White, textureName = itemTextureName1
        };
        icon.LoadTexture(content);
        itemName = new Label(Manager)
        {
            rectangle = new Rectangle(rectangle.X + rectangle.Width / 20 + rectangle.Width / 5 + rectangle.Width / 20, rectangle.Y + rectangle.Width / 10, rectangle.Width / 3 * 2, rectangle.Width / 5 ),
            fontColor = fontColor2, text = tagItem, scale = scale2, fontName = fontName2, mainColor = Color.Transparent, textAligment = TextAligment.Left
        };
        itemName.LoadTexture(content);
        discription = new Label(Manager)
        {
            rectangle = new Rectangle(rectangle.X + rectangle.Width / 20, rectangle.Y + rectangle.Width / 20 + rectangle.Width / 5 + rectangle.Width / 20, rectangle.Width / 5, rectangle.Width / 5),
            fontColor = fontColor2, text = discriptions, scale = scale2 / 1.5f, fontName = fontName2, mainColor = Color.Transparent, textAligment = TextAligment.Left
        };
        discription.LoadTexture(content);
        foreach (var resource in resourcesNeededToCraft)
        {
            Label res = new Label(Manager)
            {
                rectangle = new Rectangle(rectangle.X + rectangle.Width / 2 - rectangle.Width / 3 / 2, rectangle.Y + rectangle.Width / 20 + rectangle.Width / 5 + rectangle.Width / 5 + rectangle.Width / 20 + (rectangle.Width / 10 + rectangle.Width / 40) * labelIndex, rectangle.Width / 3, rectangle.Width / 10),
                fontColor = fontColor2, text = resource.Key + " " + resource.Value, scale = scale2, fontName = fontName2, mainColor = Color.Transparent
            };
            res.LoadTexture(content);
            Labels.Add(res);
            labelIndex++;
        }
    }

    public override void LoadTexture(ContentManager content)
    {
        base.LoadTexture(content);
    }

    public void Update(GameTime gameTime)
    {
        labelIndex = 0;
        icon.rectangle = new Rectangle(rectangle.X + rectangle.Width / 20, rectangle.Y + rectangle.Width / 20, rectangle.Width / 5, rectangle.Width / 5);
        itemName.rectangle = new Rectangle(rectangle.X + rectangle.Width / 20 + rectangle.Width / 5 + rectangle.Width / 20,
            rectangle.Y + rectangle.Width / 10, rectangle.Width / 3 * 2, rectangle.Width / 5);
        discription.rectangle = new Rectangle(rectangle.X + rectangle.Width / 20,
            rectangle.Y + rectangle.Width / 20 + rectangle.Width / 5 + rectangle.Width / 20, rectangle.Width / 5,
            rectangle.Width / 5);
        foreach (var label in Labels)
        {
            label.rectangle = new Rectangle(rectangle.X + rectangle.Width / 2 - rectangle.Width / 3 / 2,
                rectangle.Y + rectangle.Width / 20 + rectangle.Width / 5 + rectangle.Width / 5 + rectangle.Width / 20 +
                (rectangle.Width / 10 + rectangle.Width / 40) * labelIndex, rectangle.Width / 3, rectangle.Width / 10);
            labelIndex++;
        }

    }
}