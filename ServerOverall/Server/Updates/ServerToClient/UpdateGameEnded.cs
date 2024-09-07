using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerOverall.Server.Updates.ServerToClient
{
    /// <summary>
    /// Хранит полученый лут и уведомляет о конце игры
    /// </summary>
    public class UpdateGameEnded : UpdateData   
    {
      public UpdateGameEnded() { UpdateType = "UpdateGameEnded"; }
    }
}
