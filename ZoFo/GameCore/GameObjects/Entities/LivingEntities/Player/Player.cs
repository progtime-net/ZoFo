using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient;

namespace ZoFo.GameCore.GameObjects.Entities.LivingEntities.Player;
public class Player : LivingEntity
{
    private int health;
    Server server = new Server();

    public Player(Vector2 position) : base(position)
    {
    }

    public void Update(GameTime gameTime)
    { 
        // server.AddData();
    }
    
    public void TextureLoad(SpriteBatch spriteBatch)
    {

    }
}
