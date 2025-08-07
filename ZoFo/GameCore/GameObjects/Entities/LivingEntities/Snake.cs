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
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;
using ZoFo.GameCore.Graphics;

namespace ZoFo.GameCore.GameObjects
{ 
    public class Snake : LivingEntity
    {
        public override GraphicsComponent graphicsComponent { get; } = new StaticGraphicsComponent("Content/Textures/Test/Rock");
        List<Vector2> deltas = new List<Vector2>() { new Vector2(0,0)};
        Vector2 direction = new Vector2(0, 0); 
        public Snake(Vector2 position) : base(position)
        {
            
            graphicsComponent.ObjectDrawRectangle.Width = 100;
            graphicsComponent.ObjectDrawRectangle.Height= 100;
        }
        public override void Update()
        {
            if (Random.Shared.NextDouble()>0.8)
            {
                direction = (new Vector2(deltas.Last().X + Random.Shared.Next(-1, 2),
                    deltas.Last().Y + Random.Shared.Next(-1, 2)
                    ))/200;
                AppManager.Instance.server.AddData(new UpdateSnake() { deltas = deltas.Select(x=>x.Serialize()).ToList(), IdEntity = Id });
            }
            if (Random.Shared.NextDouble() > 0.8)
            {
                deltas.Add(deltas.Last() + direction);

            }
            deltas.Add(deltas.Last() + direction);
            deltas.RemoveAt(0);
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in deltas)
            {
                AppManager.Instance.debugHud.Log(item.ToString());   
                graphicsComponent.ObjectDrawRectangle.X = (int)(position.X + item.X * 10);
                graphicsComponent.ObjectDrawRectangle.Y = (int)(position.Y + item.Y * 10);
                base.Draw(spriteBatch);
            }
        }

        internal void SetDeltas(List<Vector2> deltas)
        {
            this.deltas = deltas;
        }
    }
}
