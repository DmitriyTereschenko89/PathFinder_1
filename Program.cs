using NUnit.Framework;
using System.Text.RegularExpressions;

namespace PathFinder_1
{
    internal class Program
    {
        public class Finder
        {
            private static List<(int, int)> GetNeighbors(string[] grid, int row, int col)
            {
                List<(int, int)> neighbors = new();
                if (row > 0 && grid[row - 1][col] != 'W')
                {
                    neighbors.Add((row - 1, col));
                }
                if (row < grid.Length - 1 && grid[row + 1][col] != 'W')
                {
                    neighbors.Add((row + 1, col));
                }
                if (col > 0 && grid[row][col - 1] != 'W')
                {
                    neighbors.Add((row, col - 1));
                }
                if (col < grid.Length - 1 && grid[row][col + 1] != 'W')
                {
                    neighbors.Add((row, col + 1));
                }
                return neighbors;
            }

            public static bool PathFinder(string maze)
            {
                string[] grid = Regex.Split(maze, @"(?:\r\n)|(?:\r)|(?:\n)");
                bool[,] visited = new bool[grid.Length, grid.Length];
                Queue<(int, int)> queue = new();
                queue.Enqueue((0, 0));
                while (queue.Count > 0)
                {
                    (int, int) cell = queue.Dequeue();
                    if (cell.Item1 == grid.Length - 1 && cell.Item2 == grid.Length - 1)
                    {
                        return true;
                    }
                    if (visited[cell.Item1, cell.Item2])
                    {
                        continue;
                    }
                    visited[cell.Item1, cell.Item2] = true;
                    List<(int, int)> neighbors = GetNeighbors(grid, cell.Item1, cell.Item2);
                    foreach ((int, int) neighbor in neighbors)
                    {
                        queue.Enqueue(neighbor);
                    }
                }
                return false;
            }
        }
        static void Main(string[] args)
        {
            string a = ".W.\n" +
                   ".W.\n" +
                   "...",

               b = ".W.\n" +
                   ".W.\n" +
                   "W..",

               c = "......\n" +
                   "......\n" +
                   "......\n" +
                   "......\n" +
                   "......\n" +
                   "......",

               d = "......\n" +
                   "......\n" +
                   "......\n" +
                   "......\n" +
                   ".....W\n" +
                   "....W.";

            Assert.AreEqual(true, Finder.PathFinder(a));
            Assert.AreEqual(false, Finder.PathFinder(b));
            Assert.AreEqual(true, Finder.PathFinder(c));
            Assert.AreEqual(false, Finder.PathFinder(d));
        }
    }
}