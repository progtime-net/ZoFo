
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
using ZoFo.GameCore.GameObjects.Entities.LivingEntities.Enemies;
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
            AppManager.Instance.InputManager.ActionEvent += () => networkManager.AddData(new UpdateInput(){
                InputMovementDirection = AppManager.Instance.InputManager.InputMovementDirection,
                InputAttackDirection = AppManager.Instance.InputManager.InputAttackDirection
            });
        }

        public void OnDataSend(string data)
        {
            List<UpdateData> updateDatas = JsonSerializer.Deserialize<List<UpdateData>>(data);
            // тут будет switch
            AppManager.Instance.debugHud.Log(data);
            foreach (var item in updateDatas)
            {
                GotData(item);
            }

        }
        public void GameEndedUnexpectedly() { }

        public void JoinRoom(string ip, int port)
        {
            networkManager.JoinRoom(ip, port);
        }
        public void JoinYourself(int port) { networkManager.JoinYourself(port); }

        #endregion

        List<MapObject> mapObjects = new List<MapObject>();
        List<GameObject> gameObjects = new List<GameObject>();
        List<StopObject> stopObjects = new List<StopObject>();
        /// <summary>
        /// Клиент должен обнговлять игру анимаций
        /// </summary>
        /// <param name="gameTime"></param>
        internal void Update(GameTime gameTime)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                AppManager.Instance.debugHud.Set("GameTime", gameTime.TotalGameTime.ToString());
                gameObjects[i].UpdateAnimations();
            }
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
        }

        internal void GotData(UpdateData update)
        {
            if (update is UpdateTileCreated) 
            {
                mapObjects.Add(
                new MapObject(
                    (update as UpdateTileCreated).Position,
                    (update as UpdateTileCreated).Size.ToVector2(),
                    (update as UpdateTileCreated).sourceRectangle,
                    (update as UpdateTileCreated).tileSetName
                    ));
            }
            else if (update is UpdateStopObjectCreated) 
            {
                stopObjects.Add(
                new StopObject(
                    (update as UpdateStopObjectCreated).Position,
                    (update as UpdateStopObjectCreated).Size.ToVector2(),
                    (update as UpdateStopObjectCreated).sourceRectangle,
                    (update as UpdateStopObjectCreated).tileSetName,
                    (update as UpdateStopObjectCreated).collisions
                    ));
            }
            else if (update is UpdateGameObjectCreated)
            {
                if ((update as UpdateGameObjectCreated).GameObjectType == "EntittyForAnimationTests")
                    gameObjects.Add(new EntittyForAnimationTests((update as UpdateGameObjectCreated).position));
                if ((update as UpdateGameObjectCreated).GameObjectType == "Player")
                    gameObjects.Add(new Player((update as UpdateGameObjectCreated).position));
                if ((update as UpdateGameObjectCreated).GameObjectType == "Ammo")
                    gameObjects.Add(new Ammo((update as UpdateGameObjectCreated).position))                if ((update as UpdateGameObjectCreated).GameObjectType == "Zombie")
                    gameObjects.Add(new Zombie((update as UpdateGameObjectCreated).position));


                (gameObjects.Last() as Entity).SetIdByClient((update as UpdateGameObjectCreated).IdEntity);
                //var a = Assembly.GetAssembly(typeof(GameObject));
                //gameObjects.Add( TODO reflection
                //Activator.CreateInstance(Type.GetType("ZoFo.GameCore.GameObjects.Entities.EntittyForAnimationTests")
                ///*(update as UpdateGameObjectCreated).GameObjectType*/, new []{ new Vector2(100, 100) })
                //as GameObject
                //);
            }
            else if (update is UpdatePosition)
            {
                var ent = FindEntityById(update.IdEntity);

                ent.position = (update as UpdatePosition).NewPosition;
                DebugHUD.Instance.Log("newPosition " + ent.position);
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

    }
}