using System;
using System.Data.SqlTypes;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;


namespace ZoFo.GameCore.GameManagers.NetworkManager
{
    public class ClientNetworkManager
    {
        private IPAddress iPAddress = IPAddress.Any;
        private int port = 7632;
        private EndPoint endPoint;
        private Socket socket;
        public delegate void OnDataSent(string Data);
        public event OnDataSent GetDataSent; // event
        public void Init() //create endPoint, socket
        {
            endPoint = new IPEndPoint(iPAddress, port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void SendData()
        {

        }

        public void StopConnection()
        { 
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

        public void JoinRoom() // multyplayer
        {
            SendData();
            StartListening();
        }

        public void JoinYourself()  // single player
        {
            SendData();
            StartListening();
        }

        //поток 2
        public void StartListening()
        {
            socket.Connect(endPoint);

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