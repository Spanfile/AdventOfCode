using System;
using System.Collections.Generic;
using System.IO;

namespace Day22
{
    internal enum Direction
    {
        Left, Up, Right, Down
    }

    internal enum State
    {
        Clean, Weakened, Infected, Flagged
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            new Program().SolvePart2();
            Console.ReadLine();
        }

        private Program()
        {

        }

        private void SolvePart1()
        {
            var input = LoadInput();
            var grid = new Dictionary<(int, int), bool>();

            var y = -1;
            foreach (var line in input.Split('\n'))
            {
                var x = 0;
                y += 1;
                foreach (var c in line)
                    grid.Add((x++, y), c == '#');
            }

            var pos = (y / 2, y / 2);
            Console.WriteLine($"Starting position: {pos} ({y})");
            var dir = Direction.Up;
            var infections = 0;

            for (var i = 0; i < 10000; i++)
            {
                int newDir;
                if (grid[pos])
                    newDir = ((int)dir + 1) % 4;
                else
                    newDir = ((int)dir + 3) % 4;

                dir = (Direction)newDir;
                grid[pos] = !grid[pos];

                if (grid[pos])
                    infections += 1;

                switch (dir)
                {
                    case Direction.Up:
                        pos = (pos.Item1, pos.Item2 - 1);
                        break;

                    case Direction.Down:
                        pos = (pos.Item1, pos.Item2 + 1);
                        break;

                    case Direction.Left:
                        pos = (pos.Item1 - 1, pos.Item2);
                        break;

                    case Direction.Right:
                        pos = (pos.Item1 + 1, pos.Item2);
                        break;
                }

                if (!grid.ContainsKey(pos))
                    grid.Add(pos, false);
            }

            Console.WriteLine($"Infections: {infections}");
        }

        private void SolvePart2()
        {
            var input = LoadInput();
            var grid = new Dictionary<(int, int), State>();

            var y = -1;
            foreach (var line in input.Split('\n'))
            {
                var x = 0;
                y += 1;
                foreach (var c in line)
                    grid.Add((x++, y), c == '#' ? State.Infected : State.Clean);
            }

            var pos = (y / 2, y / 2);
            var dir = Direction.Up;
            var infections = 0;

            for (var i = 0; i < 10000000; i++)
            {
                var newDir = (int)dir;
                switch (grid[pos])
                {
                    case State.Infected:
                        newDir = ((int)dir + 1) % 4;
                        break;
                    case State.Clean:
                        newDir = ((int)dir + 3) % 4;
                        break;
                    case State.Flagged:
                        newDir = ((int)dir + 2) % 4;
                        break;
                }

                dir = (Direction)newDir;
                grid[pos] = (State)(((int)grid[pos] + 1) % 4);

                if (grid[pos] == State.Infected)
                    infections += 1;

                switch (dir)
                {
                    case Direction.Up:
                        pos = (pos.Item1, pos.Item2 - 1);
                        break;

                    case Direction.Down:
                        pos = (pos.Item1, pos.Item2 + 1);
                        break;

                    case Direction.Left:
                        pos = (pos.Item1 - 1, pos.Item2);
                        break;

                    case Direction.Right:
                        pos = (pos.Item1 + 1, pos.Item2);
                        break;
                }

                if (!grid.ContainsKey(pos))
                    grid.Add(pos, State.Clean);
            }

            Console.WriteLine($"Infections: {infections}");
        }

        private string LoadInput() => File.ReadAllText("input.txt");

        private string SampleInput1() => @"
..#
#..
...";
    }
}
