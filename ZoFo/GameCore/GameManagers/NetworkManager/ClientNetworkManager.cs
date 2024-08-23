using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;
using ZoFo.GameCore.GUI;


namespace ZoFo.GameCore.GameManagers.NetworkManager
{
    public class ClientNetworkManager
    {
        public int PlayerId = 0;
        private IPEndPoint endPoint;
        private IPEndPoint sendingEP;
        private Socket socket;
        List<UpdateData> updates = new List<UpdateData>();
        private List<Datagramm> waitingDatagramm = new List<Datagramm>();
        private int currentServerDatagrammId = 0;
        public delegate void OnDataSent(string Data);
        public event OnDataSent GetDataSent; // event
        public bool IsConnected { get { return socket.Connected; } }
        public IPEndPoint InfoConnect => (IPEndPoint)socket.LocalEndPoint ?? endPoint;
        public ClientNetworkManager()
        {
            Init();
        }

        public void Init() //create endPoint, socket
        {
            GetDataSent += AnalyzeData;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            endPoint = new IPEndPoint(GetIp(), 8081);
            socket.Bind(endPoint);
            Thread thread = new Thread(StartListening);
            thread.IsBackground = true;
            thread.Start();
        }

        public void SendData()
        {
            if (updates != null)
            {
                Datagramm Datagramm = new Datagramm();
                Datagramm.updateDatas = updates;
                byte[] bytes = Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(Datagramm));  //нужно сериализовать
                socket.SendTo(bytes, sendingEP);
                updates.Clear();
            }

        }

        public void AddData(UpdateData UpdateData)
        {
            updates.Add(UpdateData);
        }

        #region Working With Data RDP

        public void AnalyzeData(string data)
        {
            JObject jObj = JsonConvert.DeserializeObject(data) as JObject;
            JToken token = JToken.FromObject(jObj);
            JToken updateDatas = token["updateDatas"];
            if (updateDatas.HasValues)
            {
                Datagramm Dgramm = new Datagramm();
                Dgramm.isImportant = token["isImportant"].ToObject<bool>();
                Dgramm.DatagrammId = token["DatagrammId"].ToObject<int>();
                if (PlayerId == 0)
                {
                    PlayerId = token["PlayerId"].ToObject<int>();
                    if (AppManager.Instance.gamestate != GameState.HostPlaying)
                    {
                        AppManager.Instance.ChangeState(GameState.ClientPlaying);
                    }
                    SendAcknowledgement(Dgramm.DatagrammId);
                }
                if (Dgramm.isImportant)
                {
                    if (Dgramm.DatagrammId == currentServerDatagrammId + 1)
                    {
                        currentServerDatagrammId++;
                        Dgramm.updateDatas = GetSentUpdates(token["updateDatas"]);
                        ExecuteDatagramm(Dgramm);
                        CheckDatagramm();
                    }
                    else if (Dgramm.DatagrammId > currentServerDatagrammId + 1 &&
                        waitingDatagramm.Find(x => x.DatagrammId == Dgramm.DatagrammId) == null)
                    {
                        Dgramm.updateDatas = GetSentUpdates(token["updateDatas"]);
                        waitingDatagramm.Add(Dgramm);
                    }
                    SendAcknowledgement(Dgramm.DatagrammId);
                }
                else
                {
                    Dgramm.updateDatas = GetSentUpdates(token["updateDatas"]);
                    ExecuteDatagramm(Dgramm);
                }
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
                    case "UpdateAnimation":
                        update = token.ToObject<UpdateAnimation>();
                        data.Add(update);
                        break;
                    case "UpdateEntityHealth":
                        update = token.ToObject<UpdateEntityHealth>();
                        data.Add(update);
                        break;
                    case "UpdateGameEnded":
                        update = token.ToObject<UpdateGameEnded>();
                        data.Add(update);
                        break;
                    case "UpdateGameObjectCreated":
                        update = token.ToObject<UpdateGameObjectCreated>();
                        data.Add(update);
                        break;
                    case "UpdateGameObjectWithoutIdCreated":
                        update = token.ToObject<UpdateGameObjectWithoutIdCreated>();
                        data.Add(update);
                        break;
                    case "UpdateGameObjectDeleted":
                        update = token.ToObject<UpdateGameObjectDeleted>();
                        data.Add(update);
                        break;
                    case "UpdateInteraction":
                        update = token.ToObject<UpdateInteraction>();
                        data.Add(update);
                        break;
                    case "UpdateStopObjectCreated":
                        update = token.ToObject<UpdateStopObjectCreated>();
                        data.Add(update);
                        break;
                    case "UpdateInteractionReady":
                        update = token.ToObject<UpdateInteractionReady>();
                        data.Add(update);
                        break;
                    case "UpdateLoot":
                        update = token.ToObject<UpdateLoot>();
                        data.Add(update);
                        break;
                    case "UpdatePlayerParametrs":
                        update = token.ToObject<UpdatePlayerParametrs>();
                        data.Add(update);
                        break;
                    case "UpdatePosition":
                        update = token.ToObject<UpdatePosition>();
                        data.Add(update);
                        break;
                    case "UpdateTileCreated":
                        update = token.ToObject<UpdateTileCreated>();
                        data.Add(update);
                        break;
                    case "UpdateCreatePlayer":
                        update = token.ToObject<UpdateCreatePlayer>();
                        data.Add(update);
                        break;

                }
            }

