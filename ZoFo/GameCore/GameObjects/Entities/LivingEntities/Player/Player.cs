using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;

namespace ZoFo.GameCore.GameObjects.Entities.LivingEntities.Player;

public class Player : LivingEntity
{
    public Vector2 InputWeaponRotation{ get; set; }
    public Vector2 InputPlayerRotation{ get; set;}
    public bool IsTryingToShoot{get;set;}
    Texture2D texture;
    private float speed;
    private int health;
    public Player(Vector2 position) : base(position)
    {
        //InputWeaponRotation = new Vector2(0, 0);
        //InputPlayerRotation = new Vector2(0, 0);
    }

    public void Update(GameTime gameTime)
    { 
        
    }
}
