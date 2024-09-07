using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers.NetworkManager.SerializableDTO;

namespace ServerOverall.Server.Updates.ServerToClient
{
    /// <summary>
    /// Хранит объект, который только отправили
    /// </summary>
    public class UpdateGameObjectCreated : UpdateData     
    {
        public UpdateGameObjectCreated() { UpdateType = "UpdateGameObjectCreated"; isImportant = true; }

        public string GameObjectType { get; set; }
        
        public string GameObjectId { get; set; }

        public SerializableVector2 position { get; set; }
    }
}
