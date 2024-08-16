namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;

public class UpdateInteraction : UpdateData // при попытке взаимодействия с объектом
{
    public int IdEntity { get; set; }
    public string UpdateType { get; set; }
}