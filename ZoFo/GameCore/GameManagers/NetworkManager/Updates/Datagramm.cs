using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates
{
    public class Datagramm
    {
        public int DatagrammId { get; set; }
        public bool isImportant { get; set; }
        public List<UpdateData> updateDatas { get; set; }
    }
}
