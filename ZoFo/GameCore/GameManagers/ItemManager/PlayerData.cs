using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.ItemManager
{
    /// <summary>
    /// Класс хранит информацю о количестве ресурсов у игрока
    /// </summary>
    internal class PlayerData
    {
        Dictionary<string, int> items; 
        /// <summary>
        /// Принимает тэг и крафтит этот объект
        /// </summary>
        /// <param name="itemTag"></param>
        public void CraftItem(string itemTag)
        {
            //TODO
        }
    }
}
