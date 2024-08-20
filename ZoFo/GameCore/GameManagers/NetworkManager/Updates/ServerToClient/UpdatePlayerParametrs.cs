using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient
{
    /// <summary>
    /// Хранит хп, радиацию
    /// </summary>
    public class UpdatePlayerParametrs : UpdateData    
    {
        public UpdatePlayerParametrs() { UpdateType = "UpdatePlayerParametrs"; }
        public float radiatoin;
        public float health;
    }
}
