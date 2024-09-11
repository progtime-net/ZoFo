using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameLibrary.UI.Elements;
using static System.String;
using ZoFo.GameCore.GameManagers;
using static System.Net.Mime.MediaTypeNames;
using System;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;

namespace ZoFo.GameCore.GUI;

public class DebugHUD
{
    private SpriteFont _spriteFont;
    private Dictionary<string, string> _text = new();
    private List<string> _log = new();
    public static DebugHUD Instance { get; private set; }
    public Texture2D noTexture;
    public static bool IsActivated = true;
    public void Initialize()
    {
        Instance = this;

    }

    public void LoadContent()
    {
        _spriteFont = AppManager.Instance.Content.Load<SpriteFont>("Fonts/Font2");
    }

    bool prev_KeyState;//SHould move this logic and deneralize to main input manager
    public void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.D1) && !prev_KeyState)
            ChangeStatLayer();
        
        prev_KeyState = Keyboard.GetState().IsKeyDown(Keys.D1);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!IsActivated) return;                
        //return;//TODO delete
        var keysString = Join("\n", _text.Select(el => el.Key + ": " + el.Value).ToList());
        spriteBatch.Begin();
        spriteBatch.DrawString(
            _spriteFont,
            keysString,
            new Vector2(10, 10),
            Color.Cyan,
            0,
            Vector2.Zero,
            1,
            SpriteEffects.None,
            0
        );
        spriteBatch.DrawString(
            _spriteFont,
            Join("\n", _log),
            new Vector2(10, 10 + _spriteFont.MeasureString(keysString).Y),
            Color.Green,
            0,
            Vector2.Zero,
            1,
            SpriteEffects.None,
            0
        );

        DrawGameObjecctsCounter(spriteBatch);

        spriteBatch.End();

    }

    public void Set(string key, string value)
    {
        _text[key] = value;
    }
    public static void DebugSet(string key, string value)
    {
        Instance._text[key] = value;
    }

    public void Log(string value)
    {
        _log.Add(value);
        if (_log.Count > 30)
        {
            _log.RemoveAt(0);
        }
    }

    static int totalSavedStatistics = 100;
    public static void DebugLog(string value)
    {
        Instance._log.Add(value);
        if (Instance._log.Count > totalSavedStatistics)
        {
            Instance._log.RemoveAt(0);
        }
    }

    public static Dictionary<string, List<int>> gameObjectsStatistics = new();
    public static string currentListName = "";
    private static int colorDelta = 0;
    private static int statLayer = 0;
    public static void ChangeStatLayer()
    {
        if (gameObjectsStatistics.Count == 0) return;
        
        statLayer = (statLayer + 1) % gameObjectsStatistics.Count;
        currentListName = gameObjectsStatistics.ToArray()[statLayer].Key;
        DebugSet("statistics list name:", currentListName);

    }
    public static void AddGOData(int data, string listName)
    {
        if (!gameObjectsStatistics.ContainsKey(listName))
        {

            gameObjectsStatistics.Add(listName, new List<int>());
            DebugSet("statistics list name:", currentListName);

        }
        if (currentListName == "")
            currentListName = listName;
        DebugSet("statistics list current parametr:", data.ToString());
        gameObjectsStatistics[listName].Add(data);
        if (gameObjectsStatistics[listName].Count > totalSavedStatistics)
        {
            gameObjectsStatistics[listName].RemoveAt(0);
            if (currentListName == listName)
                colorDelta++;



        }

    }
    public static void AddAdditionalDataToGraph(int data, string listName)
    {
        if (!gameObjectsStatistics.ContainsKey(listName))
        {
            gameObjectsStatistics.Add(listName, new List<int>());
            DebugSet("statistics list name:", currentListName);

        }
        if (currentListName == "")
            currentListName = listName;
        if (gameObjectsStatistics[listName].Count() == 0)
            gameObjectsStatistics[listName].Add(0);

        gameObjectsStatistics[listName][gameObjectsStatistics[listName].Count() - 1] += data;
        DebugSet("statistics list current parametr:", gameObjectsStatistics[listName][gameObjectsStatistics[listName].Count() - 1].ToString());

        if (gameObjectsStatistics[listName].Count > totalSavedStatistics)
        {
            gameObjectsStatistics[listName].RemoveAt(0);
            if (currentListName == listName)
                colorDelta++;



        }

    }
    static Color[] colorsForGraphic = new Color[] {
    new Color(255,0,0),
    new Color(250,0,0),
    new Color(245,0,0),
    new Color(240,0,0),
    new Color(235,0,0),
    new Color(240,0,0),
    new Color(245,0,0),
    };
    public void DrawGameObjecctsCounter(SpriteBatch spriteBatch)
    {
        if (gameObjectsStatistics.Count == 0) return;
        if (gameObjectsStatistics[currentListName].Count == 0) return;

        Point leftTopPoint      = (SettingsManager.Instance.Resolution.ToVector2() * new Vector2(0.8f, 0.8f)).ToPoint();
        Point rightBottomPoint = (SettingsManager.Instance.Resolution.ToVector2() * new Vector2(0.99f, 0.95f)).ToPoint();

        Point leftBottomPoint   = new Point(leftTopPoint.X, rightBottomPoint.Y);
        Point rightTopPoint     = new Point(rightBottomPoint.X, leftTopPoint.Y);
        int max = gameObjectsStatistics[currentListName].Max();

        spriteBatch.Draw(noTexture, new Rectangle(leftTopPoint, rightBottomPoint - leftTopPoint
            ), new Color(Color.Gray, 0.4f));
        for (int i = 0; i < gameObjectsStatistics[currentListName].Count; i++)
        {
            int val = gameObjectsStatistics[currentListName][i];
            int needToMax = (int)((1 - ((float)val)/max) * (leftBottomPoint.Y - leftTopPoint.Y));
            float sizeOfBar = (rightBottomPoint.X - leftBottomPoint.X)/ (float)totalSavedStatistics;
            int dX = (int)(i * sizeOfBar);

            Color col = colorsForGraphic[Math.Abs(colorDelta + i) % colorsForGraphic.Count()];
            

            spriteBatch.Draw(noTexture, new Rectangle(
                leftBottomPoint.X + dX,
                leftTopPoint.Y + (needToMax),
                (int)Math.Ceiling(sizeOfBar),
                (leftBottomPoint.Y - leftTopPoint.Y) - needToMax
                ), col);
        }
    }
}