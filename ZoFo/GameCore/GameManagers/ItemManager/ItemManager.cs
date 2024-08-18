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
        void LoadItemTextures()
        {
            foreach (var item in tagItemPairs)
            {
                item.Value.LoadTexture();
            }
        }
        void Initialize()
        {
            tagItemPairs.Add("Wood", new ItemInfo("Wood","Wood",false,null));
            tagItemPairs.Add("Peeble", new ItemInfo("Peeble", "Peeble", false, null));
            tagItemPairs.Add("Steel", new ItemInfo("Steel", "Steel", false, null));
        }
        
    }
}
