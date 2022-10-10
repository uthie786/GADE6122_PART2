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
            GoldAmount = random.Next(0, 6);
        }

        public int GoldAmount
        {
            get { return goldAmount; }
            set { goldAmount = value; }
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
