
using System.Collections.Generic;
using System.Text.Json;
using ZoFo.GameCore.GameManagers.NetworkManager;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates;

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
            // Тут будет switch
        }

        public void GameEndedUnexpectedly(){ }

        public void JoinRoom(){ }

        public void JoinYourself(){ }
    }
}