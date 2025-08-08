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
        public List<Vector2> deltas = new List<Vector2>() { new Vector2(0,0)};
        public Vector2 direction = new Vector2(0, 0); 
        public Snake(Vector2 position) : base(position)
        {
            
            graphicsComponent.ObjectDrawRectangle.Width = 5;
            graphicsComponent.ObjectDrawRectangle.Height= 5;
        }
        int tick = 0;
        public static float snakePixelStepSize = 4f;
        public override void Update()
        {
            tick++;
            if (tick % 50 == 0)
            {
                deltas.Add(deltas.Last() + direction);

            }
            if (tick % 2 == 0)
            {
                deltas.Add(deltas.Last() + direction);
                deltas.RemoveAt(0);
            }
            AppManager.Instance.server.AddData(new UpdateSnake() { deltas = deltas.Select(x=>x.Serialize()).ToList(), IdEntity = Id });
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in deltas)
            {
                AppManager.Instance.debugHud.Log(item.ToString());   
                graphicsComponent.ObjectDrawRectangle.X = (int)(position.X + item.X * snakePixelStepSize);
                graphicsComponent.ObjectDrawRectangle.Y = (int)(position.Y + item.Y * snakePixelStepSize);
                base.Draw(spriteBatch);
            }
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
