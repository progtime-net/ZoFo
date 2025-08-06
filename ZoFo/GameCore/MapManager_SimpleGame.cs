using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using ZoFo.GameCore.GameManagers.CollisionManager;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GameManagers.MapManager;
using ZoFo.GameCore.GameObjects;
using ZoFo.GameCore.Graphics;
using ZoFo.GameCore.GUI;
using Microsoft.Xna.Framework;

namespace ZoFo.GameCore
{
    internal class MapManager_SimpleGame : MapManagerProto
    {
        public override void LoadMap(string mapName = "main")
        {
            RegisterGameObject(new Tank(new Vector2(0, 0)));
        }
    }
    public class Tank : Entity
    {

        public override GraphicsComponent graphicsComponent { get; } = new StaticGraphicsComponent("Content\\Textures\\NonAnimation\\Hull_01");
        public Tank(Vector2 position) : base(position)
        { 
            graphicsComponent.ObjectDrawRectangle = new Rectangle(0, 0, 30, 30);
            collisionComponent.stopRectangle = new Rectangle(10, 20, 10, 10);  
            collisionComponent.isTrigger = true;
            collisionComponent.hasCollision = true; 
            collisionComponent.triggerRectangle = new Rectangle(-5, -5, 40, 40); 
        }
    }
     
}