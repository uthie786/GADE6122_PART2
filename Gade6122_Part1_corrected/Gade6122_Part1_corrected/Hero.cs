using System;
using System.Collections.Generic;
using System.Text;

namespace Gade6122_Part1_corrected
{
    [Serializable]
    public class Hero : Character //Hero character
    {
        public Hero(int x, int y, int hp) : base(x,y)
        {
            this.hp = hp;
            this.maxHp = hp;
            this.damage = 2;
        }
        //going to have to change for hero moving onto a GOLD or WEAPON tile for example
        //for now Hero can only move onto EmptyTiles
        public override Movement ReturnMove(Movement move = Movement.NoMovemnt)
        {
            if (move == Movement.NoMovemnt)
            {
                return move;
            }
            if (vision[(int)move] is EmptyTile || vision[(int)move] is Gold)
            {
                return move;
            }
            
            return Movement.NoMovemnt;
        }
        public override string ToString() //tostring method thats used to print the Hero stats
        {
            return $"Player stats: \n" +
                $"HP: {hp}/{maxHp}\n" +
                $"Damage: {damage} \n" +
                $"Total Gold: {Purse}\n" +
                $"[{x}, {y}]";                   
        }
    }
}
