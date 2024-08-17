using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.ItemManager
{
    class WeaponItemInfo: EquippableItemInfo
    {
        //поля
        float damage;

        public WeaponItemInfo(string tag) : base(tag)
        {
        }
    }
}
