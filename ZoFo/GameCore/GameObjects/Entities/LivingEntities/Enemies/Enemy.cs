using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace ZoFo.GameCore.GameObjects;
public class Enemy : LivingEntity
{
    protected float speed;
    protected int health;
    public Enemy(Vector2 position) : base(position)
    {
    }
    public override void Update()
    {

        
    }
}