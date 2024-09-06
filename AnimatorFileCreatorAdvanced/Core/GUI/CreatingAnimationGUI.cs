using Microsoft.Xna.Framework;
using MonogameLibrary.UI.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NativeFileDialogSharp;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.IO;
using ZoFo.GameCore.Graphics;
using Newtonsoft.Json;

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
                if (result.Path is null) return;

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
            RunButton.LeftButtonPressed += () =>
            {
                BuildAnimation();
            };
            Button SaveBtn = new Button(Manager)
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

            SaveBtn.LeftButtonPressed += () =>
            {
                Save();
            };



            #region LowerPanel


            InputIsCycle = CreateInputAndCheckBoxPair(GetRelativeRectangle_SettingSizes(0.0f, 0.3f, 0.3f, 0.1f), 0.2f, "Зациклено?:", "");
            TicksPerFrame = CreateInputAndTextPair(GetRelativeRectangle_SettingSizes(0.0f, 0.3f, 0.4f, 0.1f), 0.2f, "Тиков на кадр:", "5");
            InputIdName = CreateInputAndTextPair(GetRelativeRectangle_SettingSizes(0.0f, 0.3f, 0.5f, 0.1f), 0.8f, "ID:", "run");
            InputWidth = CreateInputAndTextPair(GetRelativeRectangle_SettingSizes(0.0f, 0.3f, 0.6f, 0.1f), 0.2f, "Колонн:", "1");
            InputHeight = CreateInputAndTextPair(GetRelativeRectangle_SettingSizes(0.0f, 0.3f, 0.7f, 0.1f), 0.2f, "Рядов:", "1");
            InputFramesCount = CreateInputAndTextPair(GetRelativeRectangle_SettingSizes(0.0f, 0.3f, 0.8f, 0.1f), 0.2f, "Кадров:", "1");
            animationRow = CreateInputAndTextPair(GetRelativeRectangle_SettingSizes(0.0f, 0.3f, 0.9f, 0.1f), 0.2f, "Ряд анимации:", "1");

            #endregion
        }
        TextBox InputWidth;
        TextBox InputHeight;
        TextBox InputFramesCount;
        TextBox animationRow;
        TextBox InputIdName;
        TextBox TicksPerFrame;
        CheckBox InputIsCycle;

        /// <summary>
        /// ratio = inputWidth/totalWidth
        /// </summary>
        /// <param name="area"></param>
        /// <param name="ratio"></param>
        /// <param name="textString"></param>
        /// <param name="textBoxString"></param>
        /// <returns></returns>
        public TextBox CreateInputAndTextPair(Rectangle area, float ratio, string textString, string textBoxString)
        {
            int arWidth = area.Width;
            area.Width = arWidth - (int)(arWidth * ratio) + (int)(arWidth * (ratio / 2));
            Label lb = new Label(Manager)
            {
                rectangle = area,
                text = textString,
                scale = 0.2f,
                fontColor = Color.Black,
                mainColor = Color.Black,
                fontName = "Fonts\\Font4",
                textureName = "GUI/Button",
                textAligment = MonogameLibrary.UI.Enums.TextAligment.Left
            };
            area.X += (int)(arWidth * (1 - ratio));
            area.Width = arWidth - (int)(arWidth * (1 - ratio));
            TextBox tb = new TextBox(Manager)
            {
                rectangle = area,
                text = textBoxString,
                scale = 0.4f,
                fontColor = Color.Black,
                mainColor = Color.Gray,
                fontName = "Fonts\\Font4",
                textureName = "GUI/Button",
                textAligment = MonogameLibrary.UI.Enums.TextAligment.Center
            };
            return tb;
        }

        public CheckBox CreateInputAndCheckBoxPair(Rectangle area, float ratio, string textString, string textBoxString)
        {
            int arWidth = area.Width;
            area.Width = arWidth - (int)(arWidth * ratio) + (int)(arWidth * (ratio / 2));
            Label lb = new Label(Manager)
            {
                rectangle = area,
                text = textString,
                scale = 0.2f,
                fontColor = Color.Black,
                mainColor = Color.Black,
                fontName = "Fonts\\Font4",
                textureName = "GUI/Button",
                textAligment = MonogameLibrary.UI.Enums.TextAligment.Left
            };
            area.X += (int)(arWidth * (1 - ratio));
            area.Width = arWidth - (int)(arWidth * (1 - ratio));
            CheckBox tb = new CheckBox(Manager)
            {
                rectangle = area,
                text = textBoxString,
                scale = 0.4f,
                fontColor = Color.Black,
                mainColor = Color.Gray,
                fontName = "Fonts\\Font4",
                textureName = "GUI/Button",
                textAligment = MonogameLibrary.UI.Enums.TextAligment.Center
            };
            return tb;
        }



        Texture2D BlackTexture;
        Texture2D LoadedSample;
        Rectangle SampleRectangle;
        Rectangle ExampleAnimation;

        Rectangle AnimationSampleRectangle;
        Rectangle AnimationExampleSampleRectangle;
        public override void LoadContent()
        {
            BlackTexture = new Texture2D(AppManager.Instance.GraphicsDevice, 1, 1);
            BlackTexture.SetData(new Color[] { Color.Black });
            SampleRectangle = GetRelativeRectangle_SettingSizes(0.3f, 0.4f, 0.1f, 0.9f);
            ExampleAnimation = GetRelativeRectangle_SettingSizes(0.7f, 0.3f, 0.1f, 0.9f);

            base.LoadContent();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(BlackTexture, SampleRectangle, Color.White);
            spriteBatch.Draw(BlackTexture, ExampleAnimation, Color.White);

            if (LoadedSample != null)
            {
                spriteBatch.Draw(LoadedSample, AnimationSampleRectangle, Color.White);
                spriteBatch.Draw(LoadedSample, AnimationExampleSampleRectangle, AppLogic.animationRectangle, Color.White);

            }
            spriteBatch.End();
            base.Draw(spriteBatch);
        }
        float margin_left = 0.01f;
        float margin_right = 0.01f;
        float margin_top = 0.01f;
        float margin_bottom = 0.01f;
        public Rectangle GetRelativeRectangle_SettingSizes(float marginPercentFromLeft, float relativeXSize, float marginPercentFromTop, float relativeYSize, Rectangle? area = null)
            => GetRelativeRectangle(marginPercentFromLeft, 1 - marginPercentFromLeft - relativeXSize, marginPercentFromTop, 1 - marginPercentFromTop - relativeYSize, area);

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
                AnimationSampleRectangle = new Rectangle(SampleRectangle.X
                    + (SampleRectangle.Width -
                    (int)(SampleRectangle.Height * (texture.Width / (float)texture.Height))
                    ) / 2
                    ,
                    SampleRectangle.Y,
                    (int)(SampleRectangle.Height * (texture.Width / (float)texture.Height)),
                    SampleRectangle.Height
                    );
            }
        }
        public void BuildAnimation()
        {

            Point point = new Point(13, 1);
            if (!int.TryParse(InputWidth.text, out point.X))
                point.X = 1;
            if (point.X < 1)
                point.X = 1;
            if (!int.TryParse(InputHeight.text, out point.Y))
                point.Y = 1;
            if (point.Y < 1)
                point.Y = 1;

            AppLogic.BuildBaseRectangle(point.X, point.Y);

            AppLogic.SetRow(int.Parse(animationRow.text));
            AppLogic.SetFrmesCount(int.Parse(InputFramesCount.text));
            AppLogic.SetAnimationId(InputIdName.text);
            AppLogic.SetIsCycle(InputIsCycle.GetChecked);
            AppLogic.SetTicksPerAllFrames(int.Parse(TicksPerFrame.text));

            if (AppLogic.animationRectangle.Width / (float)AppLogic.animationRectangle.Height > ExampleAnimation.Width / (float)ExampleAnimation.Height)
            {
                //not  full height
                AnimationExampleSampleRectangle = new Rectangle(ExampleAnimation.X,
                    ExampleAnimation.Y + (ExampleAnimation.Height -
                    (int)(ExampleAnimation.Width * (AppLogic.animationRectangle.Height / (float)AppLogic.animationRectangle.Width))
                    ) / 2,
                    ExampleAnimation.Width,
                    (int)(ExampleAnimation.Width * (AppLogic.animationRectangle.Height / (float)AppLogic.animationRectangle.Width))
                    );
            }
            else
            {

                //TODO
                AnimationExampleSampleRectangle = new Rectangle(ExampleAnimation.X
                    + (ExampleAnimation.Width -
                    (int)(ExampleAnimation.Height * (AppLogic.animationRectangle.Width / (float)AppLogic.animationRectangle.Height))
                    ) / 2
                    ,
                    ExampleAnimation.Y,
                    (int)(ExampleAnimation.Height * (AppLogic.animationRectangle.Width / (float)AppLogic.animationRectangle.Height)),
                    ExampleAnimation.Height
                    );
            }
        }

        public override void Update(GameTime gameTime)
        {
            AppLogic.Update();
            base.Update(gameTime);
        }

        public void Save()
        {
            AppLogic.SaveCurrentAnimation("");
        }

    }
    static class AppLogic
    {
        public static Texture2D fileTexture;
        public static Rectangle textureTotalRectangle;
        public static Rectangle animationRectangle;
        public static Point animjationStep;
        public static int rows;
        public static int columns;
        public static bool buildDone = false;
        public static string textureFilePath;
        public static string textureEndName;
        public static void LoadFile(string filePath)
        {
            fileTexture = Texture2D.FromFile(AppManager.Instance.GraphicsDevice, filePath);
            textureTotalRectangle = new Rectangle(0, 0, fileTexture.Width, fileTexture.Height);
            buildDone = false;
            textureFilePath = filePath;


            var temp = filePath.Split('\\');
            textureEndName = /*temp[temp.Length - 2] + "/" +*/ temp[temp.Length - 1];
            textureEndName = textureEndName.Split('.')[0];
        }
        public static void BuildBaseRectangle(int _columns, int _rows)
        {
            rows = _rows;
            columns = _columns;
            animationRectangle = new Rectangle(0, 0, textureTotalRectangle.Width / columns, textureTotalRectangle.Height / rows);
            animjationStep = new Point(animationRectangle.Width, 0);

            buildDone = true;
        }
        /// <summary>
        /// rows from 0
        /// </summary>
        /// <param name="row"></param>
        public static void SetRow(int row)
        {
            animationRectangle.Y = animationRectangle.Height * row;

        }

        public static int ticksPassed = 0;
        public static void Update()
        {
            if (!buildDone) return;
            ticksPassed++;
            if (ticksPassed > 3)
            {
                ticksPassed = 0;
                SetNextFrame();
            }
        }
        static int curframe = 0;
        static int curtick = 0;
        private static void SetNextFrame()
        {
            curtick++;
            if (curtick < frameTimes.First().Item2) return;
            curtick = 0;

            curframe++;
            if (curframe >= frameCount)
            {
                curframe = 0;
            }
            //animationRectangle.Y += animjationStep.Y;
            //if (animationRectangle.Right > textureTotalRectangle.Width) // ended row
            //{
            //    animationRectangle.X = 0;
            //}
            //animationRectangle.Y = curframe * animjationStep.Y;
            animationRectangle.X = curframe * animjationStep.X;

        }

        public static void SetFrmesCount(int _frameCount) => frameCount = _frameCount;
        public static int frameCount;

        public static void SetIsCycle(bool _isCycle) => isCycle = _isCycle;
        public static bool isCycle;

        public static void SetAnimationId(string _id) => id = _id;
        public static string id;

        public static List<Tuple<int, int>> frameTimes = new List<Tuple<int, int>>() { new Tuple<int, int>(0, 5) };


        public static void SetTicksPerAllFrames(int ticks) => frameTimes = new List<Tuple<int, int>>() { new Tuple<int, int>(0, ticks) };

        public static void SaveCurrentAnimation(string saveString)
        {
            DialogResult result = Dialog.FolderPicker();
            if (result.Path is null)
            {
                return;
            }
            var temp = result.Path.Split("Animations")[1].Remove(0, 1);
            //string textureName = temp[temp.Length - 2] + "/" + temp[temp.Length - 1];
            //textureName = textureName.Split('.')[0];
            //choose save folder (it  will save for further animations)

            id = id.ToLower();
            AnimationContainer container = new AnimationContainer();

            if (!Directory.Exists("../../../../ZoFo/Content/Textures/AnimationTextures/" + temp))
                Directory.CreateDirectory("../../../../ZoFo/Content/Textures/AnimationTextures/" + temp);
            if (!File.Exists("../../../../ZoFo/Content/Textures/AnimationTextures/" + temp + "/" + textureEndName + ".png"))
                File.Copy(textureFilePath, "../../../../ZoFo/Content/Textures/AnimationTextures/" + temp + "/" + textureEndName + ".png");

            container.Offset = new Vector2(0, animationRectangle.Y);
            container.FramesCount = frameCount;
            container.FrameTime = frameTimes;
            animationRectangle.X = 0;
            container.StartSpriteRectangle = animationRectangle;
            container.TextureFrameInterval = 0;
            container.IsCycle = isCycle;
            container.Id = id;
            container.TextureName = "Textures/AnimationTextures/" + temp + "/" + textureEndName;
            string json = JsonConvert.SerializeObject(container);

            StreamWriter writer = new StreamWriter(result.Path + "/" + id + ".animation");//"../../../../ZoFo/Content/Textures/Animations/" + id + ".animation");
            writer.WriteLine(json);
            writer.Close();
        }
        public static string selectOnlyPathFromContent(string path)
        {
            return path.Split("Content")[1];
        }
    }
}
