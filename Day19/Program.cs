using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day19
{
    internal enum Direction
    {
        Left, Right, Up, Down
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            new Program().SolvePart1();
            Console.ReadLine();
        }

        private Program()
        {

        }

        private void SolvePart1()
        {
            var input = LoadInput();
            var grid = new char[input.Length,input[0].Length];
            var pos = (0, 0);
            var dir = Direction.Down;

            Console.WriteLine($"Dimensions: {grid.GetLength(1)}x{grid.GetLength(0)}");

            var y = 0;
            foreach (var line in input)
            {
                var x = 0;
                foreach (var c in line)
                {
                    grid[y, x] = c;

                    if (y == 0)
                        if (c == '|')
                            pos = (x, y);

                    x += 1;
                }
                y += 1;
            }

            var letters = new List<char>();
            var steps = 0;
            while (true)
            {
                //Console.Write($"Going {dir} from {pos} ({grid[pos.Item2, pos.Item1]})");

                switch (dir)
                {
                    case Direction.Down:
                        pos = (pos.Item1, pos.Item2 + 1);
                        break;

                    case Direction.Up:
                        pos = (pos.Item1, pos.Item2 - 1);
                        break;

                    case Direction.Left:
                        pos = (pos.Item1 - 1, pos.Item2);
                        break;

                    case Direction.Right:
                        pos = (pos.Item1 + 1, pos.Item2);
                        break;
                }

                var c = grid[pos.Item2, pos.Item1];

                //Console.WriteLine($" -> new pos {pos} ({c})");

                //for (y = 0; y < grid.GetLength(0); y++)
                //{
                //    for (var x = 0; x < grid.GetLength(1); x++)
                //    {
                //        if (x == pos.Item1 && y == pos.Item2)
                //            Console.Write('#');
                //        else
                //            Console.Write(grid[y, x]);
                //    }
                //    Console.WriteLine();
                //}

                if (char.IsLetter(c))
                    letters.Add(c);
                else
                {
                    if (char.IsWhiteSpace(c))
                        break;
                }

                steps += 1;

                if (c != '+')
                    continue;

                if (dir == Direction.Down || dir == Direction.Up)
                {
                    if (!char.IsWhiteSpace(grid[pos.Item2, pos.Item1 - 1]))
                        dir = Direction.Left;
                    else if (!char.IsWhiteSpace(grid[pos.Item2, pos.Item1 + 1]))
                        dir = Direction.Right;
                }
                else
                {
                    if (!char.IsWhiteSpace(grid[pos.Item2 - 1, pos.Item1]))
                        dir = Direction.Up;
                    else if (!char.IsWhiteSpace(grid[pos.Item2 + 1, pos.Item1]))
                        dir = Direction.Down;
                }
            }

            Console.WriteLine($"Letters: {letters.Select(c => c.ToString()).Aggregate((s1, s2) => s1.ToString() + s2.ToString())}, steps: {steps}");
        }

        private string[] LoadInput() => File.ReadAllLines("input.txt");

        private string[] SampleInput1() => new[] {
            "     |          ",
            "     |  +--+    ",
            "     A  |  C    ",
            " F---|----E|--+ ",
            "     |  |  |  D ",
            "     +B-+  +--+ "
        };
    }
}
