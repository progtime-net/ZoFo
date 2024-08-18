using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.MapManager.MapElements
{
    public class TileSet
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public int ImageHeight { get; set; }
        public int ImageWidth { get; set; }
        public int Margin { get; set; }
        public int Spacing { get; set; }
        public int TileCount { get; set; }
        public int TileHeight { get; set; }
        public int TileWidth { get; set; }
        public int Columns { get; set; }
        public int FirstGid { get; set; }
        public List<Tile> Tiles { get; set; }
    }
}
