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
        delegate void OnDataSent(string Data);

        public void Init() //create endPoint, socket
        {
            endPoint = new IPEndPoint(iPAddress, port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void SendData()
        {

        }

        public void JoinRoom()
        {
            StartListening();
        }

        public void JoinYourself()
        {
            StartListening();
        }

        //поток 2
        public void StartListening()
        {
            socket.Connect(endPoint);

            byte[] bytes = new byte[2048];

            var countAnsw = socket.Receive(bytes);

            string message = Encoding.UTF8.GetString(bytes, 0, countAnsw);
        }
    }
}