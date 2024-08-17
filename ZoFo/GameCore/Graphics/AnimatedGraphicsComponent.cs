using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ZoFo.GameCore.GameManagers;

namespace ZoFo.GameCore.Graphics
{

    public class AnimatedGraphicsComponent : GraphicsComponent
    {
        public Rectangle ObjectDrawRectangle;



        public event Action<string> OnAnimationEnd;
        private List<AnimationContainer> animations;
        private List<Texture2D> textures;
        public List<string> texturesNames; //rethink public and following that errors
        private AnimationContainer currentAnimation;

        public bool animating = true;
        private int step = 1;
        
        public AnimationContainer CurrentAnimation
        {
            get
            {
                return currentAnimation;
            }
        }
        public string LastAnimation { get; set; }
        public string GetCurrentAnimation
        {
            get { return currentAnimation.Id; }
        }

        private AnimationContainer idleAnimation;
        //private SpriteBatch _spriteBatch;

        private int currentFrame;
        public int CurrentFrame
        {
            get
            {
                return currentFrame;
            }
        }
        private int interval;
        private int lastInterval;
        private Rectangle sourceRectangle;
        public AnimatedGraphicsComponent(List<string> animationsId, string neitralAnimationId)
        {
            //this._spriteBatch = _spriteBatch;
            currentFrame = 0;
            lastInterval = 1;
            LoadAnimations(animationsId, neitralAnimationId);
            currentAnimation = idleAnimation;
            SetInterval();
            buildSourceRectangle();
        }

        
        public AnimatedGraphicsComponent(string textureName)
        {
            BuildComponent(textureName);
        }
        public AnimatedGraphicsComponent()
        {
        }
        public void BuildComponent(string textureName)
        {
            mainTextureName = textureName;
            //texturesNames.Add(textureName);//Added by SD
            animations = new List<AnimationContainer>();
            textures = new List<Texture2D>();
            var texture = AppManager.Instance.Content.Load<Texture2D>(textureName);
            textures.Add(texture);
            AnimationContainer animationContainer = new AnimationContainer();
            animationContainer.StartSpriteRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            animationContainer.TextureFrameInterval = 0;
            animationContainer.TextureName = texture.Name;
            animationContainer.IsCycle = true;
            animationContainer.FramesCount = 1;
            animationContainer.FrameTime = new List<Tuple<int, int>>() { new Tuple<int, int>(0, 10) };
            animationContainer.Id = texture.Name;
            currentAnimation = animationContainer;
            idleAnimation = animationContainer;
            animations.Add(animationContainer);
        }

        private void LoadAnimations(List<string> animationsId, string neitralAnimationId)
        {
            animations = new List<AnimationContainer>();
            foreach (var id in animationsId)
            {
                animations.Add(AppManager.Instance.animationBuilder.Animations.Find(x => x.Id == id));
                if (id == neitralAnimationId)
                {
                    idleAnimation = animations.Last();
                }
            }
        }

        public override void LoadContent()
        {
            textures = new List<Texture2D>();
            texturesNames = new List<string>();

            if (animations is null)
            {
                return;
            }

            foreach (var animation in animations)
            {
                if (!texturesNames.Contains(animation.TextureName))
                {
                    texturesNames.Add(animation.TextureName);
                    textures.Add(AppManager.Instance.Content.Load<Texture2D>(animation.TextureName));
                }
            }
        }

        public void AnimationSelect(string animationId, bool reverse = false)
        {
            currentAnimation = animations.Find(x => x.Id == animationId);
            if (reverse)
            {
                currentFrame = currentAnimation.FramesCount;
                step = -1;
            }
            else
            {
                step = 1;
                currentFrame = 1;
            }
            buildSourceRectangle();
            SetInterval();
        }

        public void StartAnimation()
        {
            animating = true;
        }
        
        public void AnimationStep()
        {
            currentFrame += step;
        }

        public void SetFrame(int frame)
        {
            currentFrame = frame;
        }

        public void StopAnimation()
        {
            currentFrame = 0;
            interval = 0;
            currentAnimation = idleAnimation;
            buildSourceRectangle();
            SetInterval();
        }

        private void AnimationEnd()
        {
            if (!currentAnimation.IsCycle)
            {
                if (OnAnimationEnd != null)
                {
                    OnAnimationEnd(currentAnimation.Id);
                }
                currentAnimation = idleAnimation;
                animating = false;
            }
            currentFrame = 0;
        }

        public override void Update()
        {
            if (currentAnimation.FramesCount <= currentFrame || currentFrame < 0)
            {
                AnimationEnd();
            }
            
            if (!animating)
                return;

            if (interval == 0)
            {
                currentFrame += step;
                buildSourceRectangle();
                SetInterval();
            }
            
            interval--;
        }
        

        public override void Draw(Rectangle destinationRectangle, SpriteBatch _spriteBatch)
        {
            Texture2D texture = textures[texturesNames.FindIndex(x => x == currentAnimation.TextureName)];
            float scale;
            if (currentAnimation.Offset.X != 0)
            {
                destinationRectangle.X -= (int)currentAnimation.Offset.X;
                scale = destinationRectangle.Height / sourceRectangle.Height;
                destinationRectangle.Width = (int)(sourceRectangle.Width * scale);

            }
            else if (currentAnimation.Offset.Y != 0)
            {
                destinationRectangle.Y -= (int)currentAnimation.Offset.Y;
                scale = destinationRectangle.Width / sourceRectangle.Width;
                destinationRectangle.Height = (int)(sourceRectangle.Height * scale);
            }

            destinationRectangle.X -= CameraPosition.X;
            destinationRectangle.Y -= CameraPosition.Y;

            destinationRectangle = Scaling(destinationRectangle);
            _spriteBatch.Draw(texture,
                destinationRectangle, sourceRectangle, Color.White);
        }
        public override void Draw(Rectangle destinationRectangle, SpriteBatch _spriteBatch, Rectangle sourceRectangle)
        {
            Texture2D texture = textures[texturesNames.FindIndex(x => x == currentAnimation.TextureName)];
            float scale;
            if (currentAnimation.Offset.X != 0)
            {
                destinationRectangle.X -= (int)currentAnimation.Offset.X;
                scale = destinationRectangle.Height / sourceRectangle.Height;
                destinationRectangle.Width = (int)(sourceRectangle.Width * scale);

            }
            else if (currentAnimation.Offset.Y != 0)
            {
                destinationRectangle.Y -= (int)currentAnimation.Offset.Y;
                scale = destinationRectangle.Width / sourceRectangle.Width;
                destinationRectangle.Height = (int)(sourceRectangle.Height * scale);
            }

            destinationRectangle.X -= CameraPosition.X;
            destinationRectangle.Y -= CameraPosition.Y;

            destinationRectangle = Scaling(destinationRectangle);
            _spriteBatch.Draw(texture,
                destinationRectangle, sourceRectangle, Color.White);
        }
        private void buildSourceRectangle()
        {
            sourceRectangle = new Rectangle();
            if (currentAnimation == null)
            {
                currentAnimation = idleAnimation;
            }
            sourceRectangle.X = currentAnimation.StartSpriteRectangle.X + currentFrame *
                (currentAnimation.StartSpriteRectangle.Width + currentAnimation.TextureFrameInterval);
            sourceRectangle.Y = currentAnimation.StartSpriteRectangle.Y;
            sourceRectangle.Height = currentAnimation.StartSpriteRectangle.Height;
            sourceRectangle.Width = currentAnimation.StartSpriteRectangle.Width;
        }

        private void SetInterval()
        {
            Tuple<int, int> i = currentAnimation.FrameTime.Find(x => x.Item1 == currentFrame);
            if (i != null)
            {
                interval = i.Item2;
                lastInterval = interval;
            }
            else
            {
                interval = lastInterval;
            }
        }
        public static void SetCameraPosition(Vector2 playerPosition)
        {
            CameraPosition = (playerPosition).ToPoint();
            CameraPosition.X -= 200;
            CameraPosition.Y -= 120;
            
            // TODO
            /*
            if (CameraPosition.X > AppManager.Instance.GameManager.CameraBorder.Y - 460)
            {
                CameraPosition.X = (int)AppManager.Instance.GameManager.CameraBorder.Y - 460;
            }
            
            if (CameraPosition.Y < AppManager.Instance.GameManager.CameraBorder.Z)
            {
                CameraPosition.Y = (int)AppManager.Instance.GameManager.CameraBorder.Z;
            }
            if (CameraPosition.X < AppManager.Instance.GameManager.CameraBorder.X)
            {
                CameraPosition.X = (int)AppManager.Instance.GameManager.CameraBorder.X;
            }
            if (CameraPosition.Y > AppManager.Instance.GameManager.CameraBorder.W - 240)
            {
                CameraPosition.Y = (int)AppManager.Instance.GameManager.CameraBorder.W - 240;
            }
            
            AppManager.Instance.DebugHUD.Set("CameraPosition", $"{CameraPosition.X}, {CameraPosition.Y}");
        */
        }
        public static Point CameraPosition = new Point(0, 0);
    }
}
