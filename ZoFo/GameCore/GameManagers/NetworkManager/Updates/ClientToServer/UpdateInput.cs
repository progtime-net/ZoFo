using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers.NetworkManager.SerializableDTO;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ClientToServer
{
    public class UpdateInput :UpdateData
    {
    // public int IdEntity { get; set; }
    public SerializableVector2 InputMovementDirection{get;set;}
    public SerializableVector2 InputAttackDirection {get;set;}

    public int PlayerId {get;set;}
    public UpdateInput()
    {
        UpdateType = "UpdateInput";
    }
    
    }
}
