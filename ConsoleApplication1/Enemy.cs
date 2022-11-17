using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleApplication1
{
    
    
    
    public class Enemy : Statistic
    {
        public string[] EnemyTypes = new string[]
        {
            "Bats",
            "Vampire",
            "Ogre"
        };
        
        private char Sprite = '.'; 
        private string Name = ""; 
        public ConsoleColor Color;

        public Enemy() : base(100,100,0,1,0,5,5)
        {
            
        }
        
        public Position CurrentPosition = new Position(1, 1);
        public Position OldPosition = new Position(1, 1);
        
        public void PlaceEnemy(Map map)
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
        public void MoveEnemy(GameManager gameManager, Tile[,] tiles,Player player)
        {
            if (!EnemyDeath())
            {
                var futurePosition = new Position(CurrentPosition.Y, CurrentPosition.X);
                OldPosition.X = CurrentPosition.X;
                OldPosition.Y = CurrentPosition.Y;

                var xDir = 0;
                var yDir = 0;

                if (Math.Abs (player.CurrentPosition.X - CurrentPosition.X) < float.Epsilon) yDir = player.CurrentPosition.Y > CurrentPosition.Y ? 1 : -1;
                else xDir = player.CurrentPosition.X > CurrentPosition.X ? 1 : -1;

                futurePosition.X += xDir;
                futurePosition.Y += yDir;

                if(player.futurPosition.X == futurePosition.X && player.futurPosition.Y == futurePosition.Y )
                {
                    //var mc = player.CurrentPosition.X == CurrentPosition.X && player.CurrentPosition.Y == CurrentPosition.Y;
                    player.DecreaseHp(Damage(player.defense));
                }
                else if(tiles[futurePosition.X,futurePosition.Y].isPassable)
                {
                    CurrentPosition.X = futurePosition.X;
                    CurrentPosition.Y = futurePosition.Y;
                }
            }
        }
        public void DisplayEnemy(Tile[,] array)
        {
            if (!EnemyDeath())
            {
                array[OldPosition.X, OldPosition.Y] = new Tile(position: new Position(OldPosition.X, OldPosition.Y),type:TileType.Ground);
                array[CurrentPosition.X, CurrentPosition.Y].Color = Color;
                array[CurrentPosition.X, CurrentPosition.Y].Sprite = Sprite;
            }
            else
            {
                array[CurrentPosition.X, CurrentPosition.Y] = new Tile(position: new Position(CurrentPosition.X, CurrentPosition.Y),type:TileType.Ground);
                //array[CurrentPosition.X, CurrentPosition.Y].Color = ConsoleColor.DarkRed;
                array[OldPosition.X, OldPosition.Y].Sprite = 'O';
                array[CurrentPosition.X, CurrentPosition.Y].Sprite = 'C';

                array[OldPosition.X, OldPosition.Y].isPassable = true;
                array[CurrentPosition.X, CurrentPosition.Y].isPassable = true;
            }
        }
        public void ShowEnemy(Tile[,] array)
        {
            if (!EnemyDeath())
            {
                
                Console.ForegroundColor = Color;
                Console.Write(Sprite + " " + Name);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write( " ->hp: " + hp +"/" + maxHp);
            
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write( "▐ "  +"exp: " + targetXp + " ▐ "+ "lvl: "+ lvl +" ▐ ");
            
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("attack " + attack +" / defense: " +defense + "\n");   
            }
        }
        public bool EnemyDeath()
        {
            if (hp>0)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        } 

        public Enemy(int index)
        {
            //Enemy = enemy;
            switch (index)
            {
                case 0:
                    Sprite = 'v';
                    Name = EnemyTypes[index];
                    Color = ConsoleColor.White;
                    hp = 5;
                    maxHp = 5;
                    defense = 1;
                    xp = 1;
                    break;
                case 1:
                    Sprite = 'ᚖ';
                    Name = EnemyTypes[index];
                    Color = ConsoleColor.DarkMagenta;
                    hp = 10;
                    maxHp = 10;
                    xp = 5;
                    attack = 7;
                    defense = 3;
                    break;
                case 2:
                    Sprite = 'ᚗ';
                    Name = EnemyTypes[index];
                    hp = 7;
                    xp = 10;
                    maxHp = 7;
                    attack = 5;
                    defense = 7;
                    Color = ConsoleColor.DarkGreen;
                    break;
            }
        }


        

    }
}