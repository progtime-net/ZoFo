
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

namespace ZoFo.GameCore
{
    public class Client
    {
        ClientNetworkManager networkManager;

        public bool IsConnected { get { return networkManager.IsConnected; } }
        public Client()
        {
            networkManager = new ClientNetworkManager();
            networkManager.GetDataSent += OnDataSend;
        }

        public void OnDataSend(string data)
        {
            List<UpdateData> updateDatas = JsonSerializer.Deserialize<List<UpdateData>>(data);
            // тут будет switch
            foreach (var item in updateDatas)
            {
                GotData(item);
            }

        }
        public void GameEndedUnexpectedly() { }
        public void JoinRoom(string ip,int port)
        {
            networkManager.JoinRoom(ip,port);
        }
        public void JoinYourself(int port) { networkManager.JoinYourself(port); }


        List<MapObject> mapObjects = new List<MapObject>();
        List<GameObject> gameObjects = new List<GameObject>();
        /// <summary>
        /// Клиент должен обнговлять игру анимаций
        /// </summary>
        /// <param name="gameTime"></param>
        internal void Update(GameTime gameTime)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].UpdateAnimations();
            }
        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < mapObjects.Count; i++)
            {
                mapObjects[i].Draw(spriteBatch);
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
            else if (update is UpdateGameObjectCreated)
            {
                var a = Assembly.GetAssembly(typeof(GameObject));
                if ((update as UpdateGameObjectCreated).GameObjectType == "EntittyForAnimationTests")
                {

                    gameObjects.Add(
                        new EntittyForAnimationTests(new Vector2(100,100))
                    );
                }
                //gameObjects.Add( TODO reflection
                //Activator.CreateInstance(Type.GetType("ZoFo.GameCore.GameObjects.Entities.EntittyForAnimationTests")
                ///*(update as UpdateGameObjectCreated).GameObjectType*/, new []{ new Vector2(100, 100) })
                //as GameObject
                //);
            }
        }
    }
}