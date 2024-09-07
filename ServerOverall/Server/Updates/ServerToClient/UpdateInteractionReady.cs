namespace ServerOverall.Server.Updates.ServerToClient;

/// <summary>
///  При изменении возможности повзаимодействовать с объектом
/// </summary>
/// <param name="idEntity"></param>
/// <param name="isReady"></param>
public class UpdateInteractionReady(int idEntity, bool isReady)
    : UpdateData 
{
    public int IdEntity { get; set; } = idEntity;
    public string UpdateType { get; set; } = "UpdateInteractionReady";
    public bool IsReady { get; set; } = isReady;
}