            return data;
        }

        public void SendAcknowledgement(int DatagrammId)
        {
           
            Datagramm Dgramm = new Datagramm() { DatagrammId = DatagrammId, PlayerId = PlayerId };
            string data = System.Text.Json.JsonSerializer.Serialize(Dgramm);
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            socket.SendTo(buffer, sendingEP);

        }
        void CheckDatagramm()
        {
            Datagramm orderedDgramm = waitingDatagramm.Find(x => x.DatagrammId == currentServerDatagrammId + 1);
            while (orderedDgramm != null)
            {
                currentServerDatagrammId++;
                ExecuteDatagramm(orderedDgramm);
                waitingDatagramm.Remove(orderedDgramm);
                orderedDgramm = waitingDatagramm.Find(x => x.DatagrammId == currentServerDatagrammId + 1);
            }
        }
        void ExecuteDatagramm(Datagramm Dgramm)
        {

            AppManager.Instance.client.UpdatesList(Dgramm.updateDatas);
            //Достаёт Update и передает в ивент
        }

#endregion
        #region Join
        /// <summary>
        /// приложение пытается подключиться к комнате
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public void JoinRoom(string ip, int port) // multyplayer
        {
            sendingEP = new IPEndPoint(IPAddress.Parse(ip), port);
            SendData();
            Thread listen = new Thread(StartListening);
            listen.IsBackground = true;
            listen.Start();
        }

        /// <summary> 
        /// создается одиночная комната к которой ты подключаешься 
        /// </summary>
        public void JoinYourself(int port)  // single player
        {
            sendingEP = AppManager.Instance.server.MyIp;
            SendData();
            Thread listen = new Thread(StartListening);
            listen.IsBackground = true;
            listen.Start();
        }
        #endregion
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

        //поток 2
        public void StartListening()
        {
            byte[] buffer = new byte[65535];
            string data;
            while (socket != null)
            { 
                EndPoint senderRemote = new IPEndPoint(IPAddress.Any, 0);
                int size = socket.ReceiveFrom(buffer, buffer.Length, SocketFlags.None, ref senderRemote);
                byte[] correctedBuffer = new byte[size];
                Array.Copy(buffer, correctedBuffer, size);
                data = Encoding.UTF8.GetString(correctedBuffer);
                GetDataSent(data); 
            }
        }
    }
}