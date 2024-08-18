using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ZoFo.GameCore.GameManagers.CollisionManager;
using ZoFo.GameCore.Graphics;

namespace ZoFo.GameCore.GameObjects.Entities.Interactables;

public class Door : Interactable
{
    public bool isOpened;

    public override StaticGraphicsComponent graphicsComponent { get; } = new("DoorInteraction");

    public Door(Vector2 position) : base(position)
    {
        //graphicsComponent.OnAnimationEnd += _ => { isOpened = !isOpened; };//���������, ��� ����� ������ ������������� - SD
    }

    public override void OnInteraction(GameObject sender)
    {
        //graphicsComponent.AnimationSelect("DoorInteraction", isOpened);
        //graphicsComponent.AnimationStep();
    }
}