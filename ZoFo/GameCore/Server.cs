using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers.NetworkManager;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;
using ZoFo.GameCore.GameObjects;

namespace ZoFo.GameCore
{
    public class Server
    {
        private List<GameObject> gameObjects;
        private ServerNetworkManager networkManager;
        //  private List<> entity;  //entity
        public Server()
        {
            networkManager = new ServerNetworkManager();
            networkManager.GetDataSend += OnDataSend;
        }

        public void OnDataSend(string data)
        {
            List<IUpdateData> updateDatas = JsonSerializer.Deserialize<List<IUpdateData>>(data);
            //ТУТ Switch case будет честное слово
        }
        public void CreateRoom(int players) {
            networkManager.StartWaitingForPlayers(players);
        }
    //  public void StartGame() { }   принудительный запуск
        public void EndGame() {
            UpdateGameEnded gameEnded = new UpdateGameEnded();
            networkManager.AddData(gameEnded);
        }

        internal void Update(GameTime gameTime)
        { 
        }

        public void RegisterEntity(GameObject gameObject)
        {
          gameObjects.Add(gameObject);
        }
    }
}
