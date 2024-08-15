﻿using Microsoft.Xna.Framework.Graphics;
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
        private IPAddress ip = IPAddress.Any;
        private int port = 7632;
        private IPEndPoint endPoint;
        private Socket socket;
        private List<Socket> clients;
        private List<IUpdateData> updates;
        public delegate void OnDataSend(string data);
        public event OnDataSend GetDataSend;   // event
        Dictionary<Socket, Thread> managerThread;


        public void Init()   //create Socket
        {
            endPoint = new IPEndPoint(ip, port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            managerThread = new Dictionary<Socket, Thread>();
        }
        public void SendData() //отправляет клиенту Data
        {
            string data = JsonSerializer.Serialize(updates);
            var databytes = Encoding.UTF8.GetBytes(data);
            foreach (var item in clients)
            {
                item.SendAsync(databytes);
            }
        }
        public void AddData(IUpdateData data)//добавляет в лист updates новую data
        {
            updates.Add(data);
        }

        public void CloseConnection()
        {
            foreach (var item in clients)
            {
                item.Shutdown(SocketShutdown.Both);
                item.Close();
            }
            foreach (var item in managerThread)
            {
                foreach (var socket in clients)
                {
                    managerThread[socket].Interrupt();
                }
            }
            managerThread.Clear();
            clients.Clear();
        }
        //Поток 2

        public void StartWaitingForPlayers(object players)//Слушает игроков, которые хотят подключиться
        {
            int playNumber = (int)players;
            socket.Bind(endPoint);
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

        private void StartListening(object socket)//начать слушать клиентов в самой игре активируют Ивент
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
