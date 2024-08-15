using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace ZoFo.GameCore.GameManagers.NetworkManager;

public delegate void OnDataSent(string Data);

public class ClientNetworkManager
{
    static public event OnDataSent DataSent;

    static void OnDataSent(string Data)
    {

    }
    static void StartListening()
    {

    }
}