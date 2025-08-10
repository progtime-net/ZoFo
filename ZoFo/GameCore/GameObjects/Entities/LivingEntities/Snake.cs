using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GameManagers.CollisionManager;
using ZoFo.GameCore.GameManagers.NetworkManager.SerializableDTO;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ClientToServer;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;
using ZoFo.GameCore.Graphics;

namespace ZoFo.GameCore.GameObjects
{
    public class Snake : LivingEntity
    {
        public override GraphicsComponent graphicsComponent { get; } = new AnimatedGraphicsComponent(new List<string> { "snake_base" }, "snake_base");
        public List<Vector2> prevPositions = new List<Vector2>() { new Vector2(0, 0) };
        public Vector2 direction = new Vector2(0, 0);
        public Snake(Vector2 position) : base(position)
        {

            graphicsComponent.ObjectDrawRectangle.Width = 10;
            graphicsComponent.ObjectDrawRectangle.Height = 10;

            collisionComponent.stopRectangle = new Rectangle(0, 0, 10, 10);
            collisionComponent.triggerRectangle = new Rectangle(-5, -5, 20, 20);
        }
        int tick = 0;
        public static float snakePixelStepSize = 4f;
        public override void Update()
        {
            tick++;
            if (tick % 50 == 0)
            {
                Extend();
            }
            if (tick % 2 == 0)
            {
                prevPositions.Add(position);
                prevPositions.Add((prevPositions.Last() + position) * 0.5f);
                prevPositions.RemoveAt(0);
                prevPositions.RemoveAt(0);
            }
            //for (int i = 0; i < prevPositions.Count; i++)
            //{

            //    prevPositions[i] -= Vector2.UnitX * direction.X;
            //    prevPositions[i] -= Vector2.UnitY * direction.Y;
            //}
            velocity.X = direction.X * snakePixelStepSize;
            velocity.Y = direction.Y * snakePixelStepSize;
            AppManager.Instance.server.AddData(new UpdateSnake() { deltas = prevPositions.Select(x => x.Serialize()).ToList(), IdEntity = Id });
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in prevPositions)
            {
                AppManager.Instance.debugHud.Log(item.ToString());
                graphicsComponent.ObjectDrawRectangle.X = (int)(item.X);
                graphicsComponent.ObjectDrawRectangle.Y = (int)(item.Y);
                base.Draw(spriteBatch);
            }
            graphicsComponent.ObjectDrawRectangle.X = (int)(position.X);
            graphicsComponent.ObjectDrawRectangle.Y = (int)(position.Y);
            base.Draw(spriteBatch);
        }

        internal void SetDeltas(List<Vector2> deltas)
        {
            this.prevPositions = deltas;
        }

        internal void HandleNewInput(UpdateInput data)
        {
            if (data.InputMovementDirection.GetVector2() == Vector2.Zero)
                return;
            direction = data.InputMovementDirection.GetVector2();
            direction.Normalize();
        }

        internal void Eat(Collectable collectable)
        {
            Extend();
        }
        internal void Extend()
        {
            prevPositions.Add(prevPositions.Last());
        }
    }
}
