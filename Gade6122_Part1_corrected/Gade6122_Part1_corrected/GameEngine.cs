using System;
using System.Collections.Generic;
using System.Text;

namespace Gade6122_Part1_corrected
{
    public class GameEngine
    {
        Map map;
        
        public string Display
        {
            get { return map.ToString(); }
        }
        public string HeroStats
        {
            get { return map.Hero.ToString(); }
        }
        public GameEngine()
        {
            map = new Map(10, 20, 10, 20, 8);
        }
        public bool MovePlayer(Movement direction)
        {
            if (direction == Movement.NoMovemnt)
            {
                return false;
            }
            Movement validMove = map.Hero.ReturnMove(direction);
            if (validMove == Movement.NoMovemnt)
            {
                return false;
            }

            map.Hero.Move(validMove);
            map.UpdateMap();
            return true;
        }
        public string PlayerAttack(Movement direction)
        {
            if (direction == Movement.NoMovemnt)
            {
                return "Attack Failed!";
            }
            Tile tile = map.Hero.Vision[(int)direction];
            if (tile is Enemy)
            {
                Enemy enemy = (Enemy)tile;
                map.Hero.Attack(enemy);
                return "Hero attacked: " + enemy.ToString();
            }
            return "Attack Failed, no enemy in this direction";
        }
    }
}
