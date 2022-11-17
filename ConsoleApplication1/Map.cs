using System;

namespace ConsoleApplication1
{
    public class Map
    {
        private Random rnd = new Random();
        
        
        public int Width;
        public int Height;
        public Tile[,] Tiles;
        public char[,] array;
        private int minPercent = 8;
        private int maxPercent = 16;

        public Map()
        {
            Width = rnd.Next(30,61);
            Height = rnd.Next(6,9);
            Tiles = new Tile[Width, Height];
        }
        
        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            Tiles = new Tile[Width, Height];
        }

        public Boolean isBorder(int x, int y)
        {
            return x == 0 
                   || 
                   y == 0 
                   || 
                   x == Width - 1 
                   || 
                   y == Height -1;
        }
        
        public int wallNumber()
        {
            
            var totalTile = ((Width - 2) * (Height - 2));

            var min = totalTile * minPercent / 100;
            var max = totalTile * maxPercent / 100;
            return rnd.Next(min,max+1) ;
        }

        public void CreateWall(int number)
        {
            while (number > 0)
            {
                var randomX = rnd.Next(1,Width);
                var randomY = rnd.Next(1,Height);

                if (Tiles[randomX,randomY].isPassable)
                {
                    Tiles[randomX, randomY] = new Tile(
                        position: new Position(randomX, randomY),
                        type: TileType.Wall);
                    number--;
                }
            }
        }

        public void CreateMap()
        {    for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Tiles[x, y] = isBorder(x, y)
                            ? new Tile(
                                position: new Position(x, y),
                                type: TileType.Wall)
                        :
                        new Tile(
                            position: new Position(x, y),
                            type: TileType.Ground
                            );
                }
            }
        }

        
        
        public void DisplayMap()
        {    for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Console.ForegroundColor = Tiles[x, y].Color;
                    Console.Write(Tiles[x,y].Sprite);
                }
                Console.WriteLine();
            }
        }


    }
}