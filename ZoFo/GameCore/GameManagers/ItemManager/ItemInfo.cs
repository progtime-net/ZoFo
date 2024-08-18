using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.ItemManager
{
    public class ItemInfo
    {
        //поля
        string tag;
        string textureName;
        Texture2D itemTexture;
        bool isCraftable;
        Dictionary<string, int> resourcesNeededToCraft;
        public ItemInfo (string tag)
        {
            this.tag = tag;
        }
        public ItemInfo(string tag,string textureName,bool isCraftable, Dictionary<string, int> resourcesNeededToCraft)
        {
            this.tag = tag;
            this.textureName = textureName;
            
            this.isCraftable = isCraftable;
            this.resourcesNeededToCraft = resourcesNeededToCraft;
        }
        //методы
        public void LoadTexture()
        {
            //я что-то хз как это
            itemTexture=AppManager.Instance.Content.Load<Texture2D>(textureName);
        }
    }
}
