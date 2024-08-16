using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZoFo.GameCore.GameManagers.CollisionManager;
using ZoFo.GameCore.ZoFo_graphics;

namespace ZoFo.GameCore.GameObjects.Entities
{
    public abstract class Entity : GameObject
    {
        protected override GraphicsComponent graphicsComponent => null;
        public CollisionComponent collisionComponent { get; protected set; }
        public int Id { get; set; }
        protected Entity(Vector2 position) : base(position)
        {
        }
        public virtual void Update()
        {

        }
    }
}

