using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerOverall.Server.Updates.ServerToClient
{
    /// <summary>
    /// Обнивляет хп сущности
    /// </summary>
    public class UpdateEntityHealth : UpdateData
    {
       public UpdateEntityHealth() { UpdateType = "UpdateEntityHealth"; }
    }
}
