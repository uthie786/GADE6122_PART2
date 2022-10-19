using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;

namespace Gade6122_Part1_corrected
{
    [Serializable]
    public class GameEngine
    {
        public Map map;
        public SwampCreature swampCreature;
        const string SERIALIZED_GAME_SAVE = "SwampGameSave.gdf";
        
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
            map = new Map(10, 20, 10, 20, 8, 4);
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
            Item item = map.GetItemAtPoisition(map.Hero.X, map.Hero.Y);
            if (item is Gold)
            {
                map.Hero.PickUp(item);
                map.UpdateMap();
            }

            map.Hero.Move(validMove);           
            map.UpdateMap();
            return true;
        }
        public string ShowEnemyStats(Movement direction)
        {
            if (direction == Movement.NoMovemnt)
            {
                return string.Empty;
            }
            Tile tile = map.Hero.Vision[(int)direction];
            if (!(tile is Enemy))
            {
                return string.Empty;                            
            }
            Enemy enemy = (Enemy)tile;
            return $"Enemy stats:\n HP = {enemy.HP}\n Damage = {enemy.Damage} \n Coordinates = ({enemy.X}, {enemy.Y})";
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
               if (enemy.IsDead)
                {
                   
                    return "Hero killed " + enemy.ToString();
                   
                    
                }
                return "Hero attacked: " + enemy.ToString();
            }

            
            return "Attack Failed, no enemy in this direction"; 
        }
        public void EnemyAttacks()
        {
            map.UpdateMap();
            for (int i = 0; i < map.Enemy.Length; i++)
            {
                if (map.Enemy[i] is SwampCreature && map.Enemy[i].IsDead == false)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        Tile tile = map.Enemy[i].Vision[x];
                        if (tile is Hero)
                        {
                            Hero hero = (Hero)tile;
                            map.Enemy[i].Attack(hero);
                        }
                    }
                }
                else if (map.Enemy[i] is Mage && map.Enemy[i].IsDead == false)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        Tile tile = map.Enemy[i].Vision[x];
                        if (tile is Hero)
                        {
                            Hero hero = (Hero)tile;
                            map.Enemy[i].Attack(hero);
                        }
                        if (tile is SwampCreature)
                        {
                            swampCreature = (SwampCreature)tile;
                           map.Enemy[i].Attack(swampCreature);
                        }
                    }
                }
                
               
            }
            
        }
        public void Save()
        {
            FileStream stream = new FileStream(SERIALIZED_GAME_SAVE, FileMode.Create, FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, map);
            stream.Close();
        }
        public void Load()
        {
            FileStream stream = new FileStream(SERIALIZED_GAME_SAVE, FileMode.Open, FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            map = (Map)bf.Deserialize(stream);
            stream.Close();
        }


    }
}
