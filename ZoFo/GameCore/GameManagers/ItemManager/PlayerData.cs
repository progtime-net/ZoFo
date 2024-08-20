using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
        public Dictionary<string, int> items = new Dictionary<string, int>(); 
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
            if (File.Exists("Items.txt"))
            {
                string data;
                using (StreamReader reader = new StreamReader("Items.txt"))
                {
                    data = reader.ReadToEnd();
                }
                
                List<PlayerItemsData> itemsDatas = JsonSerializer.Deserialize<List<PlayerItemsData>>(data);
                foreach (var item in itemsDatas)
                {
                    items.Add(item.Name, item.Count);
                }
            }
        }

        public void SavePlayerData()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            List<PlayerItemsData> playerItemsDatas = new List<PlayerItemsData>();
            foreach (var item in items)
            {
                playerItemsDatas.Add(new PlayerItemsData { Name = item.Key, Count = item.Value });
            }

            string data = JsonSerializer.Serialize<List<PlayerItemsData>>(playerItemsDatas);

            using (StreamWriter outputFile = new StreamWriter("Items.txt", new FileStreamOptions() { Mode = FileMode.Create, Access = FileAccess.Write } ))
            {
                outputFile.WriteLine(data);
            }
        }
    }

    class PlayerItemsData
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
