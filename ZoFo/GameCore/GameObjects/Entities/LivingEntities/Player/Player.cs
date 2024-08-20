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

namespace ZoFo.GameCore.GameObjects;

public class Player : LivingEntity
{ 
    public Vector2 InputWeaponRotation { get; set; }
    public Vector2 InputPlayerRotation { get; set; }
    
    private float speed; 
    public int reloading; 
    public float health= 100;
    public float MaxHealth = 100;
 
    public override GraphicsComponent graphicsComponent { get; } = new AnimatedGraphicsComponent(AppManager.Instance.AssetManager.Player.Animations, AppManager.Instance.AssetManager.Player.IdleAnimation);

    public AnimatedGraphicsComponent animatedGraphicsComponent => graphicsComponent as AnimatedGraphicsComponent;
 
    public float rad = 0;
    public float MaxRad = 100;
    public LootData lootData;
    
  

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
        collisionComponent.stopRectangle = new Rectangle(0, 15, 30, 15); 
        speed = 2.5f; 

        StartAnimation("player_look_down"); 
    }


    public  override void Update()
    { 
        #region название current текстуры
            var idName = animatedGraphicsComponent.CurrentAnimation.Id;
        #endregion
        
        #region анимация управления подбора лута
        DebugHUD.DebugSet("texture name", idName); 
        if (reloading>0)
        {
            reloading--;

        }
 
        switch(AppManager.Instance.InputManager.ConvertVector2ToState(InputPlayerRotation))
        {
            case ScopeState.Top:
                if  (idName!="player_run_up")
                    StartAnimation("player_run_up");
                break;
            case ScopeState.Down:
                if  (idName!="player_run_down")
                    StartAnimation("player_run_down");
                break;
            case ScopeState.Right:
            case ScopeState.Left:
                if  (idName!="player_run_right")
                    StartAnimation("player_run_right");
                break;
            case ScopeState.TopRight:
            case ScopeState.TopLeft:
                if  (idName!="player_run_right_up")
                    StartAnimation("player_run_right_up");
            break;
            case ScopeState.DownRight:
            case ScopeState.DownLeft:
                if  (idName!="player_run_right_down")
                    StartAnimation("player_run_right_down");
            break;
            case ScopeState.Idle:
                if (idName!="player_look_down")
                    StartAnimation("player_look_down");
            break;
        }
        #endregion
        
        #region анимация поворота оружия
            int currentAttackSection = AppManager.Instance.InputManager.ConvertAttackVector2ToState(InputWeaponRotation);
            switch(currentAttackSection)
            {
                case 0 or 1:
                    //right
                break;
                case 2 or 3:
                    //down_right_right
                break;
                case 4 or 5:
                    //down_right
                break;
                case 6 or 7:
                    //down_right_left
                break;
                case 8 or 9:
                    //down
                break;
                case 10 or 11:
                    //down_left_right
                break;
                case 12 or 13:
                    //down_left
                break;
                case 14 or 15:
                    //down_left_left
                break;
                case 16 or -14:
                    //left
                break;
                case -13 or -12:
                    //top_left_left
                break;
                case -11 or -10:
                    //top_left
                break;
                case -9 or -8:
                    //top_left_right
                break;
                case -7 or -6:
                    //top
                break;
                case -5 or -4:
                    //top_right_left
                break;
                case -3 or -2:
                    //top_right
                break;
                case -1 or 0:
                    //top_right_right
                break;
            }
        #endregion

        MovementLogic();
    }
    public void WeaponAttack(){

    }
    public void MovementLogic() 
    {
        velocity += InputPlayerRotation * speed;
        DebugHUD.DebugSet("player pos server", position.ToString());
    }
    public void HandleNewInput(UpdateInput updateInput)
    {
        InputPlayerRotation = updateInput.InputMovementDirection.GetVector2();
        InputWeaponRotation = updateInput.InputAttackDirection.GetVector2();

    }
    public void HandleInteract(UpdateInputInteraction updateInputInteraction)
    {
        IsTryingToInteract = true;
    }

    #region MyRegion

    public bool isDying;
    public virtual void TakeDamage(float damage)
    {
        if (isDying) return;
        health -= damage;
        AppManager.Instance.server.AddData(new UpdatePlayerParametrs() { health = health, radiatoin = rad, IdEntity = Id });
        if (health < 0)
            Die();
    }
    public override void Die()
    {
        base.Die();
    }

	#endregion    
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
    public override void Draw(SpriteBatch spriteBatch)
    {
        //DrawDebugRectangle(spriteBatch, collisionComponent.stopRectangle.SetOrigin(position + new Vector2(10,10)), Color.Green);
        base.Draw(spriteBatch);
    }
}