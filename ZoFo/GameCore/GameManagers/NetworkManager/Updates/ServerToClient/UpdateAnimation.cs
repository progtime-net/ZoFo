using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient
{
    /// <summary>
    /// Хранит новое сосотяние анимации
    /// </summary>
    public class UpdateAnimation : UpdateData
    {
        public UpdateAnimation() { UpdateType = "UpdateAnimation"; }
        public string animationId { get; set; }
    }
}
