using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.MapManager.MapElements
{
    public class TileMap
    {
        public bool Infinite { get; set; }
        public int TileHeight { get; set; }
        public int TileWidth { get; set; }
        public List<TileSet> TileSets { get; set; }
    }
}
