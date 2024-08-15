using System;

namespace ZoFo.GameCore.GameObjects;
public class LivingEntity : Entity
{
    public bool isOnGround = true;
    public Vector2 velocity;
    public Vector2 acceleration;
    public Vector2 Acceleration { get; private set; }
    public LivingEntity(Vector2 position) : base(position)
    {
        acceleration = new Vector2(0, 30);
    }
    public override void SetPosition(Vector2 position)
    {
        _pos = position;

    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }
    public virtual void StartCicycleAnimation(string animationName)
    {
        if (GraphicsComponent.GetCurrentAnimation != animationName)
        {
            GraphicsComponent.StartAnimation(animationName);
        }
    }
}
