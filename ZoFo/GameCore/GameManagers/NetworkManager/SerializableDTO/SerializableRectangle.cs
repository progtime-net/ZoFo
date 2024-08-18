using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers.NetworkManager.SerializableDTO;

namespace ZoFo.GameCore.GameManagers.NetworkManager.SerializableDTO
{
    [Serializable]
    [JsonSerializable(typeof(SerializableRectangle))]
    public class SerializableRectangle
    {
        public SerializablePoint Size { get; set; }
        public SerializablePoint Location { get; set; }
        public SerializableRectangle()
        {

        }

        public SerializableRectangle(Rectangle rectangle) { Size = new SerializablePoint(rectangle.Size); Location = new SerializablePoint(rectangle.Location); }

        public Rectangle GetRectangle()
        {
            return new Rectangle(Location.GetPoint(), Size.GetPoint());
        }
    }
}
