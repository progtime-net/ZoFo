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
        public void CollisionComponent()
        {

        }

        public void AnimationComponent()
        {

        }

        public void UpdateLogic()
        {

        }

    }
}

//вектор
//вилосити
//поситион
//текстура
