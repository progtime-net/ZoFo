using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using DangerousD.GameCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ZoFo.GameCore.GameManagers.ItemManager;
using ZoFo.GameCore.GUI;
using static System.Collections.Specialized.BitVector32;

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
        public Point CurentScreenResolution = new Point(1920, 1080);
        public Client client;
        public Server server;

            
        #region Managers
        
        public InputManager InputManager;
        public ItemManager.ItemManager ItemManager;

        public AnimationBuilder animationBuilder{get;set; }

        #endregion

        public AppManager()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Instance = this;
            InputManager = new InputManager();

            currentGUI = new MainMenuGUI();

        }

        protected override void Initialize()
        {
            currentGUI.Initialize();
            


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);




        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputManager.Update();
            currentGUI.Update(gameTime);
            switch (gamestate)
            {
                case GameState.NotPlaying:
                    break;
                case GameState.HostPlaying:
                    server.Update(gameTime);
                    client.Update(gameTime);
                    break;
                case GameState.ClientPlaying:
                    server.Update(gameTime);
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            currentGUI.Draw(_spriteBatch);
            switch (gamestate)
            {
                case GameState.ClientPlaying:
                case GameState.HostPlaying:
                    client.Draw(_spriteBatch); 
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
        }

        public void GameEnded(Dictionary<string, int> lootIGot)
        {
            //TODO
        }
    }
}
