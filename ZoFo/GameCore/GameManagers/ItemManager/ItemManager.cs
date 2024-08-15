using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.ItemManager
{
    class ItemManager
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
            tagItemPairs.Add("wood", new ItemInfo("wood","wood",false,null));
            tagItemPairs.Add("rock", new ItemInfo("rock", "rock", false, null));
            tagItemPairs.Add("steel", new ItemInfo("steel", "steel", false, null));
        }
        
    }
}
