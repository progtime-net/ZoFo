using Microsoft.Xna.Framework;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GameManagers.CollisionManager;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;
using ZoFo.GameCore.GameObjects.Entities.LivingEntities.Player;
using ZoFo.GameCore.Graphics;

namespace ZoFo.GameCore.GameObjects.Entities.Interactables;

public class Interactable : Entity
{
    public override StaticGraphicsComponent graphicsComponent => throw new System.NotImplementedException();

    public Interactable(Vector2 position) : base(position)
    {
        collisionComponent.isTrigger = true;
        collisionComponent.hasCollision = false;
        collisionComponent.OnTriggerEnter += (sender) => ChangeInteraction(sender, true);
        collisionComponent.OnTriggerExit += (sender) => ChangeInteraction(sender, false);
        collisionComponent.OnTriggerZone += OnInteraction;
    }

    private void ChangeInteraction(GameObject sender, bool isReady)
    {
        AppManager.Instance.server.AddData(new UpdateInteractionReady((sender as Player).Id, isReady));
    }

    public virtual void OnInteraction(GameObject sender)
    {
        
    }
}