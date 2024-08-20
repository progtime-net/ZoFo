using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient
{
    /// <summary>
    /// Хранит лут
    /// </summary>
    public class UpdateLoot : UpdateData 
    {
        public string lootName;
        public int quantity;
        public UpdateLoot() { UpdateType = "UpdateLoot"; }
        public UpdateLoot(string lootName, int quantity, int id) 
        { 
            UpdateType = "UpdateLoot";
            this.lootName = lootName;
            this.quantity = quantity;
            IdEntity = id;
        }
    }
}
