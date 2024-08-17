using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ClientToServer;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient
{

    /// <summary>
    /// При создании тайла
    /// </summary>
    public class UpdateTileCreated : UpdateData
    {
        public UpdateTileCreated() { UpdateType = "UpdateTileCreated"; }
        public Texture2D TextureTile { get; set; }
        public Vector2 Position { get; set; }
        public Point Size { get; set; }
        public Rectangle sourceRectangle { get; set; }
        public string tileSetName { get; set; }
    }
}
