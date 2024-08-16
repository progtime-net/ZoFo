using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates;

namespace ZoFo.GameCore.GameManagers.NetworkManager
{
    public class ServerNetworkManager
    {
        private IPAddress ip =IPAddress.Parse("127.0.0.1"); //IPAddress.Any
        private int port = 7632;
        private IPEndPoint endPoint;
        private Socket socket;
        private List<Socket> clients;
        private List<UpdateData> updates;
        public delegate void OnDataSend(string data);
        public event OnDataSend GetDataSend;   // event
        Dictionary<Socket, Thread> managerThread;
        Thread serverTheread;

        public ServerNetworkManager() { Init(); }

        /// <summary>
        /// Initialize varibles and Sockets
        /// </summary>
        private void Init()
        {
            endPoint = new IPEndPoint(ip, port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            managerThread = new Dictionary<Socket, Thread>();
            clients = new List<Socket>();
            updates = new List<UpdateData>();
            managerThread = new Dictionary<Socket, Thread>();
            socket.Bind(endPoint);
        }

        /// <summary>
        /// отправляет клиенту Data
        /// </summary>
        public void SendData() 
        {
            string data = JsonSerializer.Serialize(updates);
            var databytes = Encoding.UTF8.GetBytes(data);
            foreach (var item in clients)
            {
                item.SendAsync(databytes);
            }
        }

        /// <summary>
        /// добавляет в лист updates новую data
        /// </summary>
        /// <param name="data"></param>
        public void AddData(UpdateData data)
        {
            updates.Add(data);
        }

        /// <summary>
        /// По сути конец игры и отключение игроков
        /// </summary>
        public void CloseConnection() 
        {
            foreach (var item in clients)
            {
                //Закрывает сокеты клиентов
                item.Shutdown(SocketShutdown.Both);
                item.Close();
            }
            foreach (var item in managerThread)
            {
                foreach (var socket in clients)
                {
                    //Закрывает потоки клиентов
                    managerThread[socket].Interrupt();
                }
            }
            //очищает листы
            managerThread.Clear();
            clients.Clear();
        }

        /// <summary>
        /// Начинает работу сервера (Ожидает подключений)
        /// </summary>
        /// <param name="players"></param>
        public void Start(object players)
        {
            serverTheread = new Thread(StartWaitingForPlayers);
            serverTheread.Start(players);
        }

        //Потоки Клиентов

        /// <summary>
        /// Слушает игроков, которые хотят подключиться
        /// </summary>
        /// <param name="players"></param>
        public void StartWaitingForPlayers(object players)
        {
            int playNumber = (int)players;
          
            socket.Listen(playNumber);
            for (int i = 0; i < playNumber; i++)
            {
                Socket client = socket.Accept();
                Thread thread = new Thread(StartListening);
                thread.Start(client);
                managerThread.Add(client, thread);
                clients.Add(client);  //добавляем клиентов в лист
            }

        }

        /// <summary>
        /// начать слушать клиентов в самой игре активируют Ивент
        /// </summary>
        /// <param name="socket"></param>
        private void StartListening(object socket)
        {
            // obj to Socket
            Socket client = (Socket)socket;
            while (client.Connected)
            {
                var buff = new byte[1024];
                var answ = client.Receive(buff);
                string response = Encoding.UTF8.GetString(buff, 0, answ);
                GetDataSend(response);
            }
            Thread.Sleep(-1);

        }
    }
}
