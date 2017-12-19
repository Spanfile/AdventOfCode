using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

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
            var input = SampleInput1();
            var grid = new char[input[0].Length,input.Length];
            var pos = (0, 0);
            var dir = Direction.Down;

            var y = 0;
            foreach (var line in input)
            {
                var x = 0;
                foreach (var c in line)
                {
                    grid[y, x] = c;
                    Console.Write(c);

                    if (y != 0)
                        continue;
                    if (c == '|')
                        pos = (x, y);

                    x += 1;
                }
                Console.WriteLine();
                y += 1;
            }

            var letters = new List<char>();
            while (true)
            {
                Console.Write($"On ({pos.Item1},{pos.Item2}) ({grid[pos.Item2, pos.Item1]}) going {dir}");

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

                Console.WriteLine($" -> new pos ({pos.Item1},{pos.Item2}) ({c})");

                if (char.IsLetter(c))
                    letters.Add(c);
                else
                {
                    if (c != '-' || c != '|' || c != '+')
                        break;
                }

                if (c != '+')
                    continue;

                if (dir == Direction.Down || dir == Direction.Up)
                {
                    if (!char.IsWhiteSpace(grid[pos.Item1 - 1, pos.Item2]))
                        dir = Direction.Left;
                    else if (!char.IsWhiteSpace(grid[pos.Item1 + 1, pos.Item2]))
                        dir = Direction.Right;
                }
                else
                {
                    if (!char.IsWhiteSpace(grid[pos.Item1, pos.Item2 - 1]))
                        dir = Direction.Up;
                    else if (!char.IsWhiteSpace(grid[pos.Item1, pos.Item2 + 1]))
                        dir = Direction.Down;
                }
            }

            Console.WriteLine($"Letters: {letters.Select(c => c.ToString()).Aggregate((s1, s2) => s1.ToString() + s2.ToString())}");
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
