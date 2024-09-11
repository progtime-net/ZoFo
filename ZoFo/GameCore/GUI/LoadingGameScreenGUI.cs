using Microsoft.Xna.Framework;
using MonogameLibrary.UI.Base;
using MonogameLibrary.UI.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ZoFo.GameCore.GameManagers;

namespace ZoFo.GameCore.GUI
{
    internal class LoadingGameScreenGUI : AbstractGUI
    {
        private DrawableUIElement menuBackground;
        protected override void CreateUI()
        {
            int width = AppManager.Instance.CurentScreenResolution.X;
            int height = AppManager.Instance.CurentScreenResolution.Y;


            menuBackground = new DrawableUIElement(Manager) { rectangle = new Rectangle(0, 0, width, height), mainColor = Color.White, textureName = "Textures/GUI/background/waiting" };
            Elements.Add(menuBackground);
            menuBackground.LoadTexture(AppManager.Instance.Content);

            var loading = new Label(Manager) { rectangle = new Rectangle(width / 2 - (int)(width / 8), height / 7, (int)(width / 4), (int)(height / 20)), 
                text = "Loading...", fontColor = Color.White, mainColor = Color.Transparent, scale = 0.9f, fontName = "Fonts\\Font3" };
             
            Elements.Add(loading);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
