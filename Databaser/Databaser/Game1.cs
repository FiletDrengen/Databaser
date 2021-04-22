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
        public Texture2D userbackground;

        public Realm realm;

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
        }

        public void Fishing()
        {
            System.Threading.Thread.Sleep(5000);
            score += _random.Next(0, 3);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if (state.IsKeyDown(Keys.Escape))
                Exit();

            var keyboardState = Keyboard.GetState();
            var keys = keyboardState.GetPressedKeys();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (userlog.Rectangle.Contains(mouseState.Position))
                {
                    if (keys.Length > 0)
                    {
                        var keyValue = keys[0].ToString();
                        userValue += keyValue;
                    }
                }
                if (passlog.Rectangle.Contains(mouseState.Position))
                {
                    if (keys.Length > 0)
                    {
                        var keyValue = keys[0].ToString();
                        passValue += keyValue;
                    }
                }
            }

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                foreach (Tile n in map.Tiles)
                {
                    {
                        if (n.Rectangle.Contains(mouseState.Position))
                        {
                            Fishing();
                        }
                    }
                }
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            map.Draw(_spriteBatch);
            _spriteBatch.Draw(userbackground, new Rectangle(userlog.Rectangle.X - 100, userlog.Rectangle.Y - 20, 300, 140), Color.White);
            userlog.Draw(_spriteBatch);
            passlog.Draw(_spriteBatch);
            _spriteBatch.DrawString(userfont, "Username:  " + userValue, new Vector2(userlog.Rectangle.X - 80, userlog.Rectangle.Y + 10), Color.Black);
            _spriteBatch.DrawString(userfont, "Password:  " + passValue, new Vector2(passlog.Rectangle.X - 80, passlog.Rectangle.Y + 10), Color.Black);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}