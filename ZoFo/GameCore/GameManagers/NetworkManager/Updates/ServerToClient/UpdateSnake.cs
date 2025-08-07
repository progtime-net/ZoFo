using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers.NetworkManager.SerializableDTO;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient
{
    /// <summary>
    /// Хранит новое сосотяние анимации
    /// </summary>
    public class UpdateSnake : UpdateData
    {
        public UpdateSnake() { UpdateType = "UpdateSnake"; }
        public List<SerializableVector2> deltas { get; set; } = new List<SerializableVector2>();
    }
}
