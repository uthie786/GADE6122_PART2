using System;
using System.Collections.Generic;
using System.Text;

namespace Gade6122_Part1_corrected
{
    public enum Movement 
    { 
        Up,
        Right,
        Down,
        Left,
        NoMovemnt
    }

    public abstract class Character : Tile
    {
        protected int hp;
        protected int maxHp;
        protected int damage;
        protected Tile[] vision;

        public int HP { get { return hp; } }
        public int MaxHP { get { return maxHp; } }
        public int Damage { get { return damage; } }
        public Tile[] Vision { get { return vision; } }

        public bool IsDead
        {
            get { return hp <= 0; }
        }

        public Character(int x, int y) : base(x, y)
        {
            vision = new Tile[4];
        }
        public virtual void Attack(Character target)
        {
            target.hp -= damage;
            if(target.hp < 0)
            {
                target.hp = 0;
            }
        }

        public virtual bool CheckRange(Character target)
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

        public void UpdateVision(Tile[,] map)
        {
            //up
            vision[0] = y - 1 >= 0 ? map[x, y - 1] : null;
            //right
            vision[1] = x < map.GetLength(0) ? map[x + 1, y] : null;
            //down
            vision[2] = y + 1 < map.GetLength(1) ?  map[x , y+ 1] : null;
            //left
            vision[3] = x - 1 >= 0 ? map[x - 1, y] : null;
        }

        public abstract Movement ReturnMove(Movement move = Movement.NoMovemnt);

        public abstract override string ToString();
        
        private int DistanceTo(Tile target)
        {
            int xDist = Math.Abs(target.X - x);
            int yDist = Math.Abs(target.Y - y);
            return xDist + yDist;
        }

        
    }
}
