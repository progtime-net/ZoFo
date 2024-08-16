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
using ZoFo.GameCore.GameObjects.Entities;

namespace ZoFo.GameCore
{
    public class Server
    {
        private List<GameObject> gameObjects;
        private ServerNetworkManager networkManager;
        private List<Entity> entity;  //entity
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
        /// <summary>
        /// Для красоты)   Отдел Серверов
        /// </summary>
        /// <param name="data"></param>
        public void AddData(IUpdateData data)//добавляет в лист updates новую data
        {
            networkManager.AddData(data);
        }
        public void CreateRoom(int players) //Создает комнату и запускает ожидание подключений
        {
            networkManager.StartWaitingForPlayers(players);
        }

        //  public void StartGame() { }   принудительный запуск
        public void EndGame() //Добавляет UpdateGameEnded и отключает игроков
        {
            UpdateGameEnded gameEnded = new UpdateGameEnded();
            networkManager.AddData(gameEnded);
            networkManager.CloseConnection();
        } 


        public void Update(GameTime gameTime)
        { 
        }

        public void RegisterEntity(GameObject gameObject)
        {
          gameObjects.Add(gameObject);
        } 
    }
}
