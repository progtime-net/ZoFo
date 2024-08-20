﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates;
using ZoFo.GameCore.GameManagers.NetworkManager.SerializableDTO;
using ZoFo.GameCore.GUI;

namespace ZoFo.GameCore.GameManagers.NetworkManager
{
    public class ServerNetworkManager
    {
        private Socket socket;
        private IPAddress ip;
        private bool isMultiplayer;
        //Player Id to Player endPoint
        private List<IPEndPoint> clientsEP;
        public IPEndPoint endPoint;
        private List<UpdateData> commonUpdates;
        private List<UpdateData> importantUpdates;
        private List<Datagramm> sendedData;
        private List<Datagramm> arrivingDataId;
        private int currentDatagrammId = 1;
        public delegate void OnDataSend(string data);
        public event OnDataSend GetDataSend;   // event
        Thread serverThread;

        public ServerNetworkManager() { Init(); }

        /// <summary>
        /// Initialize varibles and Sockets
        /// </summary>
        private void Init()
        {
            endPoint = new IPEndPoint(GetIp(), 8080);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            clientsEP = new List<IPEndPoint>();
            commonUpdates = new List<UpdateData>();
            importantUpdates = new List<UpdateData>();
            sendedData = new List<Datagramm>();
            arrivingDataId = new List<Datagramm>();

            GetDataSend += AnalyzeData;

            socket.Bind(endPoint);

        }

        /// <summary>
        /// Получает IP устройства
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetIp()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST
            var ipList = Dns.GetHostEntry(hostName).AddressList;

            var ipV4List = new List<IPAddress>();
            foreach (var ip in ipList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipV4List.Add(ip);
                }
            }
            if (ipV4List.Count > 0)
            {
                return ipV4List[ipV4List.Count - 1];
            }
            return IPAddress.Loopback;
        }
        public void SetIsMultiplayer(bool isMultiplayer)
        {
            this.isMultiplayer = isMultiplayer;
        }

        /// <summary>
        /// отправляет клиенту Data
        /// </summary>
        public void SendData()
        {
            #region Network Sending SinglePlayerFix
            //for (int i = 0; i < updates.Count; i++)
            //{

            //    AppManager.Instance.client.GotData(updates[i]);
            //}
            //updates.Clear();
            //return; //TODO TODO REMOVE TO ADD NETWORK TODO REMOVE TO ADD NETWORK TODO REMOVE TO ADD NETWORK TODO REMOVE TO ADD NETWORK
            //Что это?
            //по 10 паков за раз TODO FIXITFIXITFIXITFIXITFIXITFIXITFIXITFIXITFIXITFIXITFIXITFIXIT
            #endregion  
            if (arrivingDataId.Count != 0)
            {
                List<Datagramm> actualArrivingId = arrivingDataId;
                for (int i = 0; i < actualArrivingId.Count; i++)
                {
                    sendedData.Remove(sendedData.Find(x => x.DatagrammId == actualArrivingId[i].DatagrammId
                    && x.PlayerId == actualArrivingId[i].PlayerId));
                }
                arrivingDataId.Clear();
            }
            List<UpdateData> dataToSend;
            if (importantUpdates.Count != 0 || sendedData.Count != 0) 
            {
                dataToSend = new List<UpdateData>();
                for (int i = 0; i < 200 && i < importantUpdates.Count; i++)
                    dataToSend.Add(importantUpdates[i]);

                for (int i = 0; i < clientsEP.Count; i++)
                {
                    Datagramm impDgramm = new Datagramm();
                    impDgramm.DatagrammId = currentDatagrammId;
                    impDgramm.updateDatas = dataToSend;
                    impDgramm.isImportant = true;
                    impDgramm.PlayerId = i + 1;
                    sendedData.Add(impDgramm);
                    foreach (Datagramm Dgramm in sendedData.Where(x => x.PlayerId == i+1))
                    {
                        string impData = JsonSerializer.Serialize(Dgramm);
                        byte[] impBuffer = Encoding.UTF8.GetBytes(impData);
                        socket.SendTo(impBuffer, clientsEP[i]);
                    }
                }
                currentDatagrammId++;
                for (int i = 0; i < 200 && i < dataToSend.Count; i++)
                    importantUpdates.RemoveAt(0);
            }
            Datagramm unImpDgramm = new Datagramm();

            dataToSend = new List<UpdateData>();
            for (int i = 0; i < 200 && i < commonUpdates.Count; i++)
                dataToSend.Add(commonUpdates[i]);

            unImpDgramm.updateDatas = dataToSend;
            string data = JsonSerializer.Serialize(unImpDgramm);
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            foreach (EndPoint sendingEP in clientsEP)
            {
                socket.SendTo(buffer, sendingEP);
            }
            for (int i = 0; i < 200 && i < dataToSend.Count; i++)
                commonUpdates.RemoveAt(0);
        }

        /// <summary>
        /// добавляет в лист updates новую data
        /// </summary>
        /// <param name="data"></param>
        public void AddData(UpdateData data)
        {
            if (data.isImportant)
            {
                importantUpdates.Add(data);
            }
            else {
                commonUpdates.Add(data);
            }
        }

        /// <summary>
        /// Начинает работу сервера (Ожидает подключений)
        /// </summary>
        /// <param name="players"></param>
        public void Start()
        {
            serverThread = new Thread(StartWaitingForPlayers);
            serverThread.IsBackground = true;
            serverThread.Start();
        }
        public void StartGame()
        {
            for (int i = 0; i < clientsEP.Count; i++)
            {
                Datagramm initDgramm = new Datagramm();
                initDgramm.isImportant = true;
                initDgramm.DatagrammId = currentDatagrammId;
                initDgramm.PlayerId = i + 1;
                sendedData.Add(initDgramm);
                string data = JsonSerializer.Serialize(initDgramm);
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                socket.SendTo(buffer, clientsEP[i]);
                currentDatagrammId++;
            }
            AppManager.Instance.ChangeState(GameState.HostPlaying);
            AppManager.Instance.SetGUI(new HUD());////
        }

        //Потоки Клиентов
        /// <summary>
        /// Слушает игроков, которые хотят подключиться
        /// </summary>
        /// <param name="players"></param>
        public void StartWaitingForPlayers()
        {
            byte[] buffer = new byte[65535];
            string data;
            while (socket != null)
            {
                EndPoint senderRemote = (EndPoint)new IPEndPoint(IPAddress.Any, 0);
                int size = socket.ReceiveFrom(buffer, buffer.Length, SocketFlags.None, ref senderRemote);
                if (AppManager.Instance.gamestate != GameState.HostPlaying && !clientsEP.Contains(senderRemote) &&
                    senderRemote != new IPEndPoint(IPAddress.Any, 0))
                {
                    clientsEP.Add((IPEndPoint)senderRemote);
                    AppManager.Instance.debugHud.Log($"Connect {senderRemote.ToString()}");
                    if (!isMultiplayer) AppManager.Instance.ChangeState(GameState.HostPlaying);
                    // Отправлять Init апдейт с информацией об ID игрока и ID датаграмма на сервере
                    //Можно добавить bool isInit для Датаграммов
                }
                byte[] correctedBuffer = new byte[size];
                Array.Copy(buffer, correctedBuffer, size);
                data = Encoding.UTF8.GetString(correctedBuffer);
                GetDataSend(data);

            }
        }
        public void AnalyzeData(string data) 
        {
            Datagramm Dgramm = JsonSerializer.Deserialize<Datagramm>(data);
            if (Dgramm.updateDatas == null)
            {
                //Обработка acknowledgement
                arrivingDataId.Add(Dgramm);
            }
            else
            {
                //Настроить десериализацию и применять неважные апдейты
            }

        }
    }
}
