using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers.NetworkManager.SerializableDTO;

namespace ServerOverall.Server.Updates.ServerToClient
{
    internal class UpdateStopObjectCreated : UpdateData
    {
        public UpdateStopObjectCreated() { UpdateType = "UpdateStopObjectCreated"; isImportant = true; }
        public Texture2D TextureTile { get; set; }
        public SerializableVector2 Position { get; set; }
        public SerializablePoint Size { get; set; }
        public SerializableRectangle sourceRectangle { get; set; }
        public string tileSetName { get; set; }
        public SerializableRectangle[] collisions { get; set; }
    }
}
