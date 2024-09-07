using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerOverall.Server.Updates.ServerToClient
{
    /// <summary>
    /// Хранит объект, который надо удлить
    /// </summary>
    public class UpdateGameObjectDeleted : UpdateData    
    {
        public UpdateGameObjectDeleted() { UpdateType = "UpdateGameObjectDeleted";  isImportant = false; }
        public string GameObjectType { get; set; }
    }
}