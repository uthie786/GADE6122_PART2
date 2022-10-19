﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Gade6122_Part1_corrected
    
{
    [Serializable]
    public class SwampCreature : Enemy
    {
        public SwampCreature(int x, int y) : base(x, y, 1, 10)
        {

        }

        public override Movement ReturnMove(Movement move = Movement.NoMovemnt)
        {
            List<int> checkedDirection = new List<int>();
            
            int index = random.Next(4);
            while (checkedDirection.Count < 4 && (!(vision[index] is EmptyTile) || (vision[index] is Hero) || (vision[index]is SwampCreature)))
            {
                if (checkedDirection.Contains(index))
                    checkedDirection.Add(index);

                index = random.Next(4);
            }
                if (checkedDirection.Count == 4)
                    return Movement.NoMovemnt;
                
                return (Movement)index;
            
        }

    }
}
