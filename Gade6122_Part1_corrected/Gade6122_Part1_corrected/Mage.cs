using System;
using System.Collections.Generic;
using System.Text;

namespace Gade6122_Part1_corrected
{
    [Serializable]
    public class Mage : Enemy //mage class inherits from Enemy
    {
        public Mage(int x, int y): base(x, y, 5, 5)
        {

        }
        public override Movement ReturnMove(Movement move) //this method always returns a 0, as a Mage never moves
        {
            return Movement.NoMovemnt;
        }
        public override bool CheckRange(Character target) //mages attack all characters around them in a one block radius
        {
            return base.CheckRange(target);
        }
    }
}
