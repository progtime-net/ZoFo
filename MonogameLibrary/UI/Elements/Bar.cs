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
using Microsoft.Xna.Framework.Input;

namespace MonogameLibrary.UI.Elements;

public class Bar : DrawableUIElement
{
    public float percent = 0.5f;
    private DrawableUIElement barInside;
    public Color inColor;
    public string inTextureName = "";
    
    public Bar(UIManager manager, int layerIndex = 0, string textureName = "") : base(manager, layerIndex, textureName)
    {
    }

    public void Initialize()
    {
        barInside = new DrawableUIElement(Manager)
        {
            rectangle = new Rectangle(rectangle.X + rectangle.Height / 8, rectangle.Y + rectangle.Height / 8,
                (int)((rectangle.Width - rectangle.Height / 4) * percent), rectangle.Height / 8 * 7),
            mainColor = inColor
        };
    }

    public override void LoadTexture(ContentManager content)
    {
        barInside.LoadTexture(content);
        base.LoadTexture(content);
    }

    public void Update(GameTime gameTime, float percent)
    {
        barInside.rectangle = new Rectangle(rectangle.X + rectangle.Height / 8, rectangle.Y + rectangle.Height / 8,
            (int)((rectangle.Width - rectangle.Height / 4) * percent), rectangle.Height / 8 * 7);
    }
}