using Microsoft.Xna.Framework;
using MonogameLibrary.UI.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NativeFileDialogSharp;
using Microsoft.Xna.Framework.Graphics;

namespace AnimatorFileCreatorAdvanced.Core.GUI
{
    internal class CreatingAnimationGUI : AbstractGUI
    {


        protected override void CreateUI()
        {
            int width = AppManager.Instance.CurentScreenResolution.X;
            int height = AppManager.Instance.CurentScreenResolution.Y;


            Label FileNameLabel = new Label(Manager)
            {
                rectangle = GetRelativeRectangle_SettingSizes(0, 0.2f, 0, 0.1f),

                text = "TEXT",
                scale = 0.1f,
                fontColor = Color.White,
                mainColor = Color.Gray,
                fontName = "Fonts\\Font",
                textureName = "GUI/Button"
            };
            Label AnimationPath = new Label(Manager)
            {
                rectangle = GetRelativeRectangle_SettingSizes(0f, 0.2f, 0.1f, 0.1f),

                text = "AnimationFileFolder/Path",
                scale = 0.1f,
                fontColor = Color.White,
                mainColor = Color.Gray,
                fontName = "Fonts\\Font",
                textureName = "GUI/Button"
            };
            Label AnimationTexturePath = new Label(Manager)
            {
                rectangle = GetRelativeRectangle_SettingSizes(0.2f, 0.2f, 0.1f, 0.1f),

                text = "AnimationTextureFolder/Path",
                scale = 0.1f,
                fontColor = Color.White,
                mainColor = Color.Gray,
                fontName = "Fonts\\Font",
                textureName = "GUI/Button"
            };


            Button openFileButton = new Button(Manager)
            {
                rectangle = GetRelativeRectangle_SettingSizes(0.4f, 0.2f, 0, 0.1f),
                text = "Open File",
                scale = 0.1f,
                fontColor = Color.White,
                mainColor = Color.Gray,
                fontName = "Fonts\\Font",
                textureName = "GUI/Button"
            };
            openFileButton.LeftButtonPressed += () =>
            {
                DialogResult result = Dialog.FileOpen();
                var temp = result.Path.Split('\\');
                string textureName = temp[temp.Length - 2] + "/" + temp[temp.Length - 1];
                textureName = textureName.Split('.')[0];


                FileNameLabel.text = textureName;
                AppLogic.LoadFile(result.Path);
                SetAnimationSample();
            };
            Elements.Add(openFileButton);


            Button AutoBuildButton = new Button(Manager)
            {
                rectangle = GetRelativeRectangle_SettingSizes(0.6f, 0.1f, 0, 0.1f),
                text = "AutoBuild",
                scale = 0.1f,
                fontColor = Color.White,
                mainColor = Color.Gray,
                fontName = "Fonts\\Font",
                textureName = "GUI/Button"
            };
            Button RunButton = new Button(Manager)
            {
                rectangle = GetRelativeRectangle_SettingSizes(0.7f, 0.1f, 0, 0.1f),
                text = "Run",
                scale = 0.1f,
                fontColor = Color.White,
                mainColor = Color.Gray,
                fontName = "Fonts\\Font",
                textureName = "GUI/Button"
            };
            Button Save = new Button(Manager)
            {
                rectangle = GetRelativeRectangle_SettingSizes(0.8f, 0.1f, 0, 0.1f),
                text = "Save",
                scale = 0.1f,
                fontColor = Color.White,
                mainColor = Color.Gray,
                fontName = "Fonts\\Font",
                textureName = "GUI/Button"
            };
            Button AddToMGCB = new Button(Manager)
            {
                rectangle = GetRelativeRectangle_SettingSizes(0.9f, 0.1f, 0, 0.1f),
                text = "Add To MGCB",
                scale = 0.1f,
                fontColor = Color.White,
                mainColor = Color.Gray,
                fontName = "Fonts\\Font",
                textureName = "GUI/Button"
            };

            AddToMGCB.LeftButtonPressed += () =>
            {
                /*
                add this to mgcb

                
#begin Textures/Animations/explosion_1.animation
/copy:Textures/Animations/explosion_1.animation
                
#begin GUI/checkboxs_off.png
/importer:TextureImporter
/processor:TextureProcessor
/processorParam:ColorKeyColor=255,0,255,255
/processorParam:ColorKeyEnabled=True
/processorParam:GenerateMipmaps=False
/processorParam:PremultiplyAlpha=True
/processorParam:ResizeToPowerOfTwo=False
/processorParam:MakeSquare=False
/processorParam:TextureFormat=Color
/build:GUI/checkboxs_off.png

                 */
            };
        }

