using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GameManagers.AssetsManager;
using ZoFo.GameCore.GameManagers.CollisionManager;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ClientToServer;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;
using ZoFo.GameCore.Graphics;
using System.Diagnostics;
using ZoFo.GameCore.GUI;
using System.Runtime.InteropServices;
using System.Linq;

namespace ZoFo.GameCore.GameObjects;

public class Player : LivingEntity
{
    public Player(Vector2 position) : base(position)
    {
        graphicsComponent.ObjectDrawRectangle.Width = 10;
        graphicsComponent.ObjectDrawRectangle.Height = 10;
    }

    public override GraphicsComponent graphicsComponent { get; } = new StaticGraphicsComponent("Content/Textures/icons/Collectables/Pebble");

    internal void HandleNewInput(UpdateInput data)
    { 
    }
}