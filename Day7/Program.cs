using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day7
{
    internal class PuzzleProgram : IEquatable<PuzzleProgram>
    {
        public string Name { get; }
        public int Weight { get; set; }
        public List<PuzzleProgram> Children { get; }
        public PuzzleProgram Parent { get; set; }

        public PuzzleProgram(string name)
        {
            Name = name;
            Children = new List<PuzzleProgram>();
        }

        public bool Equals(PuzzleProgram other)
        {
            if (ReferenceEquals(null, other))
                return false;
            return ReferenceEquals(this, other) || string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            return obj.GetType() == GetType() && Equals((PuzzleProgram)obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString() => $"<{Name}, {Weight}>";

        public int TowerWeight() => Weight + Children.Sum(c => c.TowerWeight());
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
            var staging = new Dictionary<string, PuzzleProgram>();
            PuzzleProgram root = null;

            var lineNum = 0;
            foreach (var line in input)
            {
                lineNum += 1;

                var args = line.Split(' ');
                var name = args[0];
                var weight = int.Parse(args[1].Trim('(', ')'));
                PuzzleProgram program;

                if (!staging.ContainsKey(name))
                {
                    program = new PuzzleProgram(name) { Weight = weight };
                    staging.Add(name, program);

                    if (root == null)
                        root = program;
                }
                else
                {
                    program = staging[name];
                    program.Weight = weight;
                }

                if (args.Length <= 2)
                    continue;

                var children = args.Skip(3).Select(s => s.Trim(','));
                foreach (var child in children)
                {
                    PuzzleProgram childProg;
                    if (!staging.ContainsKey(child))
                    {
                        childProg = new PuzzleProgram(child);
                        staging.Add(child, childProg);
                    }
                    else
                        childProg = staging[child];

                    program.Children.Add(childProg);
                    childProg.Parent = program;

                    if (!root.Equals(childProg))
                        continue;

                    root = program;

                    while (root.Parent != null)
                        root = root.Parent;

                    Console.WriteLine($"{lineNum}: Root assigned from {childProg} to {root}");
                }
            }

            Console.WriteLine($"Root: {root}. Total lines: {lineNum}");
        }

        private void SolvePart2()
        {
            var input = LoadInput();
            var staging = new Dictionary<string, PuzzleProgram>();
            PuzzleProgram root = null;

            foreach (var line in input)
            {
                var args = line.Split(' ');
                var name = args[0];
                var weight = int.Parse(args[1].Trim('(', ')'));
                PuzzleProgram program;

                if (!staging.ContainsKey(name))
                {
                    program = new PuzzleProgram(name) { Weight = weight };
                    staging.Add(name, program);

                    if (root == null)
                        root = program;
                }
                else
                {
                    program = staging[name];
                    program.Weight = weight;
                }

                if (args.Length <= 2)
                    continue;

                var children = args.Skip(3).Select(s => s.Trim(','));
                foreach (var child in children)
                {
                    PuzzleProgram childProg;
                    if (!staging.ContainsKey(child))
                    {
                        childProg = new PuzzleProgram(child);
                        staging.Add(child, childProg);
                    }
                    else
                        childProg = staging[child];

                    program.Children.Add(childProg);
                    childProg.Parent = program;

                    if (!root.Equals(childProg))
                        continue;

                    root = program;

                    while (root.Parent != null)
                        root = root.Parent;
                }
            }

            PrintTower(root, 0);
        }

        private void PrintTower(PuzzleProgram program, int depth)
        {
            for (var i = 0; i < depth; i++)
                Console.Write("   ");

            Console.WriteLine($"-> {program.Weight} ({program.TowerWeight()})");
            foreach (var child in program.Children)
                PrintTower(child, depth + 1);
        }

        private IEnumerable<string> LoadInput() => File.ReadLines("input.txt");

        private string[] SampleInput1() => new[] {
            "pbga (66)",
            "xhth (57)",
            "ebii (61)",
            "havc (66)",
            "ktlj (57)",
            "fwft (72) -> ktlj, cntj, xhth",
            "qoyq (66)",
            "padx (45) -> pbga, havc, qoyq",
            "tknk (41) -> ugml, padx, fwft",
            "jptl (61)",
            "ugml (68) -> gyxo, ebii, jptl",
            "gyxo (61)",
            "cntj (57)",
        };
    }
}
