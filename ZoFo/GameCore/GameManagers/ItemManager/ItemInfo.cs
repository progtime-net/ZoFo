using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.ItemManager
{
    class ItemInfo
    {
        //поля
        string tag;
        string textureName;
        Texture2D itemTexture;
        bool isCraftable;
        Dictionary<string, int> resourcesNeededToCraft;
        //методы
        private void LoadTexture()
        {
            //я что-то хз как это
        }
    }
}
