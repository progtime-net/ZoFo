using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers.NetworkManager.SerializableDTO;
using ZoFo.GameCore.GameObjects.Entities.LivingEntities;

namespace ServerOverall.Server.Updates.ServerToClient
{
    /// <summary>
    /// Хранит новую позицию
    /// </summary>
    public class UpdatePosition : UpdateData
    {
        public UpdatePosition() { UpdateType = "UpdatePosition"; }

        public SerializableVector2 NewPosition { get; set; }
    }
}
