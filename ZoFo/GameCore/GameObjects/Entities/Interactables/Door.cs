using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ZoFo.GameCore.ZoFo_graphics;

namespace ZoFo.GameCore.GameObjects.Entities.Interactables;

public class Door : Interactable
{
    public bool isOpened;

    protected override GraphicsComponent graphicsComponent { get; } = new(new List<string> { "DoorInteraction" }, "DoorInteraction");

    public Door(Vector2 position) : base(position)
    {
        graphicsComponent.actionOfAnimationEnd += _ => { isOpened = !isOpened; };
    }

    public override void OnInteraction()
    {
        graphicsComponent.StartAnimation("DoorInteraction", isOpened);
    }
    
    
}