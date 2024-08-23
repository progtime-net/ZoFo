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
            if (tag == "peeble")
                return tagItemPairs["pebble"];
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
            tagItemPairs.Add("rottenflesh", new ItemInfo("rottenflesh", "гниль", "Textures/icons/Collectables/RottenFlesh",false,null));
            tagItemPairs.Add("bottleofwater", new ItemInfo("bottleofwater", "грязная водка", "Textures/icons/Collectables/BottleOfWater",false,null));
            //tagItemPairs.Add("antiradin", new ItemInfo("antiradin", "радиацию опускает", "Textures/icons/Collectables/Ammo",false,null));
            tagItemPairs.Add("ammo", new ItemInfo("ammo", "деньги в метро", "Textures/icons/Collectables/Ammo",false,null));
            tagItemPairs.Add("wood", new ItemInfo("wood", "бревна кусок", "Textures/icons/Collectables/Wood",false,null));
            tagItemPairs.Add("fabric", new ItemInfo("fabric", "смотя какой fabric", "Textures/icons/Material/Fabric",false,null));
            tagItemPairs.Add("paper", new ItemInfo("paper", "бумага", "Textures/icons/Material/Paper",false,null));
            tagItemPairs.Add("pebble", new ItemInfo("pebble", "пять галек", "Textures/icons/Collectables/Pebble", false, null));
            tagItemPairs.Add("steel", new ItemInfo("steel", "метал, метал, \nжелезо, метал", "Textures/icons/Collectables/Steel", false, null));
            tagItemPairs.Add("pickaxe", new ItemInfo("pickaxe", "прямой путь к \nстановлению каменьщиком", "Textures/Test/pickaxe", true, new Dictionary<string, int>()
            {
                {"wood", 2},
                {"steel", 3}
            })); 
            tagItemPairs.Add("plank", new ItemInfo("plank", "проосто доска", "Textures/icons/Material/Wooden Plank", true, new Dictionary<string, int>()
            {
                {"wood", 1}
            })); 
            tagItemPairs.Add("gear", new ItemInfo("gear", "настройки", "Textures/icons/Misc/Gear", true, new Dictionary<string, int>()
            {
                {"steel", 3}
            })); 
            tagItemPairs.Add("purebottleofwater", new ItemInfo("purebottleofwater", "чистая водка", "Textures/icons/Collectables/PureBottleOfWater", true, new Dictionary<string, int>()
            {
                {"bottleofwater", 1},
                {"wood", 2}
            })); 
            tagItemPairs.Add("leather", new ItemInfo("leather", "кожа", "Textures/icons/Material/Leather", true, new Dictionary<string, int>()
            {
                {"rottenflash", 1},
                {"wood", 2}
            })); 
            tagItemPairs.Add("crate", new ItemInfo("crate", "коробка", "Textures/icons/Misc/Crate", true, new Dictionary<string, int>()
            {
                {"plank", 5}
            })); 
            tagItemPairs.Add("book", new ItemInfo("book", "книга", "Textures/icons/Misc/Book", true, new Dictionary<string, int>()
            {
                {"leather", 2},
                {"paper", 5}
            })); 
        }
        
    }
}
