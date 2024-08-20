using Microsoft.Xna.Framework;
using MonogameLibrary.UI.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GameManagers.CollisionManager;
using ZoFo.GameCore.GameManagers.MapManager;
using ZoFo.GameCore.GameManagers.NetworkManager;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ClientToServer;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;
using ZoFo.GameCore.GameObjects;
using ZoFo.GameCore.GameObjects.Entities;
using ZoFo.GameCore.GameObjects.Entities.Interactables.Collectables; 
using ZoFo.GameCore.GameObjects.Entities.LivingEntities.Player;
using ZoFo.GameCore.GameObjects.MapObjects;
using ZoFo.GameCore.GameObjects.MapObjects.StopObjects;
using ZoFo.GameCore.Graphics;
using ZoFo.GameCore.GameManagers.NetworkManager.SerializableDTO;
using ZoFo.GameCore.GUI;

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
            collisionManager = new CollisionManager();

        }
        #region server logic as App

        #region Net Methods
        //TODO Comment pls
        public void OnDataSend(string data)
        {
            List<UpdateData> updateDatas = JsonSerializer.Deserialize<List<UpdateData>>(data);
            AppManager.Instance.debugHud.Log(data);
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
                case "UpdateInput":
                    if (players.Count > 0)
                        players[0].HandleNewInput(updateData as UpdateInput);//TODO id instead of 0
                    else
                        DebugHUD.DebugLog("NO PLAYER ON MAP");
                    break;
                case "UpdateTileCreated":
                    break;
                case "UpdateInputInteraction":
                    players[0].HandleInteract(updateData as UpdateInputInteraction);
                    break;
                case "UpdateInputShoot":
                    players[0].HandleShoot(updateData as UpdateInputShoot);
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

        #endregion

        #region Game Methods
        public CollisionManager collisionManager;
        /// <summary>
        /// Запуск игры в комнате
        /// </summary>
        public void StartGame()
        {

            //TODO начинает рассылку и обмен пакетами игры
            //Грузит карту
            collisionManager = new CollisionManager();
            gameObjects = new List<GameObject>();
            entities = new List<Entity>();
            players = new List<Player>();
            new MapManager().LoadMap();

            //AppManager.Instance.server.RegisterGameObject(new EntittyForAnimationTests(new Vector2(0, 0)));
            AppManager.Instance.server.RegisterGameObject(new Player(new Vector2(740, 140)));
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                    AppManager.Instance.server.RegisterGameObject(new Zombie(new Vector2(1300 + i*70, 1000+j*70)));

            AppManager.Instance.server.RegisterGameObject(new Ammo(new Vector2(140, 440)));
            AppManager.Instance.server.RegisterGameObject(new Ammo(new Vector2(240, 440)));
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

        public List<GameObject> gameObjects;
        public List<Entity> entities;  //entity
        public List<Player> players;
        public void Update(GameTime gameTime)
        {
            if (ticks == 3) //ОБРАБАТЫВАЕТСЯ 20 РАЗ В СЕКУНДУ
            {
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    gameObjects[i].UpdateLogic();
                }
                collisionManager.ResolvePhysics();
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
            if (gameObject is StopObject)
            {
                AddData(new UpdateStopObjectCreated()
                {
                    Position = (gameObject as StopObject).position,
                    sourceRectangle = new SerializableRectangle((gameObject as StopObject).sourceRectangle),
                    Size = new SerializablePoint((gameObject as StopObject).graphicsComponent.ObjectDrawRectangle.Size),
                    tileSetName = ((gameObject as StopObject).graphicsComponent as StaticGraphicsComponent)._textureName,
                    collisions = (gameObject as StopObject).collisionComponents.Select(x => new SerializableRectangle(x.stopRectangle)).ToArray()
                });//TODO 
                foreach (var col in (gameObject as StopObject).collisionComponents)
                {
                    collisionManager.Register(col);
                }
                return;
            }
            if (gameObject is MapObject)
            {
                AddData(new UpdateTileCreated()
                {
                    Position = (gameObject as MapObject).position,
                    sourceRectangle = new SerializableRectangle((gameObject as MapObject).sourceRectangle),
                    Size = new SerializablePoint((gameObject as MapObject).graphicsComponent.ObjectDrawRectangle.Size),
                    tileSetName = ((gameObject as MapObject).graphicsComponent as StaticGraphicsComponent)._textureName
                });
                return;
            }
            if (gameObject is Entity entity)
            {
                AddData(new UpdateGameObjectCreated()
                {
                    GameObjectType = gameObject.GetType().Name,
                    IdEntity = entity.Id,
                    position = gameObject.position
                });
                collisionManager.Register(entity.collisionComponent);
                entities.Add(entity);
            }
            else
                AddData(new UpdateGameObjectCreated()
                {
                    GameObjectType = gameObject.GetType().Name,
                    position = gameObject.position
                });


            if (gameObject is Player)
                players.Add(gameObject as Player);
            ////var elems = gameObject.GetType().GetProperties(System.Reflection.BindingFlags.Public);
            ////if (elems.Count()>0) TODO
            ////{ 
            ////    AppManager.Instance.server.collisionManager.Register((elems.First().GetValue(gameObject) as CollisionComponent));
            ////}

        }

        /// <summary>
        /// Удаляет игровой объект
        /// </summary>
        /// <param name="gameObject"></param>
        public void DeleteObject(Entity entity)
        {
            if (gameObjects.Contains(entity))
                gameObjects.Remove(entity);
            if (entities.Contains(entity))
                entities.Remove(entity);
            if (players.Contains(entity))
                players.Remove(entity as Player);
            AddData(new UpdateGameObjectDeleted()
            { GameObjectType = entity.GetType().Name, IdEntity = entity.Id }
            );
            collisionManager.Deregister(entity.collisionComponent);
        }
    }

    #endregion

    #endregion
}
