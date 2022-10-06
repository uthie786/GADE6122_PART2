using System;
using System.Collections.Generic;
using System.Text;

namespace Gade6122_Part1_corrected
{
    public enum TileType
    {
        Hero,
        Enemy,
        Gold,
        Weapon,
        None
    }
    public abstract class Tile
    {
        protected int x;
        protected int y;
        protected TileType type;

        public TileType Type
        {
            get { return type; }
            set { type = value; }
        }
        public int X
        {
            get { return x; }
        }
        public int Y
        {
            get { return y; }
        }

        public Tile(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.type = TileType.None;
        }
    }
}
