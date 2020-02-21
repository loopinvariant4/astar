using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace astar
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new Stopwatch();
            var tot = new Stopwatch();
            tot.Start();
            Grid g = new Grid(64, 64);
            g.SetStartPoint(0, 0);
            g.SetEndPoint(58, 60);


            g.grid[5, 8].Passable = false;
            g.grid[1, 8].Passable = false;
            g.grid[2, 8].Passable = false;
            g.grid[3, 8].Passable = false;
            g.grid[4, 8].Passable = false;
            g.grid[6, 8].Passable = false;
            g.grid[7, 8].Passable = false;
            g.grid[8, 8].Passable = false;
            g.grid[9, 8].Passable = false;

            g.grid[1, 0].Penalty = 1;
            g.grid[2, 0].Penalty = 1;
            g.grid[3, 1].Penalty = 1;
            g.grid[3, 2].Penalty = 1;
            g.grid[3, 3].Penalty = 1;
            g.grid[3, 4].Penalty = 1;
            g.grid[3, 5].Penalty = 1;
            g.grid[3, 6].Penalty = 1;

            s.Start();
            for (int i = 0; i < 1000; i++)
            {
                var res = findPathOpt(g);
            }
            s.Stop();
            tot.Stop();
            /*
            if (res == null)
            {
                Console.WriteLine("Error! could not find path");
            }
            else
            {
                markPath(res, g);
                g.Render();
            }
            */

            Console.WriteLine("Time taken: " + s.ElapsedMilliseconds);
            Console.WriteLine("Total Time taken: " + tot.ElapsedMilliseconds);
        }

        private static void markPath(Dictionary<Tile, Tile> res, Grid g)
        {
            var next = res[g.EndPoint];
            while (next != g.StartPoint)
            {
                //Console.WriteLine(next);
                next.TileType = 'O';
                next = res[next];
            }
        }
        static Dictionary<Tile, Tile> findPathOpt(Grid g)
        {
            MinHeap<Tile> openSet = new MinHeap<Tile>(4096);

            Dictionary<Tile, Tile> cameFrom = new Dictionary<Tile, Tile>();

            Dictionary<Tile, double> gScore = new Dictionary<Tile, double>();
            gScore.Add(g.StartPoint, 0);

            Dictionary<Tile, double> fScore = new Dictionary<Tile, double>();
            fScore.Add(g.StartPoint, computeH(g.StartPoint, g));
            openSet.Add(g.StartPoint, fScore[g.StartPoint]);

            while (openSet.Count > 0)
            {
                var current = openSet.Remove();

                /*
                if (current == g.EndPoint)
                {
                    return cameFrom;
                }
                */

                foreach (var next in g.getNeighbours(current))
                {
                    var tentativeGScore = gScore[current] + costToNeighbour(current, next);
                    if (!gScore.ContainsKey(next))
                    {
                        gScore.Add(next, double.PositiveInfinity);
                    }
                    if (tentativeGScore < gScore[next])
                    {
                        //Console.WriteLine(current + ": " + tentativeGScore);
                        cameFrom[next] = current;
                        gScore[next] = tentativeGScore;
                        fScore[next] = gScore[next] + computeH(next, g);
                        if (!openSet.Contains(next))
                        {
                            openSet.Add(next, fScore[next]);
                        }
                    }
                }
            }
            return cameFrom;
        }

        static Dictionary<Tile, Tile> findPath(Grid g)
        {
            List<Tile> openSet = new List<Tile>();
            openSet.Add(g.StartPoint);

            Dictionary<Tile, Tile> cameFrom = new Dictionary<Tile, Tile>();

            Dictionary<Tile, double> gScore = new Dictionary<Tile, double>();
            gScore.Add(g.StartPoint, 0);

            Dictionary<Tile, double> fScore = new Dictionary<Tile, double>();
            fScore.Add(g.StartPoint, computeH(g.StartPoint, g));


            while (openSet.Count > 0)
            {
                var current = getTileWithLowestF(openSet, fScore);

                /*
                if (current == g.EndPoint)
                {
                    return cameFrom;
                }
                */


                openSet.Remove(current);
                foreach (var next in g.getNeighbours(current))
                {
                    var tentativeGScore = gScore[current] + costToNeighbour(current, next);
                    if (!gScore.ContainsKey(next))
                    {
                        gScore.Add(next, double.PositiveInfinity);
                    }
                    if (tentativeGScore < gScore[next])
                    {
                        //Console.WriteLine(current + ": " + tentativeGScore);
                        cameFrom[next] = current;
                        gScore[next] = tentativeGScore;
                        fScore[next] = gScore[next] + computeH(next, g);
                        if (!openSet.Contains(next))
                        {
                            openSet.Add(next);
                        }
                    }
                }
            }
            return cameFrom;
        }

        private static double costToNeighbour(Tile current, Tile next)
        {
            return current.Penalty;
        }

        private static Tile getTileWithLowestF(List<Tile> openSet, Dictionary<Tile, double> fScore)
        {
            double score = double.PositiveInfinity;
            Tile res = null;
            foreach (var t in openSet)
            {
                if (fScore[t] < score)
                {
                    res = t;
                    score = fScore[t];
                }
            }
            return res;
        }

        static double computeH(Tile t, Grid g)
        {
            return Math.Sqrt(Math.Pow(g.EndPoint.Row - t.Row, 2) + Math.Pow(g.EndPoint.Col - t.Col, 2));
        }
    }
}
