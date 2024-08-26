using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using AnimatorFileCreatorAdvanced.Core.GUI;

namespace AnimatorFileCreatorAdvanced.Core
{

    public class AppManager : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Point CurentScreenResolution = new Point(1000, 600);
        AbstractGUI GUI;
        public static AppManager Instance { get; private set; }
        public AppManager()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            SetResolution(CurentScreenResolution.X, CurentScreenResolution.Y);

            Instance = this;
            GUI = new CreatingAnimationGUI();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            GUI.Initialize();
            GUI.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            GUI.Update(gameTime);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            // TODO: Add your drawing code here

            GUI.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
        public void SetResolution(int x, int y)
        {
            _graphics.PreferredBackBufferWidth = x;
            _graphics.PreferredBackBufferHeight = y;
        }

        public void FulscrreenSwitch()
        {
            _graphics.IsFullScreen = !_graphics.IsFullScreen;
        }
    }
}
