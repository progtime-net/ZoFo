
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GameManagers.CollisionManager;
using ZoFo.GameCore.GameManagers.NetworkManager;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ClientToServer;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;
using ZoFo.GameCore.GameObjects;
using ZoFo.GameCore.GameObjects.MapObjects.StopObjects;
using ZoFo.GameCore.Graphics;
using ZoFo.GameCore.GUI;
namespace ZoFo.GameCore
{
    public class Client
    {


        public Client()
        {
            networkManager = new ClientNetworkManager();

            // Подписка на действия инпутменеджера.
            // Отправляются данные апдейтса с обновлением инпута
            AppManager.Instance.InputManager.ActionEvent += () =>
            {
                if (AppManager.Instance.client.networkManager.PlayerId > 0)
                {
                    networkManager.AddData(new UpdateInput()
                    {
                        InputMovementDirection = AppManager.Instance.InputManager.InputMovementDirection.Serialize(),
                        InputAttackDirection = AppManager.Instance.InputManager.InputAttackDirection.Serialize(),
                        PlayerId = AppManager.Instance.client.networkManager.PlayerId
                    });
                }

            };
            AppManager.Instance.InputManager.OnInteract += () =>
            {
                if (AppManager.Instance.client.networkManager.PlayerId > 0)
                    networkManager.AddData(new UpdateInputInteraction() { PlayerId = AppManager.Instance.client.networkManager.PlayerId });

            };
            AppManager.Instance.InputManager.ActiveActionEvent += () =>
            {
                if (AppManager.Instance.client.networkManager.PlayerId > 0)
                    networkManager.AddData(new UpdateInputActiveAction() { PlayerId = AppManager.Instance.client.networkManager.PlayerId });

            };
        }

        #region Network part

        ClientNetworkManager networkManager;

        public bool IsConnected { get { return networkManager.IsConnected; } }
        public IPEndPoint InfoConnect => networkManager.InfoConnect;


        public void OnDataSend(string data)
        {
            JArray jToken = JsonConvert.DeserializeObject(data) as JArray;

            foreach (JToken update in jToken.Children())
            {
                string a = update.ToString();
                UpdateTileCreated u = System.Text.Json.JsonSerializer.Deserialize<UpdateTileCreated>(a);
            }
            AppManager.Instance.debugHud.Log(data);
            //foreach (var item in updateDatas)
            //{
            //    GotData(item);
            //}

        }
        public void GameEndedUnexpectedly() { }

        public void JoinRoom(string ip, int port)
        {
            networkManager.JoinRoom(ip, port);
        }
        public void JoinYourself(int port) { networkManager.JoinYourself(port); }

        #endregion

        public Player myPlayer;
        List<MapObject> mapObjects = new List<MapObject>();
        List<GameObject> gameObjects = new List<GameObject>();
        List<Player> players = new List<Player>();
        List<StopObject> stopObjects = new List<StopObject>();
        List<Particle> particles = new List<Particle>();

        float shakeEffect = 0;
        public void AddShaking(float power)
        {
            shakeEffect += power * 3;
        }
        public void UpdateShaking()
        {
            shakeEffect *= 0.99f;
            (GraphicsComponent.CameraPosition) += new Microsoft.Xna.Framework.Point(
                (int)((Random.Shared.NextDouble() - 0.5) * shakeEffect),
                (int)((Random.Shared.NextDouble() - 0.5) * shakeEffect)
                );
        }

