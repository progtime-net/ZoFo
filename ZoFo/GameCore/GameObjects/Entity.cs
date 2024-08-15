using System;
using System.Numerics;

namespace ZoFo;

public class Entity
{
    Vector2 position;
    public Entity(Vector2 position)
    {
        this.position = position;
    }
    public virtual void SetPosition(Vector2 position)
    {
         Vector2 pos = position; 
    }
}
