using System;
using System.Collections.Generic;
using System.Text;

namespace Gade6122_Part1_corrected
{
    [Serializable]
    public abstract class Enemy : Character //Enemy character
    {
       protected static Random random = new Random();
       
        public Enemy(int x, int y, int damage, int hp) : base(x, y) //constructor for enemy class
        {
            this.damage = damage;
            this.hp = hp;
            this.maxHp = hp;
            type = TileType.Enemy;
        }
        public override string ToString() //method used to display attacking information on the window
        {
            return GetType().Name + " at [" + x + ", " + y + "] (" + damage + "dmg)"; 
        }

        
    }
}
