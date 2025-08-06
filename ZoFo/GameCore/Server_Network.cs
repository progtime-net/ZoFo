using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GameManagers.NetworkManager;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ClientToServer;
using ZoFo.GameCore.GUI;

namespace ZoFo.GameCore
{
    public partial class Server
    {

        #region Net Methods


        private ServerNetworkManager networkManager;
        private int ticks = 0;
        public IPEndPoint MyIp { get { return networkManager.endPoint; } }

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
        internal void UpdatesList(List<UpdateData> updates)
        {
            foreach (var item in updates)
            {
                ProcessIUpdateData(item);
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
                case "UpdateInput":
                    if (players.Count > 0)
                    {
                        UpdateInput data = updateData as UpdateInput;
                        if (data.PlayerId > 0)
                        {
                            players[data.PlayerId - 1].HandleNewInput(data);
                        }
                    }
                    //TODO id instead of 0
                    else
                        DebugHUD.DebugLog("NO PLAYER ON MAP");
                    break;
                case "UpdateTileCreated":
                    break;
                case "UpdateInputInteraction":
                    if (players.Count > 0)
                    {
                        UpdateInputInteraction data = updateData as UpdateInputInteraction;
                        players[data.PlayerId - 1].HandleInteract(data);
                    }
                    break;
                case "UpdateInputShoot":
                    if (players.Count > 0)
                    {
                        UpdateInputShoot data = updateData as UpdateInputShoot;
                        players[data.PlayerId - 1].HandleShoot(data);
                    }
                    break;
            }
        }//Поспать


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
        public void CreateRoom(bool isMultiplayer)
        {
            networkManager.SetIsMultiplayer(isMultiplayer);
            networkManager.Start();
        }

        #endregion
    }
}
