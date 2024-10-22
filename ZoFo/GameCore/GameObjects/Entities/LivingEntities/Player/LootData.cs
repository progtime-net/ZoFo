﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;
using ZoFo.GameCore.GUI;

namespace ZoFo.GameCore.GameObjects
{
    public class LootData
    {
        public Dictionary<string, int> loots;

        public void AddLoot(string lootName, int quantity, int id)
        {
            if (lootName == null)
            {
                DebugHUD.DebugLog("PROBLEM, loot is null");
                DebugHUD.DebugLog("PROBLEM, loot is null");
                DebugHUD.DebugLog("PROBLEM, loot is null");
                return;
            }
            AppManager.Instance.server.AddData(new UpdateLoot(lootName, quantity, id));

            if (loots.ContainsKey(lootName))
                loots[lootName] +=quantity;
            else
                loots.Add(lootName, quantity);
        }
        public void AddLoot_Client(string lootName, int quantity)
        {
            if (loots.ContainsKey(lootName))
                loots[lootName] +=quantity;
            else
                loots.Add(lootName, quantity);
        }
    }
}
