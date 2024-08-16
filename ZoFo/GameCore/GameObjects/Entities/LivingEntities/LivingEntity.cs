using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ZoFo.GameCore.GameObjects.Entities;
using ZoFo.GameCore.ZoFo_graphics;
using ZoFo.GameCore.GameManagers;

namespace ZoFo.GameCore.GameObjects.Entities.LivingEntities;
public class LivingEntity : Entity
{
    public Vector2 velocity;

    private InputManager inputManager;

    public LivingEntity(Vector2 position) : base(position)
    {
        inputManager = new InputManager();
    }

    #region Server side
    /*public override void Update()
    {
        
    }*/
    #endregion

}




