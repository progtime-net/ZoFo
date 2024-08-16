using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient
{
    public class UpdateGameEnded : UpdateData    //хранит полученый лут и уведомляет о конце игры
    {
      public UpdateGameEnded() { UpdateType = "UpdateGameEnded"; }
    }
}
