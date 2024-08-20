using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GameManagers.CollisionManager;
using ZoFo.GameCore.GameObjects.MapObjects.StopObjects;

namespace ZoFo.GameCore.GameObjects.Entities.LivingEntities.Player.PlayerAttacks
{
    internal class SwordAttack : IPlayerWeaponAttack
    {
        Rectangle rectangle;
        public SwordAttack(){
        }
        public Rectangle Attack(Vector2 position){
            rectangle = new Rectangle((int)position.X, (int)position.Y, 30, 10);

            return rectangle;
        }
    }
}
