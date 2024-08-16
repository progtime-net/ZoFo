using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.GameObjects;

namespace ZoFo.GameCore.GameManagers.CollisionManager
{
    public class CollisionComponent
    {
        //==ПОЛЯ==

        public GameObject gameObject { get; set; }
        public Rectangle Bounds { get; set; }
        
        bool doesStop;
        Rectangle stopRectangle;

        // triggers for rectangle
        bool isTrigger;
        Rectangle triggerRectangle;

        //delegate
        public delegate void EventHandler(object sender, EventArgs e);

        public CollisionComponent(int x, int y, int width, int height)
        {
            Bounds = new Rectangle(x, y, width, height);
        }

        public CollisionComponent(Rectangle bounds)
        {
            Bounds = bounds;
        }


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


    }   
}
