using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ZoFo.GameCore.GameManagers.ItemManager;
using ZoFo.GameCore.GUI;
using static System.Collections.Specialized.BitVector32;
using MonogameLibrary.UI.Base;
using ZoFo.GameCore.GameManagers.AssetsManager;
using ZoFo.GameCore.GameObjects;

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
        public Point CurentScreenResolution;
        public Client client;
        public Server server;
        public PlayerData playerData;


        #region Managers

        public InputManager InputManager;
        public ItemManager.ItemManager ItemManager;
        public SettingsManager SettingsManager;
        public SoundManager SoundManager;
        public AssetManager AssetManager;

        public AnimationBuilder animationBuilder { get; set; }

        #endregion

        public AppManager()
        {
            _graphics = new GraphicsDeviceManager(this);    
            CurentScreenResolution = new Point(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            SetResolution(CurentScreenResolution.X, CurentScreenResolution.Y);
            //FulscrreenSwitch();


            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            server = new Server();
            playerData = new PlayerData();
            ItemManager = new ItemManager.ItemManager();
            Instance = this;
            InputManager = new InputManager();
            SettingsManager = new SettingsManager();
            SettingsManager.LoadSettings();
            SoundManager = new SoundManager();
            AssetManager = new AssetManager();
            SoundManager.LoadSounds();


            currentGUI = new MainMenuGUI();
            debugHud = new DebugHUD();
            IsMouseVisible = false;

        }

        protected override void Initialize()
        {
            currentGUI.Initialize();
            
            debugHud.Initialize(); 
            ItemManager.Initialize(); 


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            debugHud.LoadContent();
            currentGUI.LoadContent(); 
            ItemManager.LoadItemTextures();


 
            animationBuilder = new AnimationBuilder();
            animationBuilder.LoadAnimations();
            GameObject.debugTexture = new Texture2D(GraphicsDevice, 1, 1);
            GameObject.debugTexture.SetData(new Color[] { Color.White }); 
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape)) { server?.CloseConnection(); Exit(); }


         //   debugHud.Set("key", "value");

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
                    client.Update(gameTime);
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
 
            
            // Pointwrap
            _spriteBatch.Begin(samplerState: SamplerState.PointWrap); 
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
            
            _spriteBatch.End(); 
            currentGUI.Draw(_spriteBatch);
            debugHud.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
        public void ChangeState(GameState gameState)
        {
            this.gamestate = gameState;
        }
        public void SetGUI(AbstractGUI gui)
        {
            currentGUI = gui;
            currentGUI.Initialize();
            currentGUI.LoadContent();

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

        public void SetServer(Server server) { this.server = server; }
        public void SetClient(Client client) { this.client = client; }
    }
}
