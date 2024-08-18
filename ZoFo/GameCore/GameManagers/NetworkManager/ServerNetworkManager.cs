using Microsoft.Xna.Framework;
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

namespace ZoFo.GameCore.GameManagers.NetworkManager
{
    public class ServerNetworkManager
    {
        private IPAddress ip = IPAddress.Parse("127.0.0.1");
        private const int port = 0;
        private IPEndPoint endPoint;
        private Socket socket;
        private List<Socket> clients;
        public List<UpdateData> updates;
        public delegate void OnDataSend(string data);
        public event OnDataSend GetDataSend;   // event
        Dictionary<Socket, Thread> managerThread;
        Thread serverTheread;
        public IPEndPoint InfoConnect => (IPEndPoint)socket.LocalEndPoint ?? endPoint;

        public ServerNetworkManager() { Init(); }

        /// <summary>
        /// Initialize varibles and Sockets
        /// </summary>
        private void Init()
        {
            endPoint = new IPEndPoint(GetIp(), port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            managerThread = new Dictionary<Socket, Thread>();
            clients = new List<Socket>();
            updates = new List<UpdateData>();
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

            foreach (var ip in ipList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }
            return IPAddress.Loopback;
        }

        /// <summary>
        /// отправляет клиенту Data
        /// </summary>
        public void SendData()
        {
            for (int i = 0; i < updates.Count; i++)
            {

                AppManager.Instance.client.GotData(updates[i]);
            }
            updates.Clear();
            return; //TODO TODO REMOVE TO ADD NETWORK TODO REMOVE TO ADD NETWORK TODO REMOVE TO ADD NETWORK TODO REMOVE TO ADD NETWORK
            //Что это?
            //по 10 паков за раз TODO FIXITFIXITFIXITFIXITFIXITFIXITFIXITFIXITFIXITFIXITFIXITFIXIT
            List<UpdateData> datasToSend = new List<UpdateData>();
            for (int i = 0; i < 200 && i<updates.Count; i++)
                datasToSend.Add(updates[i]);
            string data = JsonSerializer.Serialize(datasToSend);
            var databytes = Encoding.UTF8.GetBytes(data);
            foreach (Socket socket in clients)
            {
                clients[0].SendAsync(databytes, SocketFlags.Partial);
            }
            for (int i = 0; i < 200 && i< datasToSend.Count; i++)
                updates.RemoveAt(0); 
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
            serverTheread.IsBackground = true;
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
                AppManager.Instance.debugHud.Log($"Connect {client.LocalEndPoint.ToString()}");
                Thread thread = new Thread(StartListening);
                thread.IsBackground = true;
                thread.Start(client);
                managerThread.Add(client, thread);
                clients.Add(client);
               //AppManager.Instance.ChangeState(GameState.HostPlaying);
                //добавляем клиентов в лист
            }
            AppManager.Instance.ChangeState(GameState.HostPlaying);
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
                var buff = new byte[65535];
                var answ = client.Receive(buff, SocketFlags.Partial);
                string response = Encoding.UTF8.GetString(buff, 0, answ);
                GetDataSend(response);
            }
            Task.Delay(-1);

        }
    }
}
