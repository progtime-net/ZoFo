using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using ZoFo.GameCore.GUI;

namespace ZoFo.GameCore.GameManagers
{ 
    public enum ScopeState { Idle, Left, Right, Top, Down, TopLeft, TopRight, DownLeft, DownRight }
    public class InputManager
    {
        public event Action ShootEvent; // событие удара(когда нажат X, событие срабатывает)
        
        public event Action OnInteract; // событие взаимодействия с collectable(например, лутом)
        //с помощью кнопки E.

        public event Action ActionEvent;
        public Vector2 InputMovementDirection;
        private Vector2 prevInputMovementDirection;
        public Vector2 InputAttackDirection;
        private Vector2 prevInputAttackDirection;
        public ScopeState currentScopeState;        // Положение оружия. Left, Right, Straight, Back, StraightLeft, StraightRight, BackLeft, BackRight.
        private ScopeState prevCurrentScopeState;
        private bool _cheatsEnabled = false;
        public bool InvincibilityCheat { get; private set; } = false;
        public bool CollisionsCheat { get; private set; } = false;
        public bool InfiniteAmmoCheat { get; private set; } = false;

        private bool isShoot;
        private bool isInteract;

        private KeyboardState lastKeyboardState;
        private KeyboardState keyBoardState;
        private GamePadState lastGamePadState;
        public ScopeState ScopeState { get => currentScopeState; }
        public string currentControlsState;
        public ScopeState CurrentScopeState { get => currentScopeState; } // получить текущее состояние

