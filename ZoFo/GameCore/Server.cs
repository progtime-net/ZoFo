using Microsoft.Xna.Framework;
using MonogameLibrary.UI.Elements;
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
        private ServerNetworkManager networkManager;
        private int ticks = 0;
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

        /// <summary>
        /// Создает комнату и запускает ожидание подключений
        /// </summary>
        /// <param name="players"></param>
        public void CreateRoom(int players) 
        {
            networkManager.Start(players);
        }

        //  public void StartGame() { }   принудительный запуск

        /// <summary>
        /// Добавляет UpdateGameEnded и отключает игроков
        /// </summary>
        public void EndGame()
        {
            UpdateGameEnded gameEnded = new UpdateGameEnded();
            networkManager.AddData(gameEnded);
            networkManager.CloseConnection();
        }

        private List<GameObject> gameObjects = new List<GameObject>();
        private List<Entity> entities;  //entity
        public void Update(GameTime gameTime) 
        {
            if (ticks == 3) //ОБРАБАТЫВАЕТСЯ 20 РАЗ В СЕКУНДУ
            {
                foreach (var go in gameObjects)
                {
                    go.UpdateLogic(gameTime);
                }
                ticks = 0;
                networkManager.SendData();
            }
            ticks++;
        }

        

        /// <summary>
        /// Регистрирует игровой объект
        /// </summary>
        /// <param name="gameObject"></param>
        public void RegisterEntity(GameObject gameObject)
        {
          gameObjects.Add(gameObject);
        }
    }
}
