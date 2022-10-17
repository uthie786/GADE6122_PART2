using System;
using System.Collections.Generic;
using System.Text;

namespace Gade6122_Part1_corrected
{
    public class Map
    {
        private Tile[,] map;
        private Hero hero;
        private Enemy[] enemies; 
        public Item[] items;
        private int width;
        private int height;
        private Random random;
        public Hero Hero 
        { 
            get { return hero; }
        }
        public Enemy[] Enemy
        {
            get { return enemies; }
        }
        public Map(int minWidth, int maxWidth, int minHeight, int maxHeight, int numEnemies, int goldAmount)
        {
            random = new Random();

            width = random.Next(minWidth, maxWidth);
            height = random.Next(minHeight, maxHeight);
            map = new Tile[width, height];
            InitialiseMap();
            enemies = new Enemy[numEnemies];
            items = new Item[goldAmount];

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

        private void UpdateVision()
        {
            hero.UpdateVision(map);
            foreach (Enemy enemy in enemies)
            {
                enemy.UpdateVision(map);
            }
        }
        public void UpdateMap()
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
            map[hero.X, hero.Y] = hero;
            UpdateVision();
        }

        private Tile Create(TileType type) //creates all charcaters on the map 
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
        private void InitialiseMap()
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
        public override string ToString()
        {
            string s = "";
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
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
                            s += '†';                           
                        }                       
                        else s += "S";
                    }
                    else if (tile is Mage)
                    {
                        if (((Enemy)tile).IsDead)
                        {
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

        public Item GetItemAtPoisition(int x, int y)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != null && items[i].X == x && items[i].Y == y)
                {
                    Item selectedItem = items[i];
                    items[i] = null;
                    return selectedItem;
                }
            }

            return null;

        }

        public void MoveEnemies()
        {
            for (int i = 0; i < this.enemies.Length; i++)
            {               
                if (enemies[i] is SwampCreature && enemies[i].IsDead == false)
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


                   

                }

                
            }
        }
    }
}
