using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates;

namespace ZoFo.GameCore.GameManagers.NetworkManager
{
    public class ServerNetworkManager
    {
        private IPAddress ip = IPAddress.Any;
        private int port = 7632;
        private IPEndPoint endPoint;
        private Socket socket;
        private List<Socket> clients;
        private List<IUpdateData> updates;
        delegate void OnDataSend(string data);  //

        public void Init()   //create Socket
        {
            endPoint = new IPEndPoint(ip, port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void SendData()
        {

        }
        public void AddData(IUpdateData data) { }


        //Поток 2
        public void StartWaitingForPlayers()//Слушает игроков, которые хотят подключиться
        {
            socket.Bind(endPoint);
            socket.Listen(10);
            for (int i = 0; i < 10; i++)
            {
                Socket client = socket.Accept();
                clients.Add(client);  //добавляем клиентов в лист
            }

            StartListening();
        }

        public void StartListening()//начать слушать клиентов в самой игре активируют Ивент
        {
            var buff = new byte[1024];
            foreach (var client in clients)
            {
                var answ = client.Receive(buff);
            }
        }
    }
}
