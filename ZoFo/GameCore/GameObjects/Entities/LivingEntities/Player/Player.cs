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
    //public bool IsTryingToShoot { get; set; }
    private float speed;
    private int health;
    public override GraphicsComponent graphicsComponent { get; } = new AnimatedGraphicsComponent(new List<string> { "player_look_down" }, "player_look_down");
    private LootData lootData;
    //public bool isTryingToInteract { get; set; }
    public Player(Vector2 position) : base(position)
    {
        graphicsComponent.ObjectDrawRectangle = new Rectangle(0, 0, 100, 100);
        collisionComponent.stopRectangle = new Rectangle(0, 0, 100, 100); 
        speed = 10;
        //isTryingToInteract = false;
        //IsTryingToShoot = false; 

        StartAnimation("player_look_down"); 
    }


    public  override void Update()
    {

        MovementLogic();
    }
    public void MovementLogic() 
    {
        velocity = InputPlayerRotation * speed; 
    }
    public void HandleNewInput(UpdateInput updateInput)
    {
        InputPlayerRotation = updateInput.InputMovementDirection;
        InputWeaponRotation = updateInput.InputAttackDirection;

    }
    public void HandleInteract(UpdateInputInteraction updateInputInteraction)
    {
        //isTryingToInteract = true;
    }
    public void HandleShoot(UpdateInputShoot updateInputShoot)
    {

    }
}
