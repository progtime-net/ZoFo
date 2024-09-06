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
     

    public LivingEntity(Vector2 position) : base(position)
    { 
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

    public override void Update_OnClient()
    {
        base.Update_OnClient();
    }
    protected Vector2 prevPosition_forClient;
    public override void Draw(SpriteBatch spriteBatch)
    {
        if ((positionDraw - prevPosition_forClient).X < 0)
            graphicsComponent.Flip = SpriteEffects.FlipHorizontally;
        else if ((positionDraw - prevPosition_forClient).X > 0)
            graphicsComponent.Flip = SpriteEffects.None;
        base.Draw(spriteBatch);
        prevPosition_forClient = positionDraw;
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




