using System;
using System.Collections.Generic;
using System.Text;

namespace Gade6122_Part1_corrected
{
    [Serializable]
    public enum TileType //enum for the different tile types
    {
        Hero,
        Enemy,
        Gold,
        Weapon,
        None
    }
    [Serializable]
    public abstract class Tile //tile class
    {
        //variables for coordinates and type
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

        public Tile(int x, int y) //constructor
        {
            this.x = x;
            this.y = y;
            this.type = TileType.None;
        }
    }
}
