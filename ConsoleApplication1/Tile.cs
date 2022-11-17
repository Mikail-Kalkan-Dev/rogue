using System;

namespace ConsoleApplication1

{
public enum TileType
{
    Ground,
    Wall,
    Wind,
    Water
}
public class Tile
    {
        public Position Position = new Position(0,0);
        public char Sprite = '@';
        public Boolean isPassable;
        public ConsoleColor Color;

        public Tile(Position position,char sprite, bool ispassable, ConsoleColor color)
        {
            Position = position;
            Sprite = sprite;
            isPassable = ispassable;
            Color = color;

        }

        public Tile(Position position, TileType type)
        {
            Position = position;
            switch (type)
            {
                case TileType.Wall:
                    Sprite = '⬛';
                    isPassable = false;
                    Color = ConsoleColor.DarkBlue;
                    break;
                case TileType.Ground:
                    Sprite = '.';
                    isPassable = true;
                    Color = ConsoleColor.DarkGray;
                    break;
                case TileType.Wind:
                    Sprite = '༄';
                    isPassable = true;
                    Color = ConsoleColor.White;
                    break;
                case TileType.Water:
                    Sprite = 'ᚏ';
                    isPassable = true;
                    Color = ConsoleColor.Blue;
                    break;
            }
        }
    }
}