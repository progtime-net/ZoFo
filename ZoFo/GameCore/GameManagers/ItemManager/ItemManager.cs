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
            tagItemPairs.Add("rottenflesh", new ItemInfo("rottenflesh", "БУХАТЬ", "Textures/icons/Collectables/RottenFlesh",false,null));
            tagItemPairs.Add("purebottleofwater", new ItemInfo("purebottleofwater", "БУХАТЬ 2", "Textures/icons/Collectables/PureBottleOfWater",false,null));
            tagItemPairs.Add("bottleofwater", new ItemInfo("bottleofwater", "БУХАТЬ", "Textures/icons/Collectables/BottleOfWater",false,null));
            //tagItemPairs.Add("antiradin", new ItemInfo("antiradin", "радиацию опускает", "Textures/icons/Collectables/Ammo",false,null));
            tagItemPairs.Add("ammo", new ItemInfo("ammo", "деньги в метро", "Textures/icons/Collectables/Ammo",false,null));
            tagItemPairs.Add("wood", new ItemInfo("wood", "бревна кусок", "Textures/icons/Collectables/Wood",false,null));
            tagItemPairs.Add("peeble", new ItemInfo("peeble", "пять галек", "Textures/icons/Collectables/Peeble", false, null));
            tagItemPairs.Add("steel", new ItemInfo("steel", "метал, метал, \nжелезо, метал", "Textures/icons/Collectables/Steel", false, null));
            tagItemPairs.Add("pickaxe", new ItemInfo("pickaxe", "прямой путь к \nстановлению каменьщиком", "Textures/Test/pickaxe", true, new Dictionary<string, int>()
            {
                {"wood", 2},
                {"steel", 3}
            })); 
        }
        
    }
}
