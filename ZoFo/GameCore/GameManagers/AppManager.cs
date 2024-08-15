using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ZoFo.GameCore.GUI;
using static System.Collections.Specialized.BitVector32;
using MonogameLibrary.UI.Base;

namespace ZoFo.GameCore.GameManagers
{
    public enum GameState { NotPlaying, HostPlaying, ClientPlaying }
    public class AppManager : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        
        
        public static AppManager Instance { get; private set; }
        public GameState gamestate;
        public AbstractGUI currentGUI;
        public DebugHUD debugHud;
        public Point CurentScreenResolution = new Point(1920, 1080);
        //public Client client;
        //public Server server;

            
        #region Managers
        
        public InputManager InputManager;

        #endregion

        public AppManager()
        {
            _graphics = new GraphicsDeviceManager(this);
            SetResolution(CurentScreenResolution.X, CurentScreenResolution.Y);
            FulscrreenSwitch();
            
            
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Instance = this;
            InputManager = new InputManager();
            
            

            currentGUI = new MainMenuGUI();
            debugHud = new DebugHUD();

        }

        protected override void Initialize()
        {
            currentGUI.Initialize();
            debugHud.Initialize();
            


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            debugHud.LoadContent();
            currentGUI.LoadContent();



        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            debugHud.Set("key", "value");
            
            InputManager.Update();
            currentGUI.Update(gameTime);
            switch (gamestate)
            {
                case GameState.NotPlaying:
                    break;
                case GameState.HostPlaying:
                    //server.Update(GameTime gameTime);
                    //client.Update(GameTime gameTime);
                    break;
                case GameState.ClientPlaying:
                    //server.Update(GameTime gameTime);
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            

            debugHud.Draw(_spriteBatch);
            currentGUI.Draw(_spriteBatch);
            switch (gamestate)
            {
                case GameState.ClientPlaying:
                case GameState.HostPlaying:
                    //client.Draw(_spriteBatch); 
                    break;
                case GameState.NotPlaying:
                default:
                    break;
            }

            base.Draw(gameTime);
        }
        public void ChangeState(GameState gameState)
        {
            this.gamestate = gameState;
        }
        public void SetGUI(AbstractGUI gui)
        {
            currentGUI = gui;

            //TODO
        }

        public void GameEnded(Dictionary<string, int> lootIGot)
        {
            //TODO
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