        Texture2D BlackTexture;
        Texture2D LoadedSample;
        Rectangle SampleRectangle;
        Rectangle ExampleAnimation;

        Rectangle AnimationSampleRectangle;
        public override void LoadContent()
        {
            BlackTexture = new Texture2D(AppManager.Instance.GraphicsDevice, 1, 1);
            BlackTexture.SetData(new Color[] { Color.Black });
            SampleRectangle = GetRelativeRectangle(0.01f, 0.31f, 0.1f, 0.01f);
            ExampleAnimation = GetRelativeRectangle(0.7f, 0.01f, 0.1f, 0.01f);

            base.LoadContent();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(BlackTexture, SampleRectangle, Color.White);
            spriteBatch.Draw(BlackTexture, ExampleAnimation, Color.White);

            if (LoadedSample != null)
            {
                spriteBatch.Draw(LoadedSample, AnimationSampleRectangle, Color.White);

            }
            spriteBatch.End();
            base.Draw(spriteBatch);
        }
        float margin_left = 0.01f;
        float margin_right = 0.01f;
        float margin_top = 0.01f;
        float margin_bottom = 0.01f;
        public Rectangle GetRelativeRectangle_SettingSizes(float marginPercentFromLeft, float relativeXSize, float marginPercentFromTop, float relativeYSize, Rectangle? area = null)
            => GetRelativeRectangle(marginPercentFromLeft, 1 - marginPercentFromLeft - relativeXSize, marginPercentFromTop, 1 - marginPercentFromTop- relativeYSize, area);

        public Rectangle GetRelativeRectangle(float marginPercentFromLeft, float marginPercentFromRight, float marginPercentFromTop, float marginPercentFromBottom, Rectangle? area = null)
        {
            int width;
            int height;
            if (area is null)
            {
                width = AppManager.Instance.CurentScreenResolution.X;
                height = AppManager.Instance.CurentScreenResolution.Y;
            }
            else
            {
                width = area.Value.Width;
                height = area.Value.Height;
            }

            float left = width * (marginPercentFromLeft + margin_left);
            float right = width * (1 - marginPercentFromRight - margin_right);
            float top = height * (marginPercentFromTop + margin_top);
            float bottom = height * (1 - marginPercentFromBottom - margin_bottom);

            return new Rectangle((int)left, (int)top, (int)(right - left), (int)(bottom - top));

        }

        public void SetAnimationSample()
        {
            Texture2D texture = AppLogic.fileTexture;
            if (texture is null)
            {
                return;
            }
            LoadedSample = texture;
            if (texture.Width / (float)texture.Height > SampleRectangle.Width / (float)SampleRectangle.Height)
            {
                //not  full height
                AnimationSampleRectangle = new Rectangle(SampleRectangle.X,
                    SampleRectangle.Y + (SampleRectangle.Height -
                    (int)(SampleRectangle.Width * (texture.Height / (float)texture.Width))
                    ) / 2,
                    SampleRectangle.Width,
                    (int)(SampleRectangle.Width * (texture.Height / (float)texture.Width))
                    );
            }
            else
            {

                //TODO
                AnimationSampleRectangle = new Rectangle(SampleRectangle.X,
                    SampleRectangle.Y + (SampleRectangle.Height -
                    (int)(SampleRectangle.Width * (texture.Height / (float)texture.Width))
                    ) / 2,
                    SampleRectangle.Width,
                    (int)(SampleRectangle.Width * (texture.Height / (float)texture.Width))
                    );
            }
        }
    }
    static class AppLogic
    {
        public static Texture2D fileTexture;
        public static void LoadFile(string filePath)
        {
            fileTexture = Texture2D.FromFile(AppManager.Instance.GraphicsDevice, filePath);
        }
    }
}
