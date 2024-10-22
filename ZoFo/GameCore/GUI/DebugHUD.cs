﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameLibrary.UI.Elements;
using static System.String;
using ZoFo.GameCore.GameManagers;

namespace ZoFo.GameCore.GUI;

public class DebugHUD
{
    private SpriteFont _spriteFont;
    private Dictionary<string, string> _text = new();
    private List<string> _log = new();
    public static DebugHUD Instance { get; private set; }

    public void Initialize()
    {
        Instance = this;
    }

    public void LoadContent()
    {
        _spriteFont = AppManager.Instance.Content.Load<SpriteFont>("Fonts/Font2");
    }

    public void Update(GameTime gameTime)
    {
    }

    public void Draw(SpriteBatch spriteBatch)
    {
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
    public static void DebugLog(string value)
    {
        Instance._log.Add(value);
        if (Instance._log.Count > 30)
        {
            Instance._log.RemoveAt(0);
        }
    }
}