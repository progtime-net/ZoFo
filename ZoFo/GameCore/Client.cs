
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
using ZoFo.GameCore.GameObjects.MapObjects.Tiles;
using System.Drawing;

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
            List<IUpdateData> updateDatas = JsonSerializer.Deserialize<List<IUpdateData>>(data);
            // тут будет switch
            foreach (var item in updateDatas)
            {
              /*  switch (item.UpdateType)    Здесь нужно отлавливать и регистрировать
                {
                    case "Tile":
                        MapObject map = new MapObject();

                        break;
                }*/
            }

        }
        public void GameEndedUnexpectedly() { }
        public void JoinRoom(string ip)
        {
            networkManager.JoinRoom(ip);
        }
        public void JoinYourself() { networkManager.JoinYourself(); }


        List<MapObject> mapObjects = new List<MapObject>();
        /// <summary>
        /// Клиент должен обнговлять игру анимаций
        /// </summary>
        /// <param name="gameTime"></param>
        internal void Update(GameTime gameTime)
        {
        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < mapObjects.Count; i++)
            {
                mapObjects[i].Draw(spriteBatch);
            }
        }

        internal void GotData(IUpdateData update)
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
        }
    }
}