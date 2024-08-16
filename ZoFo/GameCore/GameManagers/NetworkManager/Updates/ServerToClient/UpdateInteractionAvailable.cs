namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;

public class UpdateInteractionAvailable(int idEntity, bool isReady)
    : IUpdateData // при изменении возможности повзаимодействовать с объектом
{
    public int IdEntity { get; set; } = idEntity;
    public string UpdateType { get; set; }
    public bool IsReady { get; set; } = isReady;
}