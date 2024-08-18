using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameObjects.Entities.LivingEntities.Player
{
    class LootData
    {
        public Dictionary<string, int> loots;

        public void AddLoot(string lootName, int quantity)
        {
            loots.Add(lootName, quantity);
        }
    }
}
