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
        public override GraphicsComponent graphicsComponent { get; } = new StaticGraphicsComponent("Content/Textures/Test/Rock");
        public List<Vector2> deltas = new List<Vector2>() { new Vector2(0, 0) };
        public Vector2 direction = new Vector2(0, 0);
        public Vector2 pointZero = new Vector2(0, 0);
        public Snake(Vector2 position) : base(position)
        {

            graphicsComponent.ObjectDrawRectangle.Width = 7;
            graphicsComponent.ObjectDrawRectangle.Height = 7;
            pointZero = position;
        }
        int tick = 0;
        public static float snakePixelStepSize = 6f;
        public override void Update()
        {
            tick++;
            if (tick % 20 == 0)
            {
                deltas.Add(deltas.Last() + direction);

            }
            if (tick % 2 == 0)
            {
                deltas.Add(deltas.Last() + direction);
                deltas.RemoveAt(0);
            }
            velocity += direction * snakePixelStepSize;
            for (int i = 0; i < deltas.Count; i++)
            {
                    deltas[i] -= deltas.Last();
            }
            AppManager.Instance.server.AddData(new UpdateSnake() { deltas = deltas.Select(x => x.Serialize()).ToList(), IdEntity = Id });
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in deltas)
            {
                AppManager.Instance.debugHud.Log(item.ToString());
                var additionalShift = -0*(position - pointZero)/ snakePixelStepSize;
                graphicsComponent.ObjectDrawRectangle.X = (int)(position.X + item.X * snakePixelStepSize - additionalShift.X);
                graphicsComponent.ObjectDrawRectangle.Y = (int)(position.Y + item.Y * snakePixelStepSize - additionalShift.Y);
                base.Draw(spriteBatch);
            }

            graphicsComponent.ObjectDrawRectangle.X = (int)(position.X  );
            graphicsComponent.ObjectDrawRectangle.Y = (int)(position.Y  );
            base.Draw(spriteBatch);
        }

        internal void SetDeltas(List<Vector2> deltas)
        {
            this.deltas = deltas;
        }

        internal void HandleNewInput(UpdateInput data)
        {
            if (data.InputMovementDirection.GetVector2() == Vector2.Zero)
                return;
            direction = data.InputMovementDirection.GetVector2();
            direction.Normalize();
        }
    }
}
