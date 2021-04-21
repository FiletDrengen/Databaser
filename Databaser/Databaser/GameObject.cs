using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Databaser
{
    public abstract class GameObject
    {
        protected Texture2D sprite;
        protected Vector2 velocity;
        protected Vector2 position;
        protected Color color = Color.White;
        protected SpriteBatch _spriteBatch;

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}