using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient
{
    /// <summary>
    /// Хранит лут
    /// </summary>
    public class UpdateLoot : UpdateData 
    {
      public UpdateLoot() { UpdateType = "UpdateLoot"; }
    }
}
