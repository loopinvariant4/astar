using System;
using System.Collections.Generic;
using System.Text;

namespace astar
{
    class Grid
    {
        public Tile[,] grid;
        public Tile StartPoint { get; set; }
        public Tile EndPoint { get; set; }
        public Grid(int rows, int cols)
        {
            grid = new Tile[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    grid[i, j] = new Tile(i, j);
                }
            }
        }

        public void SetStartPoint(int i, int j)
        {
            grid[i, j].TileType = 'S';
            StartPoint = grid[i, j];
        }

        public void SetEndPoint(int i, int j)
        {
            grid[i, j].TileType = 'E';
            EndPoint = grid[i, j];
        }

        public void Render()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Console.Write(grid[i, j].TileType + " ");
                }
                Console.WriteLine("");
            }
        }

        internal IEnumerable<Tile> getNeighbours(Tile current)
        {
            List<Tile> n = new List<Tile>();
            if (current.Row > 0 && grid[current.Row - 1, current.Col].Passable) n.Add(grid[current.Row - 1, current.Col]);

            if (current.Row < grid.GetLength(0) - 1 && grid[current.Row + 1, current.Col].Passable) n.Add(grid[current.Row + 1, current.Col]);

            if (current.Col > 0 && grid[current.Row, current.Col - 1].Passable) n.Add(grid[current.Row, current.Col - 1]);

            if (current.Col < grid.GetLength(1) - 1 && grid[current.Row, current.Col + 1].Passable) n.Add(grid[current.Row, current.Col + 1]);

            return n; 
        }
    }
}
