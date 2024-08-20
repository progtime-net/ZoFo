using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers.NetworkManager.SerializableDTO;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ClientToServer;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient
{

    /// <summary>
    /// При создании тайла
    /// </summary>
    public class UpdateTileCreated : UpdateData
    {
        public UpdateTileCreated() { UpdateType = "UpdateTileCreated"; isImportant = true; }
        [JsonInclude]
        public SerializableVector2 Position { get; set; } 
        public SerializablePoint Size { get; set; }
        public SerializableRectangle sourceRectangle { get; set; }
        public string tileSetName { get; set; }
    }
}
