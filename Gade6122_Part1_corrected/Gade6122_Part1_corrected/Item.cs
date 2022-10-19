using System;
using System.Collections.Generic;
using System.Text;

namespace Gade6122_Part1_corrected
{
    [Serializable]
    public abstract class Item : Tile //inherits from tile
    {
        public Item(int x, int y) : base(x, y)
        {

        }

        public abstract override string ToString(); //returns the type of item 



    }
}
