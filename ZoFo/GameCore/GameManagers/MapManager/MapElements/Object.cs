using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ZoFo.GameCore.GameManagers.MapManager.MapElements
{
    public class Object
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public string Template { get; set; }
        public string Type { get; set; }
    }
}
