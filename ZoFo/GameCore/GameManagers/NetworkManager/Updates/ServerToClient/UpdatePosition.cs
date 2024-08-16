using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient
{
    /// <summary>
    /// Хранит новую позицию
    /// </summary>
    public class UpdatePosition : UpdateData       
    {
        public UpdatePosition() { UpdateType = "UpdatePosition"; }
    }
}
