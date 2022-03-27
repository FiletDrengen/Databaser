using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Databaser
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Map map;
        private int score;
        private readonly Random _random = new Random();
        private string passValue = string.Empty;
        private string userValue = string.Empty;
        public SpriteFont userfont;
        public Login userlog;
        public Login passlog;
        public Login userlogin;
        public Texture2D userbackground;
        public textfied currenttexfied;
        public bool hidelogin = true;

        public KeyboardState currentKeyState;
        public KeyboardState previousKeyState;
        public Realm realm;
        private Keys keyValue;
        private HighScore[] highscorearray;

        public enum textfied
        {
            none,
            user,
            pass
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Login.DatabaseSetup();
            map = new Map();
            realm = new Realm();
            Login.DatabaseSetup();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            userfont = Content.Load<SpriteFont>("File");
            Tile.content = Content;
            Login.content = Content;
            realm.Map3(map);
            userbackground = Content.Load<Texture2D>("Tile1");
            userlog = new Login(new Rectangle(650, 50, 100, 30));
            passlog = new Login(new Rectangle(650, 100, 100, 30));
            userlogin = new Login(new Rectangle(650, 150, 100, 30));
            highscorearray = Login.Loadhighscore();
        }

        public void Fishing()
        {
            System.Threading.Thread.Sleep(5000);
            score += _random.Next(0, 3);
        }

        private bool KeypressTest(Keys theKey)
        {
            if (currentKeyState.IsKeyUp(theKey) && previousKeyState.IsKeyDown(theKey))
                return true;

            return false;
        }

        protected override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            currentKeyState = Keyboard.GetState();
            var keys = currentKeyState.GetPressedKeys();

            if (currentKeyState.IsKeyDown(Keys.Escape))
                Exit();

            if (userlog.Rectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
                currenttexfied = textfied.user;
            else if (passlog.Rectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
                currenttexfied = textfied.pass;
            else if (mouseState.LeftButton == ButtonState.Pressed)
                currenttexfied = textfied.none;

            if (currenttexfied == textfied.user)
            {
                if (KeypressTest(keyValue))
                {
                    userValue += keyValue.ToString();
                    if (previousKeyState.IsKeyDown(Keys.Back))
                    {
                        int i = userValue.Length - 5;
                        if (i <= 0)
                        {
                            i = 5;
                        }
                        userValue = userValue.Remove(i, 5);
                    }
                }
                else if (keys.Length > 0)
                    keyValue = keys[0];
            }

            if (currenttexfied == textfied.pass)
            {
                if (KeypressTest(keyValue))
                {
                    passValue += keyValue.ToString();
                    if (previousKeyState.IsKeyDown(Keys.Back))
                    {
                        passValue = passValue.Remove(passValue.Length - 5, 5);
                    }
                }
                else if (keys.Length > 0)
                    keyValue = keys[0];
            }

            foreach (Tile n in map.Tiles)
            {
                if (n.Rectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
                {
                    Fishing();
                }
            }

            if (userlogin.Rectangle.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
            {
                if (Login.Userlogin(userValue, passValue))
                {
                    hidelogin = false;
                }
            }

            previousKeyState = currentKeyState;

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (hidelogin == true)
            {
                _spriteBatch.Draw(userbackground, new Rectangle(userlog.Rectangle.X - 100, userlog.Rectangle.Y - 20, 300, 180), Color.White);
                userlog.Draw(_spriteBatch);
                passlog.Draw(_spriteBatch);
                userlogin.Draw(_spriteBatch);
                _spriteBatch.DrawString(userfont, "Username:  " + userValue, new Vector2(userlog.Rectangle.X - 80, userlog.Rectangle.Y + 10), Color.Black);
                _spriteBatch.DrawString(userfont, "Password:  " + passValue, new Vector2(passlog.Rectangle.X - 80, passlog.Rectangle.Y + 10), Color.Black);
                _spriteBatch.DrawString(userfont, "Login  ", new Vector2(userlogin.Rectangle.X + 30, userlogin.Rectangle.Y + 5), Color.Black);
            }
            else
            {
                _spriteBatch.DrawString(userfont, "Username: " + userValue, new Vector2(100, 1), Color.Black);
            }
            map.Draw(_spriteBatch);
            _spriteBatch.DrawString(userfont, "Score:  " + score, new Vector2(1, 1), Color.Black);

            foreach (HighScore highscore in highscorearray)
            {
                var realm = highscore.realm.ToString();
                var user = highscore.user.ToString();
                var score = highscore.highscore.ToString();

                var loadout = "User:        Realm:         Highscore";
                _spriteBatch.DrawString(userfont, loadout, new Vector2(0, 360), Color.Black);
                _spriteBatch.DrawString(userfont, user, new Vector2(0, 380), Color.Black);
                _spriteBatch.DrawString(userfont, realm, new Vector2(70, 380), Color.Black);
                _spriteBatch.DrawString(userfont, score, new Vector2(152, 380), Color.Black);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}