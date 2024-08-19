using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZoFo.GameCore.GameManagers;

namespace ZoFo.GameCore.Graphics;

public class ManualGraphicsComponent : GraphicsComponent
{
    private Texture2D texture;
    private string textureName;
    private List<FrameContainer> frames;
    private int currentFrame;
    private int interval;
    public bool IsCycle;
    private bool animating;
    
    public ManualGraphicsComponent(string textureName, List<FrameContainer> frames)
    {
        this.textureName = textureName;
        this.frames = frames;
        currentFrame = 0;
        interval = frames[currentFrame].FrameTime;
    }

    public void StartAnimation()
    {
        animating = true;
    }

    public void EndAnimation()
    {
        currentFrame = 0;
        interval = frames[currentFrame].FrameTime;
        if (!IsCycle)
        {
            animating = false;
        } 
    }

    public override void LoadContent()
    {
        texture = AppManager.Instance.Content.Load<Texture2D>(textureName);
    }

    public override void Update()
    {
        if (!animating)
            return;
        
        if (interval == 0)
        {
            currentFrame++;
            if (currentFrame >= frames.Count)
            {
                EndAnimation();
            }

            interval = frames[currentFrame].FrameTime;
        }
        else
        {
            interval--;
        }
    }

    public override void Draw(Rectangle destinationRectangle, SpriteBatch _spriteBatch)
    {
        
        _spriteBatch.Draw(texture, destinationRectangle, frames[currentFrame].SourceRectangle, Color.White, Rotation,
            Vector2.Zero, Flip, 0);
    }

    public override void Draw(Rectangle destinationRectangle, SpriteBatch _spriteBatch, Rectangle sourceRectangle)
    {
        throw new System.NotImplementedException();
    }
}
