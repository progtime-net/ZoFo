using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient
{
    public class UpdatePlayerParametrs : IUpdateData    //Хранит хп, радиацию
    {
        public int IdEntity { get; set; }
        public string UpdateType { get; set; }
    }
}
