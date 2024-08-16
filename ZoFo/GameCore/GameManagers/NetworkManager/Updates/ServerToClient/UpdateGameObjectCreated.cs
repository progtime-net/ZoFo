using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient
{
    /// <summary>
    /// Хранит объект, который только отправили
    /// </summary>
    public class UpdateGameObjectCreated : UpdateData     
    {
        public UpdateGameObjectCreated() { UpdateType = "UpdateGameObjectCreated"; }
    }
}
