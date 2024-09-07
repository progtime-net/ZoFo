using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using ServerOverall.Server.Updates.ServerToClient;
using ZoFo.GameCore.GameManagers.NetworkManager.SerializableDTO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ServerOverall.Server.Updates.ClientToServer;
using ServerOverall.Server.Updates;
using System.Xml.Linq;

namespace ServerOverall.Server
{
    public class ServerNetworkManager
    {
        private Socket socket;
        private IPAddress ip;
        private bool isMultiplayer;
        //Player Id to Player endPoint
        public List<IPEndPoint> clientsEP;
        public IPEndPoint endPoint;
        private List<UpdateData> commonUpdates;
        private List<UpdateData> importantUpdates;
        private List<Datagramm> sendedData;
        private List<Datagramm> arrivingDataId;
        private int currentDatagrammId = 0;
        public delegate void OnDataSend(string data);
        public event OnDataSend GetDataSend;   // event
        Thread serverThread;
        int datapackSize = 150;
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
            var ips = NetworkInterface.GetAllNetworkInterfaces()
                .Where(x => x.OperationalStatus == OperationalStatus.Up)
                .Where(x => x.NetworkInterfaceType is NetworkInterfaceType.Wireless80211
                    or NetworkInterfaceType.Ethernet)
                .SelectMany(x => x.GetIPProperties().UnicastAddresses)
                .Where(x => x.Address.AddressFamily == AddressFamily.InterNetwork)
                .Select(x => x.Address)
                .ToList();

            if (ips.Count > 0)
            {
                return ips[^1];
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
            if (importantUpdates.Count > 0)
            {
                dataToSend = new List<UpdateData>();
                for (int j = 0; j < datapackSize && j < importantUpdates.Count; j++)
                    dataToSend.Add(importantUpdates[j]);
                for (int i = 0; i < clientsEP.Count; i++)
                {
                    Datagramm impDgramm = new Datagramm();
                    impDgramm.DatagrammId = currentDatagrammId;
                    impDgramm.updateDatas = dataToSend;
                    impDgramm.isImportant = true;
                    impDgramm.PlayerId = i + 1;
                    sendedData.Add(impDgramm);
                }
                for (int j = 0; j < datapackSize && j < dataToSend.Count; j++)
                    importantUpdates.RemoveAt(0);
                currentDatagrammId++;
            }

            if (sendedData.Count != 0)
            {
                for (int i = 0; i < clientsEP.Count; i++)
                {
                    foreach (Datagramm Dgramm in sendedData.Where(x => x.PlayerId == i + 1))
                    {
                        string impData = System.Text.Json.JsonSerializer.Serialize(Dgramm);
                        byte[] impBuffer = Encoding.UTF8.GetBytes(impData);
                        socket.SendTo(impBuffer, clientsEP[i]);
                    }
                }
            }
            Datagramm unImpDgramm = new Datagramm();

            dataToSend = new List<UpdateData>();
            for (int i = 0; i < 200 && i < commonUpdates.Count; i++)
                dataToSend.Add(commonUpdates[i]);

            unImpDgramm.updateDatas = dataToSend;
            string data = System.Text.Json.JsonSerializer.Serialize(unImpDgramm);
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
            else
            {
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
                string data = System.Text.Json.JsonSerializer.Serialize(initDgramm);
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                socket.SendTo(buffer, clientsEP[i]);
            }
            currentDatagrammId++;
            //AppManager.Instance.ChangeState(GameState.HostPlaying);
            //AppManager.Instance.SetGUI(new HUD());////
        }
        public void CloseConnection()
        {
            //socket.Shutdown(SocketShutdown.Both);
            clientsEP.Clear();
            socket.Close();
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
            int size;
            try
            {
                while (socket != null)
                {
                    EndPoint senderRemote = (EndPoint)new IPEndPoint(IPAddress.Any, 0);
                    size = socket.ReceiveFrom(buffer, buffer.Length, SocketFlags.None, ref senderRemote);
                    //if (AppManager.Instance.gamestate != GameState.HostPlaying && !clientsEP.Contains(senderRemote) &&
                    //    senderRemote != new IPEndPoint(IPAddress.Any, 0))
                    //{
                    //    clientsEP.Add((IPEndPoint)senderRemote);
                    //    AppManager.Instance.debugHud.Log($"Connect {senderRemote.ToString()}");
                    //    if (!isMultiplayer) AppManager.Instance.ChangeState(GameState.HostPlaying);
                    //    // Отправлять Init апдейт с информацией об ID игрока и ID датаграмма на сервере
                    //    //Можно добавить bool isInit для Датаграммов
                    //}
                    byte[] correctedBuffer = new byte[size];
                    Array.Copy(buffer, correctedBuffer, size);
                    data = Encoding.UTF8.GetString(correctedBuffer);
                    GetDataSend(data);

                }
            }
            catch (Exception)
            {
                return;
            }
        }
        public void AnalyzeData(string data)
        {
            JObject jObj = JsonConvert.DeserializeObject(data) as JObject;
            JToken token = JToken.FromObject(jObj);
            JToken updateDatas = token["updateDatas"];
            Datagramm Dgramm = new Datagramm();
            Dgramm.PlayerId = token["PlayerId"].ToObject<int>();
            if (!updateDatas.HasValues)
            {
                //Обработка acknowledgement
                Dgramm.DatagrammId = token["DatagrammId"].ToObject<int>();
                arrivingDataId.Add(Dgramm);
            }
            else
            {
                List<UpdateData> updates = GetSentUpdates(updateDatas);
                //AppManager.Instance.server.UpdatesList(updates);
            }
        }
        public List<UpdateData> GetSentUpdates(JToken updatesToken)
        {
            List<UpdateData> data = new List<UpdateData>();
            JArray updateDatas = updatesToken as JArray;
            UpdateData update = new UpdateData();
            foreach (JObject token in updateDatas.Children())
            {
                switch (token["UpdateType"].ToObject<string>())
                {
                    case "UpdateInput":
                        update = token.ToObject<UpdateInput>();
                        data.Add(update);
                        break;
                    case "UpdateInputInteraction":
                        update = token.ToObject<UpdateInputInteraction>();
                        data.Add(update);
                        break;
                    case "UpdateInputShoot":
                        update = token.ToObject<UpdateInputShoot>();
                        data.Add(update);
                        break;
                }
            }
            return data;
        }

    }
}
//TODO: Перелопатить весь код и вынести на удалённый сервер