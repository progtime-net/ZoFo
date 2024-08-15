using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.ItemManager
{
    public class ItemManager
    {
        //поля
        Dictionary<string, ItemInfo> tagItemPairs;
        //методы
        ItemInfo GetItemInfo(string tag)
        {
            return tagItemPairs.GetValueOrDefault(tag);
        }
        void LoadItemTexture()
        {

        }
        
    }
}
