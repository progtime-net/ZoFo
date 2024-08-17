using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GameManagers.MapManager;
using ZoFo.GameCore.GameManagers.NetworkManager;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;
using ZoFo.GameCore.GameObjects;
using ZoFo.GameCore.GameObjects.Entities;
using ZoFo.GameCore.GameObjects.MapObjects;

namespace ZoFo.GameCore
{
    public class Server
    {
        private ServerNetworkManager networkManager;
        private int ticks = 0;
        public IPEndPoint MyIp { get { return networkManager.InfoConnect; } }
        public Server()
        {
            networkManager = new ServerNetworkManager();
            networkManager.GetDataSend += OnDataSend;

        }
        #region server logic as App
        //TODO Comment pls
        public void OnDataSend(string data)
        {
            List<UpdateData> updateDatas = JsonSerializer.Deserialize<List<UpdateData>>(data);
            for (int i = 0; i < updateDatas.Count; i++)
            {
                ProcessIUpdateData(updateDatas[i]);
            }
        }
        /// <summary>
        /// Обработка апдейтсов, которые нам прислал клиент
        /// </summary>
        /// <param name="updateData"></param>
        public void ProcessIUpdateData(UpdateData updateData)
        {

            //ТУТ Switch case будет честное слово
            switch (updateData.UpdateType)
            {
                case "UpdateAnimation":
                    break;
                case "UpdateEntityHealth":
                    break;
                case "UpdateGameEnded":
                    break;
                case "UpdateGameObjectCreated":
                    break;
                case "UpdateGameObjectDeleted":
                    break;
                case "UpdateInteraction":
                    break;
                case "UpdateInteractionReady":
                    break;
                case "UpdateLoot":
                    break;
                case "UpdatePlayerParametrs":
                    break;
                case "UpdatePosition":
                    break;
                case "UpdateTileCreated":
                    break;

            }
        }

        public void CloseConnection()
        {
            networkManager.CloseConnection();
        }

        /// <summary>
        /// Для красоты)   Отдел Серверов 
        /// добавляет в лист updates новую data
        /// </summary>
        /// <param name="data"></param>
        public void AddData(UpdateData data)//добавляет в лист updates новую data
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

        /// <summary>
        /// Запуск игры в комнате
        /// </summary>
        public void StartGame()
        {

            //TODO начинает рассылку и обмен пакетами игры
            //Грузит карту

            gameObjects = new List<GameObject>();
            entities = new List<Entity>();
            new MapManager().LoadMap();

            AppManager.Instance.server.RegisterGameObject(new EntittyForAnimationTests(new Vector2(40, 40)));
        }

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
        public void RegisterGameObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
            if (gameObject is MapObject)
            {
                AddData(new UpdateTileCreated()
                {
                    Position = (gameObject as MapObject).position,
                    sourceRectangle = (gameObject as MapObject).sourceRectangle,
                    Size = (gameObject as MapObject).graphicsComponent.ObjectDrawRectangle.Size,
                    tileSetName = (gameObject as MapObject).graphicsComponent.mainTextureName
                });//TODO 
                return;
            }

            AddData(new UpdateGameObjectCreated()
            { GameObjectType = gameObject.GetType().Name }
            );

        }
    }
    #endregion
}
