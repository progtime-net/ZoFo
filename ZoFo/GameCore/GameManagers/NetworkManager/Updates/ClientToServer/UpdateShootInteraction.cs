using System;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ClientToServer;

public class UpdateInputActiveAction : UpdateData
{
    public UpdateInputActiveAction() { UpdateType = "UpdateInputActiveAction"; }
    public int PlayerId { get; set; }
}
