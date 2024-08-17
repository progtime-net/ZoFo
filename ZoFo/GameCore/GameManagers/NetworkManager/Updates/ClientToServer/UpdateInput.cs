using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ClientToServer
{
    public class UpdateInput :UpdateData
    {
       // public int IdEntity { get; set; }
      public UpdateInput()
        {
            UpdateType = "UpdateInput";
        }
    }
}
