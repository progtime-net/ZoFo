using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient
{
    public class UpdateLoot : UpdateData //Хранит лут
    {
      public UpdateLoot() { UpdateType = "UpdateLoot"; }
    }
}
