using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.Graphics;

namespace ZoFo.GameCore.GameObjects;
public class Enemy : LivingEntity
{
    protected float speed;
    protected float health = 5;
    public bool isAttacking;
    public bool isDying;

    public Enemy(Vector2 position) : base(position)
    {
    }
    public override void Update()
    {

    }
    public virtual void TakeDamage(float damage)
    {
        if (isDying) return;
        health -= damage;
        if (health < 0)
            Die();
    }
    public override void Die()
    {
        isDying = true;
        base.Die();
    }
    public override void DeathEnd()
    {
        Delete();
    }
}