using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GameManagers.CollisionManager;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;
using ZoFo.GameCore.Graphics;

namespace ZoFo.GameCore.GameObjects.Entities.Interactables.Collectables;

public class Wood : Collectable
{
    public override GraphicsComponent graphicsComponent { get; } = new(new List<string> { "Wood" }, "Wood");

    public Wood(Vector2 position) : base(position)
    {

    }
    public override void OnInteraction(object sender, CollisionComponent e)
    {
        AppManager.Instance.server.AddData(new UpdateLoot("Wood"));
        AppManager.Instance.server.DeleteObject(this);
    }
}