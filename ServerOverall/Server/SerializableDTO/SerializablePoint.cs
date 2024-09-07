using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.NetworkManager.SerializableDTO
{
    public class SerializablePoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public SerializablePoint(Point point) { X = point.X; Y = point.Y;}
        public SerializablePoint() { }
        public Point GetPoint() { return new Point(X, Y);}
    }
}
