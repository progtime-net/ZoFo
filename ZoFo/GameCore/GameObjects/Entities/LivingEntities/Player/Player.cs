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
        collisionComponent.stopRectangle = new Rectangle(0, 20, 30, 10); 
        speed = 5; 

        StartAnimation("player_look_down"); 
    }


    public  override void Update()
    {
        #region анимация управления, стрельбы 
        switch(AppManager.Instance.InputManager.ConvertVector2ToState(InputPlayerRotation))
        {
            case ScopeState.Top:
                
                break;
            case ScopeState.Down:

            break;
            case ScopeState.Right:
                //StartAnimation("player_running_top_rotate");
            break;
            case ScopeState.Left:
                
            break;
            case ScopeState.TopRight:
                
            break;
            case ScopeState.TopLeft:
                
            break;
            case ScopeState.DownRight:
                
            break;
            case ScopeState.DownLeft:
                
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

        Rectangle rectangle = new Rectangle((int)position.X, (int)position.Y, 200, 200);
        Entity[] entities = AppManager.Instance.server.collisionManager.GetEntities(rectangle);
        DebugHUD.DebugSet("ent[0]", entities[0].ToString());
        if(entities != null){
            foreach (Entity entity in entities){
                AppManager.Instance.server.DeleteObject(entity);
            }
        }
    }
}