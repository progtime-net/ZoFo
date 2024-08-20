using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.NetworkManager.SerializableDTO
{
    [Serializable]
    public class SerializableVector2
    {
        public float X { get; set; }
        public float Y { get; set; }
        public SerializableVector2(Vector2 vector)
        {
            X = vector.X;
            Y = vector.Y;
        }
        public Vector2 GetVector2()
        {
            return new Vector2(X, Y);
        }
    }

    
}
