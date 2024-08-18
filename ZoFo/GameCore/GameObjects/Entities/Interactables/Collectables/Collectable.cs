using Microsoft.Xna.Framework;
using System;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GameManagers.CollisionManager;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;

namespace ZoFo.GameCore.GameObjects.Entities.Interactables.Collectables;
public class Collectable : Interactable
{
    protected static readonly string _path = "Textures/icons/Collectables/";
    public Collectable(Vector2 position) : base(position)
    {
    }

    public override void OnInteraction(object sender, CollisionComponent e)
    {
        // 
        AppManager.Instance.server.AddData(new UpdateLoot());
        AppManager.Instance.server.DeleteObject(this);
    }
}
