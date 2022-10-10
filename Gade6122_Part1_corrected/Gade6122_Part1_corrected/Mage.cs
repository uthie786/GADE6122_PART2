using System;
using System.Collections.Generic;
using System.Text;

namespace Gade6122_Part1_corrected
{
    internal class Mage : Enemy
    {
        public Mage(int x, int y): base(x, y, 5, 5)
        {

        }
        public override Movement ReturnMove(Movement move) //not too sure about this
        {
            return Movement.NoMovemnt;
        }
        public override bool CheckRange(Character target)
        {
            return base.CheckRange(target);
        }
    }
}
