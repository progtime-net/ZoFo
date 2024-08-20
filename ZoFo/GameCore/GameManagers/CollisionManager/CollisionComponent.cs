using Microsoft.Win32;
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
        //==КОНСТРУКТОР== 
        public CollisionComponent(GameObject gameObject)
        {

            this.gameObject = gameObject;
            hasCollision = false;
            this.isTrigger = false;
        } 

        public CollisionComponent(GameObject gameObject, bool hasCollision = false, Rectangle? collisionRectangle = null, bool isTrigger = false, Rectangle? triggerRectangle = null)
        {
            this.gameObject = gameObject;

            this.hasCollision = hasCollision;
            this.isTrigger = isTrigger;
            if (hasCollision)
                this.stopRectangle = collisionRectangle.Value;
            if (isTrigger)
                this.triggerRectangle = triggerRectangle.Value;
            
            

            
        }

        //==ПОЛЯ==

        public GameObject gameObject { get; set; }


        public bool hasCollision;
        public Rectangle stopRectangle;

        // triggers for rectangle
        public bool isTrigger;
        public Rectangle triggerRectangle;

        //delegate
        public delegate void EventHandler(object sender, EventArgs e);




        //events DoorInteraction
        public delegate void CollisionAction(Player player);
        public event CollisionAction? OnTriggerEnter;
        public event CollisionAction? OnTriggerZone;
        public event CollisionAction? OnTriggerExit;

        public delegate void CoollisionEvent(GameObject gameObject);
        public event CoollisionEvent? OnCollision;



        public void PlayerInZone(Player player) => OnTriggerZone?.Invoke(player);
        public void PlayerEnter(Player player) => OnTriggerEnter?.Invoke(player);
        public void PlayerExit(Player player) => OnTriggerExit?.Invoke(player);
        public void OnCollisionWithObject(GameObject gameObject) => OnCollision?.Invoke(gameObject);


    }   
}
