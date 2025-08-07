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
    public class ActionMaker
    {
        public static Action SetGUI(AbstractGUI abstractGUI)
        {
            return new Action(() =>
            {
                AppManager.Instance.SetGUI(abstractGUI);
            });
        }
        /// <summary>
        /// Abstract GUI in Actino is sender
        /// </summary>
        /// <param name="abstractGUI">New UI to set and get an overlay</param>
        /// <returns></returns>
        public static Action<AbstractGUI> SetGUIAndMoveOverlay(AbstractGUI abstractGUI)
        {
            return new Action<AbstractGUI>((oldOverlay) =>
            {
                abstractGUI.SetOverlay_WithoutLoading(oldOverlay);

                AppManager.Instance.SetGUI(abstractGUI);


            });
        }
        /// <summary>
        /// Abstract GUI in Actino is sender
        /// </summary>
        /// <param name="abstractGUI">New UI to set and get an overlay</param>
        /// <returns></returns>
        public static Action<AbstractGUI> InstantEndLoading(AbstractGUI abstractGUI)
        {
            return new Action<AbstractGUI>((oldOverlay) =>
            {
                if (oldOverlay is LoadingGameScreenGUI)
                    ((LoadingGameScreenGUI)oldOverlay).FinishLoading();
                abstractGUI.SetOverlay_WithoutLoading(oldOverlay);

                AppManager.Instance.SetGUI(abstractGUI);


            });
        }
    }
    public enum LoadingScreenState { Loading, Loaded, Unloading, Unloaded}
    internal class LoadingGameScreenGUI : AbstractGUI
    {
        private DrawableUIElement menuBackground;

        public LoadingGameScreenGUI()
        {

        }
        Action<AbstractGUI> OnLoadEndedAction;
        public LoadingGameScreenGUI(Action<AbstractGUI> OnLoadEndedAction)
        {
            this.OnLoadEndedAction = OnLoadEndedAction;
        }
        public LoadingGameScreenGUI(Action OnLoadEndedAction)
        {
            this.OnLoadEndedAction = new Action<AbstractGUI>((s) => { OnLoadEndedAction?.Invoke(); });
        }
        Label loadingLabel;
        LoadingScreenState loadingScreenState = LoadingScreenState.Loading;
        protected override void CreateUI()
        {
            int width = AppManager.Instance.CurentScreenResolution.X;
            int height = AppManager.Instance.CurentScreenResolution.Y;


            menuBackground = new DrawableUIElement(Manager) { rectangle = new Rectangle(0, 0, width, height), mainColor = Color.White, textureName = "Textures/GUI/background/waiting" };
            Elements.Add(menuBackground);
            menuBackground.LoadTexture(AppManager.Instance.Content);

            loadingLabel = new Label(Manager)
            {
                rectangle = new Rectangle(width / 2 - (int)(width / 8), height / 7, (int)(width / 4), (int)(height / 20)),
                text = "Loading...",
                fontColor = Color.White,
                mainColor = Color.Transparent,
                scale = 0.9f,
                fontName = "Fonts\\Font3"
            };

            Elements.Add(loadingLabel);

        }

        double dt = 0;
        double tWhenDisblingStarted; 
        float InLength = 1;
        float MiddleMinLength = 1;
        float OutLength = 1;
        bool instaUnloadAfterLoading;
        public double InFunction(double f) => Math.Sign(f) * f * f;
        public double OutFunction(double f) => Math.Sign(f) * f * f;
        public override void Update(GameTime gameTime)
        {
            dt += gameTime.ElapsedGameTime.TotalSeconds;
            StartingUpdate();
            MiddleUpdate();
            EndingUpdate();
            base.Update(gameTime);
        }
        public void StartingUpdate()
        {
            if (loadingScreenState != LoadingScreenState.Loading) return;

            float eased = (float)InFunction(dt / InLength);
            menuBackground.mainColor = new Color(eased, eased, eased, eased);
            loadingLabel.fontColor *= eased;

            if (dt > InLength  /*&& !loaded*/)
            {
                LoadEnded();
            }

        }
        public void MiddleUpdate()
        {

            loadingLabel.fontColor = new Color(new Color(
                (float)Math.Sin(dt) / 2,
                (float)Math.Sin(1.3 * dt + 9) / 2,
                (float)Math.Sin(1.8 * dt + 5)
                ), loadingLabel.fontColor .A);
            loadingLabel.text = "Loading" + new string('.', ((int)(dt * 3)) % 4);
            loadingLabel.rectangle.Y = (int)(Math.Sin(dt) * (AppManager.Instance.SettingsManager.Resolution.Y / 7)
                +
                (1 - Math.Sin(dt)) * (AppManager.Instance.SettingsManager.Resolution.Y * 2f / 7));

            if (instaUnloadAfterLoading && loadingScreenState == LoadingScreenState.Loaded
                && dt > InLength + MiddleMinLength)
            {
                FinishLoading();
            }
        }
        public void EndingUpdate()
        {
            if (loadingScreenState != LoadingScreenState.Unloading) return; 

            float colorgrade = (float)OutFunction(OutLength - (dt - tWhenDisblingStarted)) / OutLength;
            menuBackground.mainColor = new Color(colorgrade, colorgrade, colorgrade, colorgrade);

            loadingLabel.fontColor *= colorgrade; 
            if (colorgrade < 0)
            {
                FinishingEnded();
            }
        }

        public void LoadEnded()
        {
            loadingScreenState = LoadingScreenState.Loaded; 
            OnLoadEndedAction?.Invoke(this);
        }
        public void FinishLoading(int deltaMove = 0, bool instantDisable = false)
        {

            if (loadingScreenState is LoadingScreenState.Loading)
            {
                instaUnloadAfterLoading = true;
                return;
            }

            tWhenDisblingStarted = dt + deltaMove;
            if (instantDisable) tWhenDisblingStarted += InLength;
            loadingScreenState = LoadingScreenState.Unloading;
        }

        public void FinishingEnded()
        {
            loadingScreenState = LoadingScreenState.Unloaded; 
            DeleteOverlayInParent();
        }
    }
}