        public InputManager()
        {
            isInteract = true;
            InputMovementDirection = new Vector2(0, 0);
            InputAttackDirection = new Vector2(0, 0);
            this.isShoot = false;
            currentScopeState = ScopeState.Idle;
        }
        public void Update()
        {
            if (_cheatsEnabled)
            {
                AppManager.Instance.debugHud.Set("cheats", _cheatsEnabled.ToString());
                AppManager.Instance.debugHud.Set("invincible", InvincibilityCheat.ToString());
                AppManager.Instance.debugHud.Set("infinite ammo", InfiniteAmmoCheat.ToString()); //TODO
            }

            #region Работа с GamePad
                #region Обработка гейм-пада. Задает Vector2 vectorMovementDirection являющийся вектором отклонения левого стика.
                GamePadState gamePadState = GamePad.GetState(0);
                InputMovementDirection = gamePadState.ThumbSticks.Left;
                InputAttackDirection = gamePadState.ThumbSticks.Right;
                #endregion

                #region читы 
                if (gamePadState.Triggers.Left >= 0.9 && gamePadState.Triggers.Right >= 0.9)
                    _cheatsEnabled = true;
                if (_cheatsEnabled)
                {
                    if (gamePadState.Buttons.Y == ButtonState.Pressed && lastGamePadState.Buttons.Y == ButtonState.Released)
                        InvincibilityCheat = !InvincibilityCheat;
                    if (gamePadState.Buttons.B == ButtonState.Pressed && lastGamePadState.Buttons.B == ButtonState.Released)
                        CollisionsCheat = !CollisionsCheat;
                    //TODO: infinite ammo cheat by gamepad
                }
                #endregion // Cheats

                #region set ScopeState
                ConvertVector2ToState(InputMovementDirection);
                #endregion

                #region Обработка нажатия выстрела. Вызывает событие ShootEvent
                if (gamePadState.Buttons.X == ButtonState.Pressed && !isShoot)
                {
                    isShoot = true;
                    ShootEvent?.Invoke();
                    Debug.WriteLine("Выстрел");
                }
                else if (gamePadState.Buttons.X == ButtonState.Released)
                {
                    isShoot = false;
                }
                #endregion

                lastGamePadState = gamePadState;
            #endregion
            #region Работа с KeyBoard

                #region InputAttack with mouse
                MouseState mouseState = Mouse.GetState();
                AppManager.Instance.debugHud.Set("mouse position", $"({mouseState.X}, {mouseState.Y}");
                // TODO: CurentScreenResolution
                Vector2 a = (AppManager.Instance.CurentScreenResolution / new Point(2, 2)).ToVector2();

                InputAttackDirection = Vector2.Normalize(new Vector2(mouseState.X - a.X, mouseState.Y - a.Y));
                AppManager.Instance.debugHud.Set("AttackDir(normalize)", $"({a.X}, {a.Y})");
                #endregion

                #region Состояние клавиатуры
                KeyboardState keyBoardState = Keyboard.GetState();  // Состояние клавиатуры
                #endregion
                
                #region читы 
                if (keyBoardState.IsKeyDown(Keys.LeftShift) && keyBoardState.IsKeyDown(Keys.RightShift))
                    _cheatsEnabled = true;
                if (_cheatsEnabled)
                {
                    if (keyBoardState.IsKeyDown(Keys.I) && lastKeyboardState.IsKeyUp(Keys.I))
                        InvincibilityCheat = !InvincibilityCheat;
                    if (keyBoardState.IsKeyDown(Keys.C) && lastKeyboardState.IsKeyUp(Keys.C))
                        CollisionsCheat = !CollisionsCheat;
                    if (keyBoardState.IsKeyDown(Keys.N) && lastKeyboardState.IsKeyUp(Keys.N))
                        InfiniteAmmoCheat = !InfiniteAmmoCheat;

                    List<Keys> lvls = new List<Keys>() { Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9 };

                    for (int i = 0; i < lvls.Count; i++)
                    {
                        //if (keyBoardState.IsKeyDown(lvls[i]) && lastKeyboardState.IsKeyUp(lvls[i])) //TODO
                        //    AppManager.Instance.Restart($"lvl{i}");
                    }
                }
                #endregion // Cheats

                #region Обработка состояния объекта. Задает значение полю scopeState. 
                if (keyBoardState.IsKeyDown(Keys.Up) || keyBoardState.IsKeyDown(Keys.W))
                {
                    InputMovementDirection += new Vector2(0, -1); 
                }
                if (keyBoardState.IsKeyDown(Keys.Down) || keyBoardState.IsKeyDown(Keys.S))
                { 
                    InputMovementDirection += new Vector2(0, 1); 
                }
                if (keyBoardState.IsKeyDown(Keys.Right) || keyBoardState.IsKeyDown(Keys.D))
                { 
                    InputMovementDirection += new Vector2(1, 0); 
                }
                if (keyBoardState.IsKeyDown(Keys.Left) || keyBoardState.IsKeyDown(Keys.A))
                { 
                    InputMovementDirection += new Vector2(-1, 0);

                }
                ConvertVector2ToState(InputMovementDirection);
                #endregion

                #region Обработка нажатия выстрела. Вызывает событие ShootEvent
                if ((keyBoardState.IsKeyDown(Keys.P) || keyBoardState.IsKeyDown(Keys.F)) && !isShoot)
                {
                    isShoot = true;
                    ShootEvent?.Invoke();
                    Debug.WriteLine("Выстрел");
                }
                else if (keyBoardState.IsKeyUp(Keys.F))
                {
                    isShoot = false;
                }
                #endregion

                #region Обработка взаимодействия с collectable(например лутом). Вызывает событие OnInteract
                if (keyBoardState.IsKeyDown(Keys.E) && !isInteract)
                {
                    OnInteract?.Invoke();
                    Debug.WriteLine("взаимодействие с Collectable");
                }
                else if (keyBoardState.IsKeyUp(Keys.E))
                {
                    isInteract = false;
                }
                #endregion
                lastKeyboardState = keyBoardState;
            
            #endregion 
            #region ActionEvent
                if(InputMovementDirection != prevInputMovementDirection  ||
                InputAttackDirection != prevInputAttackDirection || 
                currentScopeState != prevCurrentScopeState)
                {
                    ActionEvent?.Invoke();
                }
                prevInputMovementDirection = InputMovementDirection;
                prevInputAttackDirection = InputAttackDirection;
                prevCurrentScopeState = currentScopeState;
            #endregion
        
            DebugHUD.Instance.Set("controls", currentScopeState.ToString());
        }
        #region работа с ScopeState и Vector2
            /// <summary>
            /// возвращает число от -14 до 16, начиная с 
            /// </summary>
            /// <param name="vector"></param>
            /// <returns></returns>
            public int ConvertAttackVector2ToState(Vector2 vector){
                int currentSection = (int)Math.Ceiling(Math.Atan2(vector.Y,
                vector.X) * (180 / Math.PI) / 360 * 32);
                return currentSection;
            }
            public ScopeState ConvertVector2ToState(Vector2 vector)
            {
                int currentSection = 0;
                if(vector.X == 0f && vector.Y == 0f){
                    currentScopeState = ScopeState.Idle;
                }
                else
                {
                    currentSection = (int)Math.Ceiling(Math.Atan2(vector.Y,
                    vector.X) * (180 / Math.PI) / 360 * 16);
                

                switch(currentSection)
                {
                    case -1:
                        currentScopeState = ScopeState.Idle;
                        break;
                    case 0 or 1:
                    currentScopeState = ScopeState.Right;
                    break; 
                    case 2 or 3:
                    currentScopeState = ScopeState.DownRight;
                    break;
                    case 4 or 5:
                    currentScopeState = ScopeState.Down;
                    break;
                    case 6 or 7:
                    currentScopeState = ScopeState.DownLeft;
                    break;
                    case 8 or -7:
                    currentScopeState = ScopeState.Left;
                    break;
                    case -6 or -5:
                    currentScopeState = ScopeState.TopLeft;
                    break;
                    case -4 or -3:
                    currentScopeState = ScopeState.Top;
                    break;
                    case -2 or -1:
                    currentScopeState = ScopeState.TopRight;
                    break;
                    default:
                    break;
                }
                
                DebugHUD.DebugSet("current section", currentSection.ToString());
                DebugHUD.DebugSet("y", vector.Y.ToString());
                DebugHUD.DebugSet("x", vector.X.ToString());
                }
                return currentScopeState;
        }
        public static Vector2 ConvertStateToVector2(ScopeState scopeState)
        {
            switch (scopeState)
            {
                case ScopeState.Idle:
                    return new Vector2(0, 0);
                case ScopeState.Left:
                    return new Vector2(-1, 0);
                case ScopeState.Right:
                    return new Vector2(1, 0);
                case ScopeState.Top:
                    return new Vector2(0, -1);
                case ScopeState.Down:
                    return new Vector2(0, 1);
                case ScopeState.TopLeft:
                    return new Vector2(-1, -1);
                case ScopeState.TopRight:
                    return new Vector2(-1, 1);
                case ScopeState.DownLeft:
                    return new Vector2(1, -1);
                case ScopeState.DownRight:
                    return new Vector2(1, 1);
                default:
                    return new Vector2(0, 0);
            }  


        }
        #endregion
        public bool ButtonClicked(Keys key) => keyBoardState.IsKeyUp(key) && keyBoardState.IsKeyDown(key);
    }

}