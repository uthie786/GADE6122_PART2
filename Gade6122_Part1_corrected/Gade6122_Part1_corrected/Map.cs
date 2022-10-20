using System;
using System.Collections.Generic;
using System.Text;

namespace Gade6122_Part1_corrected
{
    [Serializable]
    public class Map
    {
        //initiating all variables 
        private Tile[,] map;
        private Hero hero;
        private Enemy[] enemies;
        public Item[] items;
        private int width;
        private int height;
        [NonSerialized] private Random random;
        public Hero Hero 
        { 
            get { return hero; }
        }
        public Enemy[] Enemy
        {
            get { return enemies; }
        }
        public Map(int minWidth, int maxWidth, int minHeight, int maxHeight, int numEnemies, int goldAmount) //creates map with min and max size and amount of enemies and gold 
        {
            random = new Random(); //random variable used to randomise the map size

            width = random.Next(minWidth, maxWidth);
            height = random.Next(minHeight, maxHeight);
            map = new Tile[width, height];
            InitialiseMap();
            enemies = new Enemy[numEnemies]; //enemy array that holds a number of enemies
            items = new Item[goldAmount]; //gold array that holds an amount of gold

            hero = (Hero)Create(TileType.Hero);
            for (int i = 0; i < enemies.Length; i++)
            {
              enemies[i] = (Enemy)Create(TileType.Enemy);           
            }
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = (Item)Create(TileType.Gold);
            }
            UpdateVision();
         }

        private void UpdateVision() //method that refreshesh the vision arrays of all of the characters on the map
        {
            hero.UpdateVision(map);
            foreach (Enemy enemy in enemies)
            {
                enemy.UpdateVision(map);
            }
        }
        public void UpdateMap() //updates the map and vision arrays of characters for the display
        {
            InitialiseMap();

           
            foreach (Enemy enemy in enemies)
            {
                map[enemy.X, enemy.Y] = enemy;
            }
            foreach (Item item in items)
            {
                if (item != null)
                {
                    map[item.X, item.Y] = item;
                }
            }
            //place hero last so its not overwritten
            if (!hero.IsDead)
            {
                map[hero.X, hero.Y] = hero;
            }
            UpdateVision();
        }

        private Tile Create(TileType type) //method that creates the characters and items where they are located
        {
            int tileX = random.Next(1, width - 1);
            int tileY = random.Next(1, height - 1);

            while(!(map[tileX, tileY] is EmptyTile))
            {
                tileX = random.Next(1, width - 1);
                tileY = random.Next(1, height - 1);
            }
            if(type == TileType.Hero)
            {
                map[tileX, tileY] = new Hero(tileX, tileY, 100);
            }
            if (type == TileType.Gold)
            {
                map[tileX, tileY] = new Gold(tileX, tileY);
            }
            else if (type == TileType.Enemy) //randomises between Swamp Creatures and Mages
            {
                int enemyType = random.Next(2);
                if (enemyType == 0)
                {
                    map[tileX, tileY] = new SwampCreature(tileX, tileY);
                }
                if (enemyType == 1)
                {
                    map[tileX, tileY] = new Mage(tileX, tileY);
                }
            }
            return map[tileX, tileY];
        }
        private void InitialiseMap() //initialises the size of the map
        {
            for(int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                    {
                        map[x, y] = new Obstacle(x, y);

                    }
                    else
                    {
                        map[x, y] = new EmptyTile(x, y);
                    }
                }
            }
        }
        public override string ToString() //Turns the map into a string which can be output in a label
        {
            string s = "";
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++) //loopes through all tiles within the map
                {
                    Tile tile = map[x, y];
                    if(tile is EmptyTile)
                    {
                        s += ".";
                    }
                    else if (tile is Obstacle)
                    {
                        s += "X";
                    }
                    else if (tile is Hero)
                    {
                        s += "H";
                    }
                    else if (tile is SwampCreature)
                    {
                        
                        if (((Enemy)tile).IsDead)
                        {
                            tile = new EmptyTile(tile.X, tile.Y);
                            s += '†';                           
                        }                       
                        else s += "S";
                    }
                    else if (tile is Mage)
                    {
                        if (((Enemy)tile).IsDead)
                        {
                            tile = new EmptyTile(tile.X, tile.Y);
                            s += '†';
                        }
                        else s += "M";
                    }
                    else if (tile is Gold)
                    {
                        s += "G";
                    }

                }
                s += "\n";
            }
            return s;            
        }

        public Item GetItemAtPoisition(int x, int y) //used to get the item located a specific location
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i]!= null && items[i].X == x && items[i].Y == y)
                {
                    Item selectedItem = items[i];
                    items[i] = null;
                    return selectedItem;
                }
            }

            return null;

        }

        public void MoveEnemies() //method that moves enemies in a random direction if possible
        {
            for (int i = 0; i < this.enemies.Length; i++)
            {               
                if (enemies[i] is SwampCreature && enemies[i].IsDead == false) //right now only swampcreatures can move
                {

                    enemies[i].UpdateVision(map);                   
                   
                    Movement direction = (enemies[i] as SwampCreature).ReturnMove(); 
                    Tile tile = enemies[i];
                    if (direction == Movement.NoMovemnt)
                    {
                        return;
                    }
                    if (direction == Movement.Up)
                    {
                        enemies[i].Move((Movement)Movement.Up);
                    }
                    if (direction == Movement.Down)
                    {
                        enemies[i].Move((Movement)Movement.Down);
                    }
                    if (direction == Movement.Left)
                    {
                        enemies[i].Move((Movement)Movement.Left);
                    }
                    if (direction == Movement.Right)
                    {
                       enemies[i].Move((Movement)Movement.Right);
                    }

                    enemies[i].UpdateVision(map);


                }

                
            }
        }
    }
}
