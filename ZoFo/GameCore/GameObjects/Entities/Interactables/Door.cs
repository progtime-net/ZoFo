using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ZoFo.GameCore.GameManagers.CollisionManager;
using ZoFo.GameCore.Graphics;

namespace ZoFo.GameCore.GameObjects.Entities.Interactables;

public class Door : Interactable
{
    public bool isOpened;

    public override GraphicsComponent graphicsComponent { get; } = new(new List<string> { "DoorInteraction" }, "DoorInteraction");

    public Door(Vector2 position) : base(position)
    {
        graphicsComponent.OnAnimationEnd += _ => { isOpened = !isOpened; };//приколько, что через нижнее подчеркивание - SD
    }

    public override void OnInteraction(object sender, CollisionComponent e)
    {
        graphicsComponent.AnimationSelect("DoorInteraction", isOpened);
        graphicsComponent.AnimationStep();
    }
}