using System;
using System.Collections.Generic;
using System.Text;

namespace astar
{
    class Tile
    {
        private bool passable;
        public int Row { get; set; }
        public int Col { get; set; }
        public bool Passable
        {
            get
            {
                return passable;
            }
            set
            {
                passable = value;
                if (passable)
                {
                    TileType = '.';
                }
                else
                {
                    TileType = 'X';
                }
            }
        }

        public double Penalty { get; set; }

        public char TileType { get; set; }

        public Tile(int row, int col, bool passable = true, double penalty = 2.0, char tileType = '.')
        {
            Row = row;
            Col = col;
            Passable = passable;
            Penalty = penalty;
            TileType = tileType;
        }

        public override string ToString()
        {
            return Row + "," + Col;
        }
    }
}
