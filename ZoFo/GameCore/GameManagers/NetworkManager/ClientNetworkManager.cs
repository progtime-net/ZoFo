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
        public delegate void OnDataSent(string Data);
        public event OnDataSent GetDataSent; // event
        public bool IsConnected { get { return socket.Connected; } }
        public IPEndPoint InfoConnect => (IPEndPoint)socket.LocalEndPoint ?? endPoint;

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
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void SendData()
        {
            for (int i = 0; i < updates.Count; i++)
            {

                AppManager.Instance.server.ProcessIUpdateData(updates[i]);
            }
            updates.Clear();
            return;// TODO remove
                byte[] bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(updates));  //нужно сериализовать
                socket.Send(bytes);
        }

        public void AddData(UpdateData UpdateData)
        {
            updates.Add(UpdateData);
        }

        public void StopConnection()
        { 
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

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

        public static IPAddress GetIp()
        {
            /*string hostName = Dns.GetHostName(); // Retrive the Name of HOST
            var ipList = Dns.GetHostByName(hostName).AddressList;
            foreach (var ip in ipList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }*/
            return IPAddress.Parse("127.0.0.1");
        }

        //поток 2
        public void StartListening()
        {
            while(socket.Connected)
            {
                byte[] bytes = new byte[2048];
                var countAnsw = socket.Receive(bytes);    //Вылетает если кто то закрыл
                string update = Encoding.UTF8.GetString(bytes, 0, countAnsw);   // обновление отосланные сервером
                GetDataSent(update);
            }
        }
    }
}