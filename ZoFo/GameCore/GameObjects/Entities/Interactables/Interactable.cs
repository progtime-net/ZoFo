using Microsoft.Xna.Framework;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GameManagers.CollisionManager;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;
using ZoFo.GameCore.GameObjects.Entities.LivingEntities.Player;
using ZoFo.GameCore.ZoFo_graphics;

namespace ZoFo.GameCore.GameObjects.Entities.Interactables;

public class Interactable : Entity
{
    public override GraphicsComponent graphicsComponent => throw new System.NotImplementedException();

    public Interactable(Vector2 position) : base(position)
    {
        collisionComponent.OnTriggerEnter += (sender, e) => ChangeInteraction(sender, e, true);
        collisionComponent.OnTriggerExit += (sender, e) => ChangeInteraction(sender, e, false);
    }

    private void ChangeInteraction(object sender, CollisionComponent e, bool isReady)
    {
        AppManager.Instance.server.AddData(new UpdateInteractionReady((sender as Player).Id, isReady));
    }

    public virtual void OnInteraction()
    {
        
    }
}