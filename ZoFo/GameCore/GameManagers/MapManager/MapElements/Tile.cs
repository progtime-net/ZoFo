using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.MapManager.MapElements
{
    public class Tile
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public List<ObjectGroup> Objectgroup { get; set; }
    }
}
