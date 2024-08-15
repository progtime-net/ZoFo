using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates;


namespace ZoFo.GameCore.GameManagers.NetworkManager
{
    public class ClientNetworkManager
    {
        private int port = 7632;
        private EndPoint endPoint;
        private Socket socket;
        List<IUpdateData> updates = new List<IUpdateData>();
        public delegate void OnDataSent(string Data);
        public event OnDataSent GetDataSent; // event
        public void Init() //create endPoint, socket
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void SendData()
        {
            while(socket.Connected)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(updates.ToString());
                socket.Send(bytes);
            }
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
        public void JoinRoom(string ip) // multyplayer
        {
            endPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            socket.Connect(endPoint);

            SendData();
            Thread listen = new Thread(StartListening);
            listen.Start();
        }

        /// <summary>
        /// создается 
        /// </summary>
        public void JoinYourself()  // single player
        {
            endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);

            socket.Connect(endPoint);

            SendData();
            Thread listen = new Thread(StartListening);
            listen.Start();
        }

        //поток 2
        public void StartListening()
        {
            while(socket.Connected)
            {
                byte[] bytes = new byte[2048];
                var countAnsw = socket.Receive(bytes);
                string update = Encoding.UTF8.GetString(bytes, 0, countAnsw);   // обновление отосланные сервером
                GetDataSent(update);
            }
        }
    }
}