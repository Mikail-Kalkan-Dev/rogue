using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    internal class Program : Player
    {

        public static void Main(string[] args)
        {
            Player player = new Player();
            //Enemy enemy = new Enemy();
            List<Enemy> enemies = new List<Enemy>();
            Map map = new Map();
            Random rnd = new Random();
            GameManager gameManager;

            gameManager = new GameManager();
            
            while (!gameManager.isTerminate)
            {
                Console.Clear();
                gameManager = new GameManager();
                player = new Player();

                for (int i = 0; i < new Random().Next(1,5); i++)
                {
                    
                    enemies.Add(new Enemy(rnd.Next(0,3)));    
                }

                map = new Map();
                
                map.CreateMap();
                map.CreateWall(map.wallNumber());
                
                player.PlacePlayer(map);
                foreach (var e in enemies)
                {
                    e.PlaceEnemy(map);
                }

                while (!gameManager.isReset)
                {
                    Console.Clear();
                    player.DisplayPlayer(map.Tiles);
                    
                    foreach (var e in enemies)
                    {
                        e.DisplayEnemy(map.Tiles);
                        e.ShowEnemy(map.Tiles);
                    }
                    
                    map.DisplayMap();
                    
                    player.ShowPerso();
                    
                    player.MovePlayer(gameManager, map.Tiles,enemies);
                    
                    foreach (var e in enemies)
                    {
                        e.ShowEnemy(map.Tiles);
                        e.MoveEnemy(gameManager,map.Tiles,player);
                    }

                }
            }
        }
    }
}