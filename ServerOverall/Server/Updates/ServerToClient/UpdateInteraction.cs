namespace ServerOverall.Server.Updates.ServerToClient;

/// <summary>
/// При попытке взаимодействия с объектом
/// отправляет пользователю разрешение на взаимодействие
/// TODO: Вероятно убрать(обсудить)
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