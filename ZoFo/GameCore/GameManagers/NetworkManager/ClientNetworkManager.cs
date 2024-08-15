using System.Security.Cryptography.X509Certificates;
using System.Threading;


namespace ZoFo.GameCore.GameManagers.NetworkManager
{
    public delegate void OnDataSent(string Data);

    public class ClientNetworkManager
    {
         public event OnDataSent DataSent;
        public static void StartListening()
        {

        }
    }
}