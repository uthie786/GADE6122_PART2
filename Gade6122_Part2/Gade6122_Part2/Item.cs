using System;
using System.Collections.Generic;
using System.Text;

namespace Gade6122_Part1_corrected
{
    public abstract class Item : Tile
    {
        public Item(int x, int y) : base(x, y)
        {

        }

        public abstract override string ToString();
        

        
    }
}
