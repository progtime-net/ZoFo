using System;
using ZoFo.GameCore.GameObjects;

namespace ZoFo.GameCore.GameObjects;
public class Entity : GameObject
{
    public int Id{ get; set; }
    //public CollisionComponent collisionComponents{ get; set; }
    //public AnimationComponent animationComponent{ get; set; }

    // в апдейте может заявляет изменения позиции
    public void UpdateLogic()
    {

    }

    

    // Методы для клиента
    public void UpdateAnimation()
    {

    }
    public void Draw(ContentManager manager)
    {

    }
}
