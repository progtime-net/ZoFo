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
    
    private float speed; 
    public int reloading; 
    public int health = 100;
    public int rad = 0;
    public LootData lootData;
    
 
    public override GraphicsComponent graphicsComponent { get; } = new AnimatedGraphicsComponent(AppManager.Instance.AssetManager.Player);

    public bool IsTryingToInteract { get; set; }

    /// <summary>
    /// Факт того, что плеер в этом апдейте пытается стрелять
    /// </summary>
    public bool IsTryingToShoot { get; set; }
    public Player(Vector2 position) : base(position)
    {
        lootData = new LootData();
        lootData.loots = new Dictionary<string, int>();
        graphicsComponent.ObjectDrawRectangle = new Rectangle(0, 0, 30, 30);
        collisionComponent.stopRectangle = new Rectangle(0, 20, 30, 10); 
        speed = 5; 

        StartAnimation("player_look_down"); 
    }


    public  override void Update()
    {
        if (reloading>0)
        {
            reloading--;

        }

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
        if (reloading > 0)
            return;
        reloading = 5;
        IsTryingToShoot = true;

        var rect = collisionComponent.stopRectangle.SetOrigin(position);
        rect.Width += 100;
        rect.Height += 100;
        Entity[] entities = AppManager.Instance.server.collisionManager.GetEntities(rect, this);
            AppManager.Instance.server.RegisterGameObject(new Particle(rect.Location.ToVector2()));
        if (entities.Length>0)
        {
            DebugHUD.DebugSet("ent[0]", entities[0].ToString());
            if (entities != null)
            {
                foreach (Entity entity in entities)
                {
                    if (entity is Enemy)
                    {
                        (entity as Enemy).TakeDamage(1);
                    }
                }
            }
        }
    }
}