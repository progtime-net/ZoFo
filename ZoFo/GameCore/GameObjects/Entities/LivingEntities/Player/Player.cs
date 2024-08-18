using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GameManagers.CollisionManager;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ClientToServer;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;
using ZoFo.GameCore.Graphics;

namespace ZoFo.GameCore.GameObjects.Entities.LivingEntities.Player;

public class Player : LivingEntity
{
    public Vector2 InputWeaponRotation { get; set; }
    public Vector2 InputPlayerRotation { get; set; }
    /// <summary>
    /// Факт того, что плеер в этом апдейте пытается стрелять
    /// </summary>
    public bool IsTryingToShoot { get; set; }
    private float speed;
    private int health;
    public override GraphicsComponent graphicsComponent { get; } = new AnimatedGraphicsComponent(new List<string> { "player_running_top_rotate" }, "player_running_top_rotate");
    private LootData lootData;
    public Player(Vector2 position) : base(position)
    {
        //InputWeaponRotation = new Vector2(0, 0);
        //InputPlayerRotation = new Vector2(0, 0);
        graphicsComponent.ObjectDrawRectangle = new Rectangle(0, 0, 100, 100);
        collisionComponent.stopRectangle = new Rectangle(0, 0, 100, 100);
    }


    public  override void Update()
    {

        MovementLogic();
    }
    float t;
    public void MovementLogic()
    {
        //velocity.X = 3+(float)Math.Sin(t);
        t++;
        if (InputPlayerRotation.X > 0.9)
        {
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D)) velocity.X = 5;
        if (Keyboard.GetState().IsKeyDown(Keys.A)) velocity.X = -5;
        if (Keyboard.GetState().IsKeyDown(Keys.S)) velocity.Y = 5;
        if (Keyboard.GetState().IsKeyDown(Keys.W)) velocity.Y = -5;
    }
    public void HandleNewInput(UpdateInput updateInput)
    {

    }
}
