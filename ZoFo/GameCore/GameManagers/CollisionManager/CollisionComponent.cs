using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.GameObjects.Entities.LivingEntities.Player;

namespace ZoFo.GameCore.GameManagers.CollisionManager
{
    public class CollisionComponent
    {
        //поля
        public Rectangle Bounds { get; set; }

        //остановлен ли перс
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
