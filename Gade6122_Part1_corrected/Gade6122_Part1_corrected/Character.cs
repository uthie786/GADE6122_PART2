using System;
using System.Collections.Generic;
using System.Text;

namespace Gade6122_Part1_corrected
{
    [Serializable]
    public enum Movement //enum for every movement direction and a no movement option
    { 
        Up,
        Right,
        Down,
        Left,
        NoMovemnt
    }
    [Serializable]
    public abstract class Character : Tile
    {
       //protected variables for the characters
        protected int hp;
        protected int maxHp;
        protected int damage;
        private int purse;
        protected Tile[] vision; //vision array thats used to checks tiles around a character

        public int HP { get { return hp; } }
        public int MaxHP { get { return maxHp; } }
        public int Damage { get { return damage; } }
        public Tile[] Vision { get { return vision; } }

        public bool IsDead //method that checks a characters HP and returns a boolean
        {
            get { return hp <= 0; }
        }
       public int Purse //Purse used for storing gold
        {
            get { return purse; }
            set { purse = value; }

        }

        public Character(int x, int y) : base(x, y) //character constructor
        {
            vision = new Tile[8];
        }
        public virtual void Attack(Character target) //method used by characters to attack other characters by reducing their HP
        {
            target.hp -= damage;
            if(target.hp < 0)
            {
                target.hp = 0;
            }
        }

        public virtual bool CheckRange(Character target) //checks distance between two characters
        {
            return DistanceTo(target) <= 1;
        }
        public void Move(Movement move)
        {
            if(move == Movement.NoMovemnt)
            {
                return;
            }
            switch (move)
            {
                case Movement.Up: y--; break;
                case Movement.Down: y++; break;
                case Movement.Left: x--; break;
                case Movement.Right: x++; break;
            }    
        }

        public void UpdateVision(Tile[,] map) //method that updates the vision array for a character
        {
            //up
            vision[0] = y - 1 >= 0 ? map[x, y - 1] : null;
            //right
            vision[1] = x < map.GetLength(0) ? map[x + 1, y] : null;
            //down
            vision[2] = y + 1 < map.GetLength(1) ?  map[x , y+ 1] : null;
            //left
            vision[3] = x - 1 >= 0 ? map[x - 1, y] : null;
            //up + left
            vision[4] = y - 1 >= 0 && x - 1 >= 0 ? map[x - 1, y - 1] : null;
            //up + right
            vision[5] = y - 1 >= 0 && x < map.GetLength(0) ? map[x + 1, y - 1] : null;
            //down + left 
            vision[6] = y + 1 < map.GetLength(1) && x-1>= 0 ? map[x - 1, y + 1] : null;
            //down + right 
            vision[7] = y + 1 < map.GetLength(1) && x < map.GetLength(0) ? map[x + 1, y + 1] : null;
        }

        public abstract Movement ReturnMove(Movement move = Movement.NoMovemnt);

        public abstract override string ToString();
        
        private int DistanceTo(Tile target) //calculates the distance between two tiles
        {
            int xDist = Math.Abs(target.X - x);
            int yDist = Math.Abs(target.Y - y);
            return xDist + yDist;
        }

        public void PickUp(Item item) //method that lets a character pick up gold and store in purse
        {
            if (item is Gold)
            {
                int goldAmount = ((Gold)item).GoldAmount;
                Purse += goldAmount;
            }
        }

        
    }
}
