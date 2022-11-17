using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleApplication1
{    public class Player : Statistic
    {
        private const char Sprite = '@';
        public ConsoleColor Color;

        public Player() : base(10,10,0,1,0,5,5)
        {
            
        }
        //Statistic player1 = new Statistic
        

        public Position CurrentPosition = new Position(1, 1);
        public Position OldPosition = new Position(1, 1);
        public Position futurPosition = new Position(1, 1);
        
        public void PlacePlayer(Map map)
        {
            var isPlaced = false;
            var rnd = new Random();
            while (!isPlaced)
            {
                var randomX = rnd.Next(1, map.Width -1);
                var randomY = rnd.Next(1,map.Height-1);



                if (map.Tiles[randomX,randomY].isPassable)
                {
                    CurrentPosition.X = randomX;
                    CurrentPosition.Y = randomY;
                    OldPosition.X = randomX;
                    OldPosition.Y = randomY;
                    isPlaced = true;
                }
            }
        }
        public void MovePlayer(GameManager gameManager, Tile[,] tiles, List<Enemy> enemies)
        {
            var keyPressed = Console.ReadKey();
            futurPosition = new Position(CurrentPosition.Y, CurrentPosition.X);
            OldPosition.X = CurrentPosition.X;
            OldPosition.Y = CurrentPosition.Y;
            switch (keyPressed.Key)
            {
                case ConsoleKey.LeftArrow:
                    futurPosition.X -= 1;
                    if(enemies.Exists(e =>futurPosition.X == e.CurrentPosition.X && futurPosition.Y == e.CurrentPosition.Y ))
                    {
                        var enemy = enemies.Find(e =>futurPosition.X == e.CurrentPosition.X && futurPosition.Y == e.CurrentPosition.Y);
                        enemy.DecreaseHp(Damage(enemy.defense));
                    }
                    else if(tiles[futurPosition.X,futurPosition.Y].isPassable)
                    {
                        CurrentPosition.X -= 1;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    futurPosition.X += 1;
                    if(enemies.Exists(e =>futurPosition.X == e.CurrentPosition.X && futurPosition.Y == e.CurrentPosition.Y ))
                    {
                        var enemy = enemies.Find(e =>futurPosition.X == e.CurrentPosition.X && futurPosition.Y == e.CurrentPosition.Y);
                        enemy.DecreaseHp(Damage(enemy.defense));
                    }
                    else if(tiles[futurPosition.X,futurPosition.Y].isPassable)
                    {
                        CurrentPosition.X += 1;
                        
                    }
                    break;
                case ConsoleKey.UpArrow:
                    futurPosition.Y -= 1;
                    if(enemies.Exists(e =>futurPosition.X == e.CurrentPosition.X && futurPosition.Y == e.CurrentPosition.Y ))
                    {
                        var enemy = enemies.Find(e =>futurPosition.X == e.CurrentPosition.X && futurPosition.Y == e.CurrentPosition.Y);
                        enemy.DecreaseHp(Damage(enemy.defense));
                    }
                    else if(tiles[futurPosition.X,futurPosition.Y].isPassable)
                    {
                        CurrentPosition.Y -= 1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    futurPosition.Y += 1;
                    if(enemies.Exists(e =>futurPosition.X == e.CurrentPosition.X && futurPosition.Y == e.CurrentPosition.Y ))
                    {
                        var enemy = enemies.Find(e =>futurPosition.X == e.CurrentPosition.X && futurPosition.Y == e.CurrentPosition.Y);
                        enemy.DecreaseHp(Damage(enemy.defense));
                    }
                    else if(tiles[futurPosition.X,futurPosition.Y].isPassable)
                    {
                        CurrentPosition.Y += 1;
                    }
                    break;
                
                case ConsoleKey.K:
                    //Process.GetCurrentProcess().Kill();
                    gameManager.isReset = true;
                    gameManager.isTerminate = true;
                    break;
                
                case ConsoleKey.R:
                    gameManager.isReset = true;
                    break;
                case ConsoleKey.H:
                    IncreaseHp(10);
                    break; 
            }
            futurPosition.X = CurrentPosition.X;
            futurPosition.Y = CurrentPosition.Y;
        }
        public void DisplayPlayer(Tile[,] array)
        {
            //array[OldPosition.X, OldPosition.Y].Sprite = '.';
            //array[OldPosition.X, OldPosition.Y].Color = ConsoleColor.DarkGray;
            
            array[OldPosition.X, OldPosition.Y] = new Tile(position: new Position(OldPosition.X, OldPosition.Y),type:TileType.Ground);
            array[CurrentPosition.X, CurrentPosition.Y].Color = ConsoleColor.Green;
            array[CurrentPosition.X, CurrentPosition.Y].Sprite = Sprite;
        }
        

        public void ShowPerso()
        {
            if (hp > 0)
            {
                
                Console.ForegroundColor = Color;
                Console.Write(Sprite);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write( " ->hp: " + hp +"/" + maxHp);
            
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write( "▐ "  +"exp: " + xp + "/" + targetXp + " ▐ "+ "lvl: "+ lvl +" ▐ ");
            
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("attack " + attack +" / defense: " +defense + "\n");   
            }
            else
            {
                Console.WriteLine("DEAD!! GAME OVER !! ");
                GameManager gameManager = new GameManager();
                gameManager.isTerminate = true;
                
            }

        }
        

    }
}