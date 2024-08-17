using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient
{
    /// <summary>
    /// Хранит объект, который надо удлить
    /// </summary>
    public class UpdateGameObjectDeleted : UpdateData    
    {
        public UpdateGameObjectDeleted() { UpdateType = "UpdateGameObjectDeleted"; }
    }
}