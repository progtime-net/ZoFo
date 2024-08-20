using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers.NetworkManager.SerializableDTO;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient
{
    public class UpdateCreatePlayer : UpdateData
    {
        public int PlayerId {  get; set; }
        public SerializableVector2 position { get; set; }
        public UpdateCreatePlayer()
        {
            isImportant = true;
            UpdateType = "UpdateCreatePlayer";
        }

    }
}
