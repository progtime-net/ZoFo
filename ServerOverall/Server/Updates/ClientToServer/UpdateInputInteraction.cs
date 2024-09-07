using System;
using ZoFo.GameCore.GameManagers.NetworkManager;

namespace ServerOverall.Server.Updates.ClientToServer;

/// <summary>
/// уведомляет сервер о том, что игрок взаимодействует
/// </summary>
public class UpdateInputInteraction : UpdateData
{
    public UpdateInputInteraction() { UpdateType = "UpdateInputInteraction"; }
    public int PlayerId { get; set; }

}
