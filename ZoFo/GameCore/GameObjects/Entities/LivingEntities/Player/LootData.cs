using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameObjects
{
    public class LootData
    {
        public Dictionary<string, int> loots;

        public void AddLoot(string lootName, int quantity)
        {
            if (loots.ContainsKey(lootName))
                loots[lootName] +=quantity;
            else
                loots.Add(lootName, quantity);
        }
    }
}
