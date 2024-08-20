
using Microsoft.Xna.Framework;
using ZoFo.GameCore.GameManagers.NetworkManager.SerializableDTO;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient
{
    /// <summary>
    /// Хранит новое сосотяние анимации
    /// </summary>
    public class UpdateGameObjectWithoutIdCreated : UpdateData
    {
        public UpdateGameObjectWithoutIdCreated() { UpdateType = "UpdateGameObjectWithoutIdCreated"; }
        public string GameObjectClassName { get; set; }
        public SerializableVector2 position { get; set; }
    }
}
