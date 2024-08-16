
using Microsoft.Xna.Framework;
using ZoFo.GameCore.ZoFo_graphics;

namespace ZoFo.GameCore.GameObjects;

public abstract class GameObject
{
    public Vector2 position;
    public Vector2 rotation;

    protected abstract GraphicsComponent graphicsComponent { get; }
    public void Draw() { }

}