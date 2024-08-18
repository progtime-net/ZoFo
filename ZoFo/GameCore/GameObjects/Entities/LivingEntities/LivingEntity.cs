using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ZoFo.GameCore.GameObjects.Entities;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GameManagers.CollisionManager;
using ZoFo.GameCore.Graphics;

namespace ZoFo.GameCore.GameObjects.Entities.LivingEntities;
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
    

}




