using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.ItemManager
{
    /// <summary>
    /// Класс хранит информацю о количестве ресурсов у игрока
    /// </summary>
    public class PlayerData
    {
        public PlayerData()
        {
            LoadPlayerData();
        }
        public Dictionary<string, int> items; 
        /// <summary>
        /// Принимает тэг и крафтит этот объект
        /// </summary>
        /// <param name="itemTag"></param>
        public bool CraftItem(string itemTag)
        {
            Dictionary<string, int> needToCraft = AppManager.Instance.ItemManager.GetItemInfo(itemTag).resourcesNeededToCraft;
            foreach (var item in needToCraft)
            {
                if (items[item.Key] < item.Value)
                {
                    return false;
                }
            }

            foreach (var item in needToCraft)
            {
                items[item.Key] -= item.Value;
            }

            if (items.Keys.Contains(itemTag))
            {
                items[itemTag] += 1;
            }
            else
            {
                items.Add(itemTag, 1);
            }
            return true;
        }
        

        public void LoadPlayerData()
        {
            //TODO
            items = new Dictionary<string, int>();
            items.Add("wood", 5);
            items.Add("steel", 110);
            items.Add("peeble", 6);
        }
    }
}
