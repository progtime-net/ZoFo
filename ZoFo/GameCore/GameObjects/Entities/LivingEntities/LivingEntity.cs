using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ZoFo.GameCore.GameObjects.Entities;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GameManagers.CollisionManager;
using ZoFo.GameCore.Graphics;

namespace ZoFo.GameCore.GameObjects;
public class LivingEntity : Entity
{
    /// <summary>
    /// Переменная для заявки на передвижения, т.е. то, на сколько вы хотите, чтобы в этом кадре переместился объект
    /// </summary>
    public Vector2 velocity;

    private InputManager inputManager;

    public LivingEntity(Vector2 position) : base(position)
    {
        inputManager = new InputManager();
        collisionComponent.hasCollision = true;
    }

    public override GraphicsComponent graphicsComponent { get; } = null;

    #region Server side
    /*public override void Update()
    {
        
    }*/
    #endregion

    public void OnCollision(CollisionComponent component)
    {

    }

    public override void UpdateAnimations()
    {
        base.UpdateAnimations();
    }
    Vector2 prevPosition_forClient;
    public override void Draw(SpriteBatch spriteBatch)
    {
        if ((position - prevPosition_forClient).X < 0)
            graphicsComponent.Flip = SpriteEffects.FlipHorizontally;
        else if ((position - prevPosition_forClient).X > 0)
            graphicsComponent.Flip = SpriteEffects.None;
        base.Draw(spriteBatch);
        prevPosition_forClient = position;
    }

    public virtual void Die()
    {
        //deathSound + animationStart
    }
    public virtual void DeathEnd()
    {
        //deathSound + animationStart
        Delete();
    }


}




