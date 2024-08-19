using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates;


namespace ZoFo.GameCore.GameManagers.NetworkManager
{
    public class ClientNetworkManager
    {
        private int port = 0;
        private IPEndPoint endPoint;
        private Socket socket;
        List<UpdateData> updates = new List<UpdateData>();
        private List<Datagramm> waitingDatagramm = new List<Datagramm>();
        private int currentServerDatagrammId = 0;
        public delegate void OnDataSent(string Data);
        public event OnDataSent GetDataSent; // event
        public bool IsConnected { get { return socket.Connected; } }
        public IPEndPoint InfoConnect => (IPEndPoint)socket.LocalEndPoint ?? endPoint;
        public EndPoint EndPointServer { get; set; }
        public ClientNetworkManager()
        {
            Init();
        }

        public bool SocketConnected()
        {
            return socket.Connected;
        }

        public void Init() //create endPoint, socket
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            endPoint = new IPEndPoint(GetIp(), 0);
            socket.Bind(endPoint);
            Thread thread = new Thread(StartListening);
            thread.Start();
        }

        public void SendData()
        {
            byte[] bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(updates));  //нужно сериализовать
            socket.Send(bytes);
        }

        public void AddData(UpdateData UpdateData)
        {
            updates.Add(UpdateData);
        }

        #region Working With Data RDP

        public void AnalyzeData(string data)
        {
            Datagramm Dgramm = JsonSerializer.Deserialize<Datagramm>(data);
            if (Dgramm.isImportant)
            {
                if (Dgramm.DatagrammId == currentServerDatagrammId + 1)
                {
                    ExecuteDatagramm(Dgramm);
                    currentServerDatagrammId++;
                    CheckDatagramm();
                }
                else if (Dgramm.DatagrammId > currentServerDatagrammId + 1)
                {
                    waitingDatagramm.Add(Dgramm);
                }
                else
                {
                    Console.WriteLine("Апдейты " + Dgramm.DatagrammId + ", уже приходили, пропускаем");
                }
                SendAcknowledgement(Dgramm.DatagrammId);
            }
            else
            {
                ExecuteDatagramm(Dgramm);
            }

        }
        public void SendAcknowledgement(int DatagrammId)
        {
           
            Datagramm Dgramm = new Datagramm() { DatagrammId = DatagrammId };
            string data = JsonSerializer.Serialize(Dgramm);
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            socket.SendTo(buffer, EndPointServer);

        }
        void CheckDatagramm()
        {
            Datagramm orderedDgramm = waitingDatagramm.Find(x => x.DatagrammId == currentServerDatagrammId + 1);
            while (orderedDgramm != null)
            {
                ExecuteDatagramm(orderedDgramm);
                currentServerDatagrammId++;
                orderedDgramm = waitingDatagramm.Find(x => x.DatagrammId == currentServerDatagrammId + 1);
            }
        }
        void ExecuteDatagramm(Datagramm Dgramm)
        {
            //Достаёт Update и передает в ивент
        }

#endregion
        public void StopConnection()
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
        #region Join
        /// <summary>
        /// приложение пытается подключиться к комнате
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public void JoinRoom(string ip, int port) // multyplayer
        {

            endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            socket.Connect(endPoint);
            SendData();
            Thread listen = new Thread(StartListening);
            listen.IsBackground = true;
            listen.Start();
        }
        public void JoinRoom(IPEndPoint endPoint) // multyplayer
        {

            this.endPoint = endPoint;
            socket.Connect(endPoint);
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
            endPoint = new IPEndPoint(GetIp(), port);
            socket.Connect(endPoint);
            SendData();
            Thread listen = new Thread(StartListening);
            listen.IsBackground = true;
            listen.Start();
        }
        #endregion
        public static IPAddress GetIp()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST
            var ipList = Dns.GetHostByName(hostName).AddressList;

            foreach (var ip in ipList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip;
                }
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
                AnalyzeData(data);
            }
        }
    }
}