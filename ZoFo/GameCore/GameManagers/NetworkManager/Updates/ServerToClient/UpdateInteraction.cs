namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;

/// <summary>
/// При попытке взаимодействия с объектом
/// </summary>
public class UpdateInteraction : UpdateData 
{ 
 public UpdateInteraction() { UpdateType = "UpdateInteraction"; } 
    public UpdateInteraction(int id)
    {
        IdEntity = id;
    }

    public int IdEntity { get; set; }
    public string UpdateType { get; set; } 
}