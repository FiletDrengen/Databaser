using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Databaser
{
    public class Map : GameObject
    {
        public int width, height;
        public List<CoolTile> Tiles = new List<CoolTile>();

        public Map()
        {
        }

        public void Gen(int[,] map, int size)
        {
            for (int i = 0; i < map.GetLength(1); i++)
            {
                for (int j = 0; j < map.GetLength(0); j++)
                {
                    int number = map[j, i];

                    if (number > 0)
                        Tiles.Add(new CoolTile(number, new Rectangle(i * size, j * size, size, size)));

                    width = (i + 1) * size;
                    height = (j + 1) * size;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in Tiles)
            {
                tile.Draw(spriteBatch);
            }
        }
    }
}