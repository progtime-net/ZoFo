using System;
using ZoFo.GameCore.GameManagers.NetworkManager;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ClientToServer;

/// <summary>
/// уведомляет сервер о том, что игрок взаимодействует
/// </summary>
public class UpdateInputInteraction : UpdateData
{
    public UpdateInputInteraction() { UpdateType = "UpdateInputInteraction"; }
}
