using ZoFo.GameCore.GameObjects;

namespace ZoFo.GameCore.GameManagers.MapManager
{
    public class MapManagerProto
    {
        public virtual void LoadMap(string mapName = "main")
        {

        }
        public virtual void RegisterGameObject(GameObject gameObject)
        {
            AppManager.Instance.server.RegisterGameObject(gameObject);
        }
    }
}