using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.ItemManager
{
    class EquippableItemInfo : ItemInfo
    {
        bool IsEquiped; //экипирован ли предмет
        public EquippableItemInfo(string tag) : base(tag)
        {
            
        }
    }
}
