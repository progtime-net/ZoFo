using System;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates;

namespace ServerOverall.Server.Updates.ClientToServer;

public class UpdateInputShoot : UpdateData
{
    public UpdateInputShoot() { UpdateType = "UpdateInputShoot"; }
    public int PlayerId { get; set; }
}
