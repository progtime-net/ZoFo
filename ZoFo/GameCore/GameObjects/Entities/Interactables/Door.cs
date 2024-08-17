using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ZoFo.GameCore.GameManagers.CollisionManager;
using ZoFo.GameCore.Graphics;

namespace ZoFo.GameCore.GameObjects.Entities.Interactables;

public class Door : Interactable
{
    public bool isOpened;

    public override AnimatedGraphicsComponent graphicsComponent { get; } = new("DoorClosed");

    public Door(Vector2 position) : base(position)
    {
        //graphicsComponent.OnAnimationEnd += _ => { isOpened = !isOpened; };
    }

    public override void OnInteraction(object sender, CollisionComponent e)
    {
        //graphicsComponent.AnimationSelect("DoorInteraction", isOpened);
        //graphicsComponent.AnimationStep();
    }
}