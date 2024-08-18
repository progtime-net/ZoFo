using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZoFo.GameCore.GameManagers.CollisionManager;

namespace ZoFo.GameCore.GameObjects.Entities
{
    public abstract class Entity : GameObject
    {
        //public override GraphicsComponent graphicsComponent => null;
        public CollisionComponent collisionComponent { get; protected set; }
        public int Id { get; set; }
        static int totalEntitiesCreated = 0;
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
    }
}

