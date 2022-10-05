using System;
using System.Collections.Generic;
using System.Text;

namespace Gade6122_Part1_corrected
{
    public class Gold : Item
    {
        private int goldAmount;
        private Random random = new Random();
        
        public Gold(int x,int y) : base(x, y)
        {
            int rng = random.Next(0, 6);
        }
    }
}
