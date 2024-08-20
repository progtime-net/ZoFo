using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GameManagers.CollisionManager;

namespace ZoFo.GameCore.GameObjects
{
    public abstract class Entity : GameObject
    {
        //public override GraphicsComponent graphicsComponent => null;
        public CollisionComponent collisionComponent { get; protected set; }
        public int Id { get; set; }
        static int totalEntitiesCreated = 1;
        protected Entity(Vector2 position) : base(position)
        {
            Id = totalEntitiesCreated;
            totalEntitiesCreated++;
            collisionComponent = new CollisionComponent(this);
        }
        /// <summary>
        /// For initialisation on Client
        /// </summary>
        /// <param name="newId"></param>
        public void SetIdByClient(int newId)
        {
            Id = newId;
        }

        public virtual void Update()
        {
        }
        public override void UpdateLogic()
        {
            Update();
            base.UpdateLogic();
        }

        public void StartAnimation(string animationId)
        {
            if (AppManager.Instance.gamestate == GameState.HostPlaying)
            {
                (graphicsComponent as Graphics.AnimatedGraphicsComponent).StartAnimation(animationId);
                AppManager.Instance.server.AddData(new GameManagers.NetworkManager.Updates.ServerToClient.UpdateAnimation()
                {
                    animationId = animationId,
                    IdEntity = Id
                });
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawDebugRectangle(spriteBatch, collisionComponent.stopRectangle.SetOrigin(position), Color.Orange);

            base.Draw(spriteBatch);
        }

        public virtual void Delete()
        {
            if (AppManager.Instance.gamestate == GameState.HostPlaying)
            {
                AppManager.Instance.server.DeleteObject(this);
            }
        }
    }
}

