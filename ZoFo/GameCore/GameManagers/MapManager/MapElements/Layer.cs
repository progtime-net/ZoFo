using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.MapManager.MapElements
{
    public class Layer
    {
        public List<Chunk> Chunks { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Id { get; set; }
        public bool Visibility { get; set; }
        public string Class { get; set; }
    }
}
