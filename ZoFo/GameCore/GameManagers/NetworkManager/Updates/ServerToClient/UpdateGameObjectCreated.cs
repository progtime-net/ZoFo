using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient
{
    public class UpdateGameObjectCreated : UpdateData     //Хранит объект, который только отправили
    {
        public UpdateGameObjectCreated() { UpdateType = "UpdateGameObjectCreated"; }
    }
}
