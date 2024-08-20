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

namespace ZoFo.GameCore.GameObjects;

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

    public override GraphicsComponent graphicsComponent { get; } = new AnimatedGraphicsComponent(AppManager.Instance.AssetManager.Player);

    private LootData lootData;
    public bool IsTryingToInteract { get; set; }
    public bool IsTryingToShoot { get; set; }
    public Player(Vector2 position) : base(position)
    {
        graphicsComponent.ObjectDrawRectangle = new Rectangle(0, 0, 30, 30);
        collisionComponent.stopRectangle = new Rectangle(0, 15, 30, 15); 
        speed = 2.5f; 

        StartAnimation("player_look_down"); 
    }


    public  override void Update()
    {
        #region анимация управления, стрельбы 
        switch(AppManager.Instance.InputManager.ConvertVector2ToState(InputPlayerRotation))
        {
            case ScopeState.Top:
                if  ((graphicsComponent as AnimatedGraphicsComponent).CurrentAnimation.TextureName!="player_run_up")
                    (graphicsComponent as AnimatedGraphicsComponent).StartCyclingAnimation("player_run_up");
                break;
            case ScopeState.Down:
                StartAnimation("player_run_down");
            break;
            case ScopeState.Right:
                StartAnimation("player_run_right");
            break;
            case ScopeState.Left:
                StartAnimation("left");
            break;
            case ScopeState.TopRight:
                StartAnimation("player_run_right_up");
            break;
            case ScopeState.TopLeft:
                StartAnimation("player_run_left_up");
            break;
            case ScopeState.DownRight:
                StartAnimation("player_run_right_down");
            break;
            case ScopeState.DownLeft:
                StartAnimation("player_run_left_down");
            break;
        }
        #endregion
        MovementLogic();
    }
    public void MovementLogic() 
    {
        velocity += InputPlayerRotation * speed; 
    }
    public void HandleNewInput(UpdateInput updateInput)
    {
        InputPlayerRotation = updateInput.InputMovementDirection;
        InputWeaponRotation = updateInput.InputAttackDirection;

    }
    public void HandleInteract(UpdateInputInteraction updateInputInteraction)
    {
        IsTryingToInteract = true;
    }
    public void HandleShoot(UpdateInputShoot updateInputShoot)
    {
        IsTryingToShoot = true;

        var rect = collisionComponent.stopRectangle.SetOrigin(position);
        rect.Width += 100;
        rect.Height += 100;
        Entity[] entities = AppManager.Instance.server.collisionManager.GetEntities(rect, this);
        if (entities != null)
        {
            foreach (Entity entity in entities)
            {
                AppManager.Instance.server.DeleteObject(entity);
            }
        }
    }
}