
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ClientToServer;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates
{
    [JsonDerivedType(typeof(UpdateAnimation))]
    [JsonDerivedType(typeof(UpdateEntityHealth))]
    [JsonDerivedType(typeof(UpdateGameEnded))]
    [JsonDerivedType(typeof(UpdateGameObjectCreated))]
    [JsonDerivedType(typeof(UpdateLoot))]
    [JsonDerivedType(typeof(UpdatePlayerParametrs))]
    [JsonDerivedType(typeof(UpdatePosition))]
    [JsonDerivedType(typeof(UpdateInput))]
    [JsonDerivedType(typeof(UpdatePlayerExit))]
    public interface IUpdateData
    {
        public int IdEntity { get; set; }   //Id объекта
        public string UpdateType { get; set; } //тип обновления
    }
}
