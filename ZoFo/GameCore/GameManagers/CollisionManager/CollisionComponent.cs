using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.GameObjects;
using ZoFo.GameCore.GameObjects.Entities.LivingEntities.Player;

namespace ZoFo.GameCore.GameManagers.CollisionManager
{
    public class CollisionComponent
    {
        //==ПОЛЯ==

        public GameObject gameObject { get; set; }
        //public Rectangle Bounds { get; set; }

        //public Rectangle Rectangle => new Rectangle();



        bool doesStop;
        public Rectangle stopRectangle;

        // triggers for rectangle
        bool isTrigger;
        public Rectangle triggerRectanglee;

        //delegate
        public delegate void EventHandler(object sender, EventArgs e);

        //public CollisionComponent(int x, int y, int width, int height)
        //{
         //   Bounds = new Rectangle(x, y, width, height);
        //}



        //events 
        public event EventHandler<CollisionComponent> OnTriggerEnter;
        public event EventHandler<CollisionComponent> OnTriggerZone;
        public event EventHandler<CollisionComponent> OnTriggerExit;

        // methods-event
        public void TriggerEnter(object component, Player player,
            EventArgs e)
        {

        }
        public void TriggerZone(object component,Player player,
             EventArgs e)
        {

        }
        public void TriggerExit(object component,Player player,
             EventArgs e)
        {

        }

        public CollisionComponent(GameObject gameObject)
        {

            this.gameObject = gameObject;
            doesStop = false;
            this.isTrigger = false;
            AppManager.Instance.server.collisionManager.Register(this);
        }
        public CollisionComponent(GameObject gameObject, bool hasCollision = false, Rectangle? collisionRectangle = null, bool isTrigger = false, Rectangle? triggerRectangle = null)
        {
            this.gameObject = gameObject;

            doesStop = hasCollision;
            this.isTrigger = isTrigger;
            if (hasCollision)
                this.stopRectangle = collisionRectangle.Value;
            if (isTrigger)
                this.triggerRectanglee = triggerRectangle.Value;

            AppManager.Instance.server.collisionManager.Register(this);
        }
    }   
}
