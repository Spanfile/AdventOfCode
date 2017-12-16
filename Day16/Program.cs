using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day16
{
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
            var input = LoadInput().ToUpper().Split(',');
            var programs = "ABCDEFGHIJKLMNOP".Select(c => c.ToString()).ToList();

            string[] args;
            foreach (var move in input)
            {
                switch (move[0])
                {
                    case 'S':
                        var amount = int.Parse(move.Substring(1));
                        var sublist = programs.Skip(programs.Count - amount).Take(amount).ToList();
                        programs.RemoveRange(programs.Count - amount, amount);
                        programs.InsertRange(0, sublist);
                        break;

                    case 'X':
                        args = move.Substring(1).Split('/');
                        var indexA = int.Parse(args[0]);
                        var indexB = int.Parse(args[1]);
                        var temp = programs[indexA];
                        programs[indexA] = programs[indexB];
                        programs[indexB] = temp;
                        break;

                    case 'P':
                        args = move.Substring(1).Split('/');
                        var progAIndex = programs.IndexOf(args[0]);
                        var progBIndex = programs.IndexOf(args[1]);
                        programs[progAIndex] = args[1];
                        programs[progBIndex] = args[0];
                        break;
                }
            }

            Console.WriteLine($"Final: {programs.Aggregate((s1, s2) => s1 + s2)}");
        }

        private void SolvePart2()
        {
            var input = LoadInput().ToUpper().Split(',');
            var programs = "ABCDEFGHIJKLMNOP".Select(c => c.ToString()).ToList();

            string[] args;
            var seen = new List<string>();
            for (var i = 0; i < 1_000_000_000; i++)
            {
                if (seen.Contains(programs.Aggregate((s1, s2) => s1 + s2)))
                {
                    Console.WriteLine(
                        $"Cycle found after {i} iterations. Final: {seen[1_000_000_000 % i]}");
                    return;
                }

                seen.Add(programs.Aggregate((s1, s2) => s1 + s2));

                foreach (var move in input)
                {
                    switch (move[0])
                    {
                        case 'S':
                            var amount = int.Parse(move.Substring(1));
                            var sublist = programs.Skip(programs.Count - amount).Take(amount).ToList();
                            programs.RemoveRange(programs.Count - amount, amount);
                            programs.InsertRange(0, sublist);
                            break;

                        case 'X':
                            args = move.Substring(1).Split('/');
                            var indexA = int.Parse(args[0]);
                            var indexB = int.Parse(args[1]);
                            var temp = programs[indexA];
                            programs[indexA] = programs[indexB];
                            programs[indexB] = temp;
                            break;

                        case 'P':
                            args = move.Substring(1).Split('/');
                            var progAIndex = programs.IndexOf(args[0]);
                            var progBIndex = programs.IndexOf(args[1]);
                            programs[progAIndex] = args[1];
                            programs[progBIndex] = args[0];
                            break;
                    }
                }
            }
        }

        private string LoadInput() => File.ReadAllText("input.txt");
        private string SampleInput1() => "s1,x3/4,pe/b";
    }
}