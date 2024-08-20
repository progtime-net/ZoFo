using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers; 
using ZoFo.GameCore.GameManagers.CollisionManager;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient; 
using ZoFo.GameCore.GameManagers.AssetsManager; 
using ZoFo.GameCore.Graphics;
using ZoFo.GameCore.GUI;

namespace ZoFo.GameCore.GameObjects
{
    class Zombie : Enemy
    { 
        public override GraphicsComponent graphicsComponent { get; } = new AnimatedGraphicsComponent(new List<string> { "zombie_damaged", "zombie_walk", "zombie_idle", "zombie_attack", "zombie_death" }, "zombie_walk");
        
        public Zombie(Vector2 position) : base(position)
        {
            health = 5;
            speed = 0.5f;
            graphicsComponent.ObjectDrawRectangle = new Rectangle(0, 0, 30, 30);
            collisionComponent.stopRectangle = new Rectangle(10, 20, 10, 10);
            isAttacking = false;
            StartAnimation("zombie_walk");
            collisionComponent.isTrigger = true;
            collisionComponent.hasCollision = true;
            (graphicsComponent as AnimatedGraphicsComponent).actionOfAnimationEnd += (animationIdEnded)=>{
                if (animationIdEnded == "zombie_attack")
                    EndAttack(animationIdEnded);
            };
            collisionComponent.OnTriggerZone += OnPlayerClose;
            collisionComponent.triggerRectangle = new Rectangle(-5, -5, 40, 40);
            (graphicsComponent as AnimatedGraphicsComponent).actionOfAnimationEnd += (str) =>
            {
                if (str == "zombie_death")
                    DeathEnd();
            };
        }

        public override void Update()
        {
            if (isDying) return;
            Vector2 duration = Vector2.Normalize(
                AppManager.Instance.server.players[0].position - position
                );
            
            
           
            
            if (!isAttacking) { velocity += new Vector2(duration.X * speed, duration.Y * speed); }

        }
        public void OnPlayerClose(GameObject sender)
        {
            
           
            if(!isAttacking)
            {
                StartAnimation("zombie_attack");
                isAttacking = true;
            }
            

            
        }
        public void EndAttack(string a)
        {
            if (AppManager.Instance.gamestate == GameState.HostPlaying)
            {
                var damagedPlayers = AppManager.Instance.server.collisionManager.GetPlayersInZone(collisionComponent.triggerRectangle.SetOrigin(position));
                //TODO ДАМАЖИТЬ ИГРОКОВ В ЗОНЕ
                if (damagedPlayers.Length > 0)
                {
                    DebugHUD.DebugLog("End of" + a);
                    foreach (var item in damagedPlayers)
                        item.TakeDamage(5);
                }
                isAttacking = false;
            }
            
        }

        public override void Die()
        {
            StartAnimation("zombie_death");
            base.Die();
        }
        public override void DeathEnd()
        {

            Instantiate(new Particle(collisionComponent.stopRectangle.Location.ToVector2() + position + ExtentionClass.RandomVector() * 20));
            Instantiate(new Particle(collisionComponent.stopRectangle.Location.ToVector2() + position + ExtentionClass.RandomVector() * 20));
            Instantiate(new Particle(collisionComponent.stopRectangle.Location.ToVector2() + position + ExtentionClass.RandomVector() * 20));

            base.DeathEnd();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawDebugRectangle(spriteBatch, collisionComponent.triggerRectangle.SetOrigin(position), Color.Blue);
            base.Draw(spriteBatch);
        }
        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
        }
    }
}
