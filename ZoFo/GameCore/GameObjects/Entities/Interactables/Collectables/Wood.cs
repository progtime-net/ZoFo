using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GameManagers.CollisionManager;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;
using ZoFo.GameCore.Graphics;

namespace ZoFo.GameCore.GameObjects;

public class Wood : Collectable
{
    public override StaticGraphicsComponent graphicsComponent { get; } = new(_path + "Wood");

    public Wood(Vector2 position) : base(position) { }
}