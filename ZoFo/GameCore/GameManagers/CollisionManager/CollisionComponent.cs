﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.CollisionManager
{
    public class CollisionComponent
    { 
        //остановлен ли перс
        bool doesStop;
        Rectangle stopRectangle;

        // --
        bool isTrigger;
        Rectangle triggerRectangle;

        //events 
        public delegate void EventHandler(object sender, EventArgs e);  

        public event EventHandler<CollisionComponent> OnTriggerEnter;
        public event EventHandler<CollisionComponent> OnTriggerZone;
        public event EventHandler<CollisionComponent> OnTriggerExit;

        // methods-event

        public void TriggerEnter(object component, EventArgs e)
        {

        }

    }
}
