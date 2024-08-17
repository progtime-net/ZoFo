using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ZoFo.GameCore.Graphics;

namespace ZoFo.GameCore.GameObjects.Entities.Interactables.Collectables;

public class Wood : Collectable
{
    public override StaticGraphicsComponent graphicsComponent { get; } = new("Wood");

    public Wood(Vector2 position) : base(position)
    {
    }
}