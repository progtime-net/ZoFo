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
        public Dictionary<string, ItemInfo> tagItemPairs;
        //методы
        public ItemInfo GetItemInfo(string tag)
        {
            return tagItemPairs[tag];
        }
        public void LoadItemTextures()
        {
            foreach (var item in tagItemPairs)
            {
                item.Value.LoadTexture();
            }
        }
        public void Initialize()
        { 
            tagItemPairs = new Dictionary<string, ItemInfo>();
            tagItemPairs.Add("wood", new ItemInfo("wood", "бревна кусок", "Textures\\Test\\wood",false,null));
            tagItemPairs.Add("rock", new ItemInfo("rock", "пять галек", "Textures\\Test\\rock", false, null));
            tagItemPairs.Add("steel", new ItemInfo("steel", "метал, метал, \nжелезо, метал", "Textures\\Test\\steel", false, null));
            tagItemPairs.Add("pickaxe", new ItemInfo("steel", "прямой путь к \nстановлению каменьщиком", "Textures\\Test\\pickaxe", true, new Dictionary<string, int>()
            {
                {"wood", 2},
                {"steel", 3}
            })); 
        }
        
    }
}
