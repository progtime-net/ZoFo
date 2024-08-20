
using System.Collections.Generic;
using System.Text.Json;
using ZoFo.GameCore.GameManagers.NetworkManager;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ZoFo.GameCore.GameObjects;
using ZoFo.GameCore.GameObjects.MapObjects;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;
using System.Drawing;
using System.Reflection;
using ZoFo.GameCore.GameObjects.Entities;
using System.Net.Sockets;
using System.Net;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ClientToServer;
using ZoFo.GameCore.GameObjects.Entities.LivingEntities.Player;
using System.Linq;
using System.Web;
using ZoFo.GameCore.GUI;
using ZoFo.GameCore.GameObjects.Entities.Interactables.Collectables;
using ZoFo.GameCore.GameObjects.MapObjects.StopObjects;
using ZoFo.GameCore.Graphics;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ZoFo.GameCore.GameManagers.CollisionManager;
namespace ZoFo.GameCore
{
    public class Client
    {
        #region Network part

        ClientNetworkManager networkManager;

        public bool IsConnected { get { return networkManager.IsConnected; } }
        public IPEndPoint InfoConnect => networkManager.InfoConnect;

        public Client()
        {
            networkManager = new ClientNetworkManager();
            networkManager.GetDataSent += OnDataSend;

            // Подписка на действия инпутменеджера.
            // Отправляются данные апдейтса с обновлением инпута
            AppManager.Instance.InputManager.ActionEvent += () =>
            {
                networkManager.AddData(new UpdateInput()
                {
                    InputMovementDirection = AppManager.Instance.InputManager.InputMovementDirection,
                    InputAttackDirection = AppManager.Instance.InputManager.InputAttackDirection
                });

            };
            AppManager.Instance.InputManager.OnInteract += () =>
            {
                networkManager.AddData(new UpdateInputInteraction() { });
            };
            AppManager.Instance.InputManager.ShootEvent += () =>
            {
                networkManager.AddData(new UpdateInputShoot() { });
            };
        }

        public void OnDataSend(string data)
        {
            //List<UpdateTileCreated> updateDatas = JsonSerializer.Deserialize<List<UpdateTileCreated>>(data);
            JArray jToken = JsonConvert.DeserializeObject(data) as JArray;

            //string[] brands = jToken.SelectToken("")?.ToObject<string[]>();
            foreach (JToken update in jToken.Children())
            {
                string a = update.ToString();
                UpdateTileCreated u = System.Text.Json.JsonSerializer.Deserialize<UpdateTileCreated>(a);
            }
            // тут будет switch
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
            shakeEffect += power;
        }
        public void UpdateShaking()
        {
            shakeEffect *= 0.99f;
            (GraphicsComponent.CameraPosition) += new Microsoft.Xna.Framework.Point(
                (int)((Random.Shared.NextDouble() - 0.5) * shakeEffect),
                (int)((Random.Shared.NextDouble() - 0.5) * shakeEffect)
                );
        }

        /// <summary>
        /// Клиент должен обнговлять игру анимаций
        /// </summary>
        /// <param name="gameTime"></param>
        internal void Update(GameTime gameTime)
        {
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
                GraphicsComponent.CameraPosition =
                    ((GraphicsComponent.CameraPosition.ToVector2() *0.9f +
                    (myPlayer.position + myPlayer.graphicsComponent.ObjectDrawRectangle.Size.ToVector2() / 2 - AppManager.Instance.CurentScreenResolution.ToVector2() / (2 * GraphicsComponent.scaling)
                    ) * 0.1f
                    ) )
                .ToPoint();
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

        internal void GotData(UpdateData update)
        {
            if (update is UpdateTileCreated)
            {
                mapObjects.Add(
                new MapObject(
                    (update as UpdateTileCreated).Position,
                    (update as UpdateTileCreated).Size.GetPoint().ToVector2(),
                    (update as UpdateTileCreated).sourceRectangle.GetRectangle(),
                    (update as UpdateTileCreated).tileSetName
                    ));
            }
            else if (update is UpdateStopObjectCreated)
            {
                stopObjects.Add(
                new StopObject(
                    (update as UpdateStopObjectCreated).Position,
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
                    created_gameObject = new Player((update as UpdateGameObjectCreated).position);
                    players.Add(created_gameObject as Player);
                    myPlayer = players[0]; 
                    gameObjects.Add(created_gameObject);
                }  
                else
                {
                    Type t = Type.GetType("ZoFo.GameCore.GameObjects." + (update as UpdateGameObjectCreated).GameObjectType);
                    GameObject gameObject = Activator.CreateInstance(t, (update as UpdateGameObjectCreated).position) as GameObject;
                    if (gameObject is Entity)
                        (gameObject as Entity).SetIdByClient((update as UpdateGameObjectCreated).IdEntity);
                    gameObjects.Add(gameObject);
                } 
                (gameObjects.Last() as Entity).SetIdByClient((update as UpdateGameObjectCreated).IdEntity);

            }
            else if (update is UpdateGameOBjectWithoutIdCreated)
            {
                Type t = Type.GetType("ZoFo.GameCore.GameObjects." + (update as UpdateGameOBjectWithoutIdCreated).GameObjectClassName);
                GameObject gameObject = Activator.CreateInstance(t, (update as UpdateGameOBjectWithoutIdCreated).position) as GameObject;
                if (gameObject is Particle)
                    particles.Add(gameObject as Particle);
            } 
            else if (update is UpdatePosition)
            {
                var ent = FindEntityById(update.IdEntity);

                if (ent != null)
                    ent.position = (update as UpdatePosition).NewPosition;
            }
            else if (update is UpdateAnimation)
            {
                var ent = FindEntityById(update.IdEntity);
                if (ent != null)
                    ((ent as Entity).graphicsComponent as AnimatedGraphicsComponent).StartAnimation((update as UpdateAnimation).animationId);
                //DebugHUD.Instance.Log("new Animation " + ent.position);
            }
            else if (update is UpdateGameObjectDeleted)
            {
                var ent = FindEntityById(update.IdEntity);

                if (ent != null)
                    DeleteObject(ent);

            }
            else if (update is UpdatePlayerParametrs)
            {
                UpdatePlayerHealth(update as UpdatePlayerParametrs);
            }
            
        }
        public void UpdatePlayerHealth(UpdatePlayerParametrs update)
        {

            //check on player hp lowered

            if (myPlayer != null)
            {
                float hpMyPlayerHp = myPlayer.health;


                var entity = FindEntityById(update.IdEntity);

                if (entity != null)
                {
                    (entity as Player).health = (update as UpdatePlayerParametrs).health;
                    (entity as Player).rad = (update as UpdatePlayerParametrs).radiatoin;
                }
                if (entity.Equals(myPlayer))
                {
                    if (hpMyPlayerHp > myPlayer.health)
                    {
                        AppManager.Instance.client.AddShaking((hpMyPlayerHp - myPlayer.health));

                    }
                }

                return;
            }
            

            var ent = FindEntityById(update.IdEntity);

            if (ent != null)
            {
                (ent as Player).health = (update as UpdatePlayerParametrs).health;
                (ent as Player).rad = (update as UpdatePlayerParametrs).radiatoin;
            }
        }


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

    }
}