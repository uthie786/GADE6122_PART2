using System;
using System.Collections.Generic;
using System.Text;

namespace Gade6122_Part1_corrected
{
    [Serializable]
    public class Gold : Item //inherits from the item class
    {
        private int goldAmount; //amount of gold represented by a gold drop 
        [NonSerialized] private Random random = new Random(); //randomises gold amounts

        public Gold(int x,int y) : base(x, y)
        {
            GoldAmount = random.Next(0, 6); //sets the amount to a random amount between 1 and 5
        }

        public int GoldAmount //public accessor that can return that amount
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
