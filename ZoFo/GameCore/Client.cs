
using System.Collections.Generic;
using System.Text.Json;
using ZoFo.GameCore.GameManagers.NetworkManager;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ZoFo.GameCore
{
    public class Client
    {
        ClientNetworkManager networkManager;
        public Client()
        {
            networkManager = new ClientNetworkManager();
            networkManager.GetDataSent += OnDataSend;
        }

        public void OnDataSend(string data)
        { 
            List<IUpdateData> updateDatas = JsonSerializer.Deserialize<List<IUpdateData>>(data);
            // тут будет switch
        }

        public void GameEndedUnexpectedly(){ }

        public void JoinRoom(string ip)
        {
            networkManager.JoinRoom(ip);
        }

<<<<<<< HEAD
        public void JoinYourself(){ networkManager.JoinYourself(); }
=======
        public void JoinYourself()
        {
            networkManager.JoinYourself();
        }
>>>>>>> 2e9fccd99a44f3e5ae11ac70f5e7282ec85d9f98

        internal void Update(GameTime gameTime)
        {
        }

        internal void Draw(SpriteBatch spriteBatch)
        { 
        }
    }
}