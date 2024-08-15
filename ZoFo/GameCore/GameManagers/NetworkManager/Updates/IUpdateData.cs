
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates
{
    public interface IUpdateData
    {
        public int IdEntity { get; set; }   //Id объекта
        public string UpdateType { get; set; } //тип обновления
    }
}
