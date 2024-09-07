
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ServerOverall.Server.Updates.ServerToClient;

namespace ServerOverall.Server.Updates
{
    [JsonDerivedType(typeof(UpdateAnimation))]
    [JsonDerivedType(typeof(UpdateEntityHealth))]
    [JsonDerivedType(typeof(UpdateGameEnded))]
    [JsonDerivedType(typeof(UpdateGameObjectCreated))]
    [JsonDerivedType(typeof(UpdateGameObjectDeleted))]
    [JsonDerivedType(typeof(UpdateInteraction))]
    [JsonDerivedType(typeof(UpdateInteractionReady))]
    [JsonDerivedType(typeof(UpdateLoot))]
    [JsonDerivedType(typeof(UpdateGameObjectWithoutIdCreated))]
    [JsonDerivedType(typeof(UpdatePlayerParametrs))]
    [JsonDerivedType(typeof(UpdatePosition))]
    [JsonDerivedType(typeof(UpdateStopObjectCreated))]
    [JsonDerivedType(typeof(UpdateTileCreated))]
    [JsonDerivedType(typeof(UpdateCreatePlayer))]

    public class UpdateData
    {
        public int IdEntity { get; set; } //Id объекта
        public string UpdateType { get; set; }  //тип обновления
        public bool isImportant { get; set; }

        public UpdateData()
        {
            
        }

        public UpdateData(int idEntity)
        {
            this.IdEntity = idEntity;
        }
    }
}
