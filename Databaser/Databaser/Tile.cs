using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Databaser
{
    public class Tile : GameObject
    {
        public Texture2D tile;

        public Rectangle Rectangle;
        public static ContentManager content;

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tile, Rectangle, color);
        }
    }

    public class CoolTile : Tile
    {
        public CoolTile(int i, Rectangle newRectangle)
        {
            tile = content.Load<Texture2D>("Tile" + i);
            this.Rectangle = newRectangle;
        }
    }
}