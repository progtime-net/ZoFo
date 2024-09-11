using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GameManagers.CollisionManager;
using ZoFo.GameCore.Graphics;

namespace ZoFo.GameCore.GameObjects
{
    /// <summary>
    /// TODO: change from particle to throwable, it is not a particle anymore
    /// </summary>
    public class Granade : GameObject
    {
        public override GraphicsComponent graphicsComponent { get; } = new AnimatedGraphicsComponent(new List<string> { "explosion_1" }, "explosion_1");

        /// <summary>
        /// TODO updates with directed effect
        /// </summary>
        /// <param name="positionTo"></param>
        /// <param name="positionFrom"></param> 
        public Granade(Vector2 positionTo) : base(positionTo)
        { 
            if (AppManager.Instance.client.myPlayer != null)
                this.positionFrom = AppManager.Instance.client.myPlayer.position;
            this.positionTo = positionTo;
            graphicsComponent.ObjectDrawRectangle = new Rectangle(-30, -30, 60, 60).SetOrigin(position);
            AppManager.Instance.SoundManager.StartSound("gun-gunshot-01", Vector2.Zero, Vector2.Zero, 0.5f, (float)(Random.Shared.NextDouble() * 2 - 1));
            (graphicsComponent as AnimatedGraphicsComponent).actionOfAnimationEnd += _ =>
            {

                //Delete_OnClient(this);

            };
        }
        Vector2 positionFrom;
        Vector2 positionTo;
        float dt = 0;
        public override void UpdateLogic_OnServer()
        {

            float m = Math.Min(positionFrom.Y, positionTo.Y) - 40;
            position.X = (1 - dt) * positionFrom.X + dt * positionTo.X;
            position.Y = (2 * positionTo.Y + 2 * positionFrom.Y - 4 * m) * dt * dt +
                (4 * m - positionTo.Y - 3 * positionFrom.Y) * dt + positionFrom.Y;

            if (dt >= 0.9)
            {
                FlightEndedOnServer();
                return;
            }
            dt += 0.05f;
            base.UpdateLogic_OnServer();
        }
        private void FlightEndedOnServer()
        {

            var rect = GetDamageRectangle();

            var entities = (AppManager.Instance.server.collisionManager.GetEntities(rect).ToList());
            foreach (var item in entities)
            {
                if (item is Enemy)
                {
                    (item as Enemy).TakeDamage(1);
                }
            }
            Delete_OnServer();
            
        }
        public override void Update_OnClient()
        {
            if (dt >= 1)
            {
                //Granade Finished the flight
                Instantiate_OnClient(new Explosion(position + ExtentionClass.RandomVector()*10));
                Delete_OnClient(this);
                base.Update_OnClient();

                for (int i = 0; i < 10; i++)
                {
                    if (Random.Shared.NextDouble() < 0.1) continue;
                    float angl = i / 10f * (float)Math.PI * 2;
                    Instantiate_OnClient(new Explosion(position + 
                    new Vector2((float)Math.Cos(angl), (float)Math.Sin(angl)
                    ) * 30));

                }


                var rect = GetDamageRectangle();
                 
                return;
            }
            float m = Math.Min(positionFrom.Y, positionTo.Y)-40;
            position.X = (1 - dt) * positionFrom.X + dt * positionTo.X;
            position.Y = (2 * positionTo.Y + 2 * positionFrom.Y - 4 * m) * dt * dt +
                (4 * m - positionTo.Y - 3 * positionFrom.Y) * dt + positionFrom.Y;

            dt += 0.05f;

            //position = 
            //base.Update_OnClient();
            graphicsComponent.ObjectDrawRectangle.X = (int)position.X; //Move To place where Updates Sets your position
            graphicsComponent.ObjectDrawRectangle.Y = (int)position.Y;
            PlayAnimation_OnClient();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {



            DrawDebugRectangle(spriteBatch, GetDamageRectangle(), Color.Red);
            base.Draw(spriteBatch);
        }
        public Rectangle GetDamageRectangle()
        {

            var rect = graphicsComponent.ObjectDrawRectangle;
            rect.X = (int)position.X;
            rect.Y = (int)position.Y;
            int size = 10;
            rect.X -= size;
            rect.Y -= size;
            rect.Width += 2 * size;
            rect.Height += 2 * size;
            return rect;
        }
    }
}
