using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers.NetworkManager.SerializableDTO;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient
{
    internal class UpdateStopObjectCreated : UpdateData
    {
        public UpdateStopObjectCreated() { UpdateType = "UpdateStopObjectCreated"; }
        public Texture2D TextureTile { get; set; }
        public Vector2 Position { get; set; }
        public SerializablePoint Size { get; set; }
        public SerializableRectangle sourceRectangle { get; set; }
        public string tileSetName { get; set; }
        public SerializableRectangle[] collisions { get; set; }
    }
}
