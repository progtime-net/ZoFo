using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ClientToServer
{
    public class UpdateInput :UpdateData
    {
    // public int IdEntity { get; set; }
    public Vector2 InputMovementDirection{get;set;}
    public Vector2 InputAttackDirection {get;set;}
    public UpdateInput()
    {
        UpdateType = "UpdateInput";
    }
    
    }
}
