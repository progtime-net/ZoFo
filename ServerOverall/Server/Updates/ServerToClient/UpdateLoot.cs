using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerOverall.Server.Updates.ServerToClient
{
    /// <summary>
    /// Хранит лут
    /// </summary>
    public class UpdateLoot : UpdateData 
    {
        public string lootName { get; set; }
        public int quantity { get; set; }
        public UpdateLoot() { UpdateType = "UpdateLoot"; isImportant = true; }
        public UpdateLoot(string lootName, int quantity, int id) 
        { 
            UpdateType = "UpdateLoot";
            this.lootName = lootName;
            this.quantity = quantity;
            IdEntity = id;
            isImportant = true;
        }
    }
}