        int tick = 0;
        /// <summary>
        /// Клиент должен обнговлять игру анимаций
        /// </summary>
        /// <param name="gameTime"></param>
        internal void Update(GameTime gameTime)
        {
            tick++;
            if (tick < 3)
                return;
            tick = 0;

            UpdateShaking();
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].UpdateAnimations();
            }
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].UpdateAnimations();
            }

            networkManager.SendData();//set to ticks
            if (myPlayer != null)
                SetCameraPositionToGameObject(myPlayer);
        }
        /// <summary>
        /// set's camera pos relative to the gameObject
        /// </summary>
        /// <param name="position"></param>
        /// <param name="gameObject"></param>
        public void SetCameraPositionToGameObject(GameObject gameObject)
        {
            float lerp_value = 0.04f;

            GraphicsComponent.CameraPosition =
                    (GraphicsComponent.CameraPosition.ToVector2()
                    * (1 - lerp_value)
                    +
                    (gameObject.position + gameObject.graphicsComponent.ObjectDrawRectangle.Size.ToVector2() / 2 - AppManager.Instance.CurentScreenResolution.ToVector2() / (2 * GraphicsComponent.scaling))
                    * lerp_value
                    )
                .ToPoint();

            DebugHUD.AddGOData(1, "ClientGotData");

        }
        public void SendData()
        {
            networkManager.SendData();
        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < mapObjects.Count; i++)
            {
                mapObjects[i].Draw(spriteBatch);
            }
            for (int i = 0; i < stopObjects.Count; i++)
            {
                stopObjects[i].Draw(spriteBatch);
            }
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].Draw(spriteBatch);
            }
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Draw(spriteBatch);
            }

        }

        internal void UpdatesList(List<UpdateData> updates)
        {
            foreach (var item in updates)
            {
                GotData(item);
            }
        }
        internal void GotData(UpdateData update)
        {
            DebugHUD.AddAdditionalDataToGraph(1, "ClientGotData");
            #region Engine Important
            if (update is UpdateTileCreated)
            {
                mapObjects.Add(
                new MapObject(
                    (update as UpdateTileCreated).Position.GetVector2(),
                    (update as UpdateTileCreated).Size.GetPoint().ToVector2(),
                    (update as UpdateTileCreated).sourceRectangle.GetRectangle(),
                    (update as UpdateTileCreated).tileSetName
                    ));
            }
            else if (update is UpdateStopObjectCreated)
            {
                stopObjects.Add(
                new StopObject(
                    (update as UpdateStopObjectCreated).Position.GetVector2(),
                    (update as UpdateStopObjectCreated).Size.GetPoint().ToVector2(),
                    (update as UpdateStopObjectCreated).sourceRectangle.GetRectangle(),
                    (update as UpdateStopObjectCreated).tileSetName,
                    (update as UpdateStopObjectCreated).collisions.Select(x => x.GetRectangle()).ToArray()
                    ));
            }

            else if (update is UpdateGameObjectCreated)
            {

                Entity created_gameObject;
                if ((update as UpdateGameObjectCreated).GameObjectType == "Player")
                {
                    created_gameObject = new Player((update as UpdateGameObjectCreated).position.GetVector2());
                    gameObjects.Add(created_gameObject);
                }
                else
                {
                    Type t = Type.GetType("ZoFo.GameCore.GameObjects." + (update as UpdateGameObjectCreated).GameObjectType);
                    GameObject gameObject = Activator.CreateInstance(t, (update as UpdateGameObjectCreated).position.GetVector2()) as GameObject;
                    if (gameObject is Entity)
                        (gameObject as Entity).SetIdByClient((update as UpdateGameObjectCreated).IdEntity);
                    gameObjects.Add(gameObject);
                }
                (gameObjects.Last() as Entity).SetIdByClient((update as UpdateGameObjectCreated).IdEntity);

            }
            else if (update is UpdateGameObjectWithoutIdCreated)
            {
                Type t = Type.GetType("ZoFo.GameCore.GameObjects." + (update as UpdateGameObjectWithoutIdCreated).GameObjectClassName);
                GameObject gameObject = Activator.CreateInstance(t, (update as UpdateGameObjectWithoutIdCreated).position.GetVector2()) as GameObject;
                if (gameObject is Particle)
                    particles.Add(gameObject as Particle);
            }
            else if (update is UpdatePosition)
            {
                var ent = FindEntityById(update.IdEntity);

                if (ent != null)
                    ent.position = (update as UpdatePosition).NewPosition.GetVector2();
            }
            else if (update is UpdateAnimation)
            {
                var ent = FindEntityById(update.IdEntity);
                if (ent != null)
                    ((ent as Entity).graphicsComponent as AnimatedGraphicsComponent).StartAnimation((update as UpdateAnimation).animationId);
            }
            else if (update is UpdateGameObjectDeleted)
            {
                var ent = FindEntityById(update.IdEntity);

                if (ent != null)
                    DeleteObject(ent);

            }
            else if (update is UpdateGameEnded)
            {
                GameEnd();
            }
            #endregion
            else if (update is UpdatePlayerParametrs && myPlayer != null && update.IdEntity == myPlayer.Id) //aaa 
            {
                UpdatePlayerParametrs(update as UpdatePlayerParametrs);
            }
            else if (update is UpdateCreatePlayer)
            {
                UpdateCreatePlayer ucp = (UpdateCreatePlayer)update;
                if (networkManager.PlayerId == ucp.PlayerId)
                {
                    myPlayer = (Player)FindEntityById(ucp.IdEntity);

                    GraphicsComponent.CameraPosition =
                        (myPlayer.position + myPlayer.graphicsComponent.ObjectDrawRectangle.Size.ToVector2() / 2 - AppManager.Instance.CurentScreenResolution.ToVector2() / (2 * GraphicsComponent.scaling)
                        ).ToPoint();

                }
            }

        }
        /// <summary>
        /// Тут данные о штуках типо здоровья, энергии, золота будет
        /// </summary>
        /// <param name="update"></param>
        public void UpdatePlayerParametrs(UpdatePlayerParametrs update)
        {
            if (myPlayer == null)
                return;


        }
        public bool changeGUI = false;
        public void GameEnd()
        {
            changeGUI = true;
        }

        #region Engine Important
        public Entity FindEntityById(int id)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (gameObjects[i] is Entity)
                {
                    if ((gameObjects[i] as Entity).Id == id)
                    {
                        return gameObjects[i] as Entity;
                    }
                }
            }
            return null;
        }

        public void DeleteObject(GameObject gameObject)
        {
            if (gameObject is Entity)
            {
                DeleteEntity(gameObject as Entity);
            }
            else if (gameObject is Particle)
            {
                if (particles.Contains(gameObject))
                    particles.Remove(gameObject as Particle);
            }
        }
        public void DeleteEntity(Entity entity)
        {

            if (gameObjects.Contains(entity))
                gameObjects.Remove(entity);
            //if (entities.Contains(entity))
            //    entities.Remove(entity);
            if (players.Contains(entity))
                players.Remove(entity as Player);
        }
        #endregion
    }
}