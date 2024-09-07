using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers.NetworkManager.SerializableDTO;

namespace ServerOverall.Server.Updates.ServerToClient
{
    public class UpdateCreatePlayer : UpdateData
    {
        public int PlayerId {  get; set; }
        public UpdateCreatePlayer()
        {
            isImportant = true;
            UpdateType = "UpdateCreatePlayer";
        }

    }
}
