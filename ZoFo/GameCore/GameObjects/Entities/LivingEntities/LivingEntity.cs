using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ZoFo.GameCore.GameObjects.Entities;
using ZoFo.GameCore.ZoFo_graphics;

namespace ZoFo.GameCore.GameObjects.Entities.LivingEntities;
public class LivingEntity : Entity
{
    public Vector2 velocity;

    public LivingEntity(Vector2 position) : base(position)
    {
    }

    public void TextureLoad(SpriteBatch spriteBatch)
    {

    }

}




