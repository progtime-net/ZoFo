using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient
{
    public class UpdateEntityHealth : UpdateData//хранит новое хп entity
    {
       public UpdateEntityHealth() { UpdateType = "UpdateEntityHealth"; }
    }
}
