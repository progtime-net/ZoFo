using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient
{
    public class UpdateGameObjectDeleted : UpdateData     //Хранит объект, который надоу удлить
    {
        public UpdateGameObjectDeleted() { UpdateType = "UpdateGameObjectDeleted"; }
    }
}