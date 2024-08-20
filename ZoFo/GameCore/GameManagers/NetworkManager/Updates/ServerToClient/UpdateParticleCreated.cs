
using Microsoft.Xna.Framework;
using ZoFo.GameCore.GameManagers.NetworkManager.SerializableDTO;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient
{
    /// <summary>
    /// Хранит новое сосотяние анимации
    /// </summary>
    public class UpdateGameOBjectWithoutIdCreated : UpdateData
    {
        public UpdateGameOBjectWithoutIdCreated() { UpdateType = "UpdateGameOBjectWithoutIdCreated"; }
        public string GameObjectClassName { get; set; }
        public Vector2 position { get; set; }
    }
}
