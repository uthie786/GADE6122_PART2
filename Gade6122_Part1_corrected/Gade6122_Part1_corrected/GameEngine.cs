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
        
        public string Display //creates a string that outputs the map
        {
            get { return map.ToString(); }
        }
        public string HeroStats //prints out the hero stats
        {
            get { return map.Hero.ToString(); }
        }

        public GameEngine()
        { //creating the size of the map
            map = new Map(10, 20, 10, 20, 8, 4);
        }
        public bool MovePlayer(Movement direction) //mehtod that moves the player if a move is valid
        {
            Movement validMove = map.Hero.ReturnMove(direction);
            if (!map.Hero.IsDead)
            {
                if (direction == Movement.NoMovemnt)
                {
                    return false;
                }
               
                if (validMove == Movement.NoMovemnt)
                {
                    return false;
                }
                Item item = map.GetItemAtPoisition(map.Hero.X, map.Hero.Y); //if the hero moves onto an item the hero picks up that item
                if (item is Gold)
                {
                    map.Hero.PickUp(item);
                    map.UpdateMap();
                }
            }
            //hero moves and map is updated
            map.Hero.Move(validMove);           
            map.UpdateMap();
            return true;
        }
        public string ShowEnemyStats(Movement direction) //method that displays the stats of the enemy the hero is facing or attacking
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

        public string PlayerAttack(Movement direction) //method that lets the hero attack in a specific direction
        {
            Tile tile = map.Hero.Vision[(int)direction];
            if (!map.Hero.IsDead)
            {
                if (direction == Movement.NoMovemnt)
                {
                    return "Attack Failed!";
                }

                if (tile is Enemy)
                {
                    Enemy enemy = (Enemy)tile;
                    map.Hero.Attack(enemy);
                    if (enemy.IsDead)
                    {
                        //the attack is output into a label
                        return "Hero killed " + enemy.ToString();


                    }
                    return "Hero attacked: " + enemy.ToString();
                }
            }

            
            return "Attack Failed, no enemy in this direction"; 
        }
        public void EnemyAttacks() //method that lets enemies attack each other and the hero
        {
            map.UpdateMap(); //updates map first so enemies can attack correctly
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
                else if (map.Enemy[i] is Mage && map.Enemy[i].IsDead == false) //mages are able to friendly fire on swampcreatures
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
        public void Save() //saves the map by writing it to a binary file
        {
            FileStream stream = new FileStream(SERIALIZED_GAME_SAVE, FileMode.Create, FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, map);
            stream.Close();
        }
        public void Load()//reads the binary file and outputs back into the map
        {
            FileStream stream = new FileStream(SERIALIZED_GAME_SAVE, FileMode.Open, FileAccess.Read);
            BinaryFormatter bf = new BinaryFormatter();
            map = (Map)bf.Deserialize(stream);
            stream.Close();
        }


    }
}
