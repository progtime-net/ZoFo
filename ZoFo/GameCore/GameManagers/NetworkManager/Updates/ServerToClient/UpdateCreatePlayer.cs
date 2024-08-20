using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient
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
