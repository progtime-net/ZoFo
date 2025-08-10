using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers.CollisionManager;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.Graphics;
using Microsoft.Xna.Framework.Graphics;
using ZoFo.GameCore.GUI;

namespace ZoFo.GameCore.GameObjects
{
  class Energy : Collectable 
  {
        public override StaticGraphicsComponent graphicsComponent { get; } = new(_path + "Energy");

        public Energy(Vector2 position) : base(position) {
            

            
        }
        public override void OnInteraction(GameObject sender)
        {

            DebugHUD.DebugLog("collected"); 
             
            Delete();
            //AppManager.Instance.server.DeleteObject(this);
            Create();
            (sender as Snake).Eat(this);

        }
        public static void Create()
        {
            AppManager.Instance.server
                .RegisterGameObject(new Energy(new Vector2(Random.Shared.Next(-1330, -90), Random.Shared.Next(-840, 300))));

        }

    }
    public class SnakeInteractions
    {
        public static void CheckSnakeInteractions(List<Snake> snakes)
        {
            for (int i = 0; i < snakes.Count; i++)
            {
                var snake_1_head_P1 = snakes[i].prevPositions.Last();
                var snake_1_head_P2 = snakes[i].prevPositions[^2];

                for (int k = 0; k < snakes[i].prevPositions.Count - 3; k++)
                {
                    var snake_2_P1 = snakes[i].prevPositions[k];
                    var snake_2_P2 = snakes[i].prevPositions[k + 1];
                    if (DoSegmentsIntersect(snake_1_head_P1, snake_1_head_P2,
                        snake_2_P1, snake_2_P2))
                    {

                        
                        snakes[i].StartAnimation("snake_energy");
                    }
                }

                for (int j = 0; j < snakes.Count; j++)
                {
                    if (i == j)
                        return;

                    for (int k = 0; k < snakes[j].prevPositions.Count-1; k++)
                    {
                        var snake_2_P1 = snakes[j].prevPositions[k];
                        var snake_2_P2 = snakes[j].prevPositions[k+1];
                        if (DoSegmentsIntersect(snake_1_head_P1, snake_1_head_P2,
                            snake_2_P1, snake_2_P2))
                        {

                            snakes[i].StartAnimation("snake_energy");
                        }
                    }

                }
            }
        }
        public static bool DoSegmentsIntersect(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
        {
            // Настраиваемая погрешность
            const float tolerance = 1e-5f;

            static float Orientation(Vector2 a, Vector2 b, Vector2 c)
                => (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);

            float o1 = Orientation(p1, p2, p3);
            float o2 = Orientation(p1, p2, p4);
            float o3 = Orientation(p3, p4, p1);
            float o4 = Orientation(p3, p4, p2);

            // Общий случай
            if (o1 * o2 < 0 && o3 * o4 < 0)
                return true;

            // Функция для проверки точки на отрезке с учетом tolerance
            bool OnSegment(Vector2 a, Vector2 b, Vector2 c)
                => c.X - Math.Max(a.X, b.X) <= tolerance
                    && Math.Min(a.X, b.X) - c.X <= tolerance
                    && c.Y - Math.Max(a.Y, b.Y) <= tolerance
                    && Math.Min(a.Y, b.Y) - c.Y <= tolerance;

            // Особые случаи (коллинеарность)
            if (Math.Abs(o1) < tolerance && OnSegment(p1, p2, p3)) return true;
            if (Math.Abs(o2) < tolerance && OnSegment(p1, p2, p4)) return true;
            if (Math.Abs(o3) < tolerance && OnSegment(p3, p4, p1)) return true;
            if (Math.Abs(o4) < tolerance && OnSegment(p3, p4, p2)) return true;

            return false;
        }

    }
}
