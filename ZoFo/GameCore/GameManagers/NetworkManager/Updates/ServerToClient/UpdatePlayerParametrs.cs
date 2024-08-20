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
        public UpdatePlayerParametrs() { UpdateType = "UpdatePlayerParametrs"; isImportant = true; }
        public float radiatoin { get; set; }
        public float health { get; set; }
    }
}
