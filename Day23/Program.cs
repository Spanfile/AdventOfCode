using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Authentication;

namespace Day23
{
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
            var registers = new Dictionary<string, long> {
                {"a", 0L},
                {"b", 0L},
                {"c", 0L},
                {"d", 0L},
                {"e", 0L},
                {"f", 0L},
                {"g", 0L},
                {"h", 0L}
            };
            var index = 0L;
            var mulInvokes = 0L;

            while (true)
            {
                var ins = input[index];
                var args = ins.Split(' ');

                long value;
                switch (args[0])
                {
                    case "set":
                        if (!long.TryParse(args[2], out value))
                            value = registers[args[2]];
                        //Console.WriteLine($"{pid}: set reg {args[1]} value {value} ({args[2]})");
                        registers[args[1]] = value;
                        break;

                    case "sub":
                        if (!long.TryParse(args[2], out value))
                            value = registers[args[2]];
                        //Console.WriteLine($"{pid}: add reg {args[1]} value {value} ({args[2]})");
                        registers[args[1]] -= value;
                        break;

                    case "mul":
                        if (!long.TryParse(args[2], out value))
                            value = registers[args[2]];
                        //Console.WriteLine($"{pid}: mul reg {args[1]} value {value} ({args[2]})");
                        registers[args[1]] *= value;
                        mulInvokes += 1;
                        break;

                    case "jnz":
                        if (!long.TryParse(args[1], out var cond))
                            cond = registers[args[1]];
                        if (!long.TryParse(args[2], out value))
                            value = registers[args[2]];
                        //Console.Write($"{pid}: jump (from {index}) {value} if {args[1]} ({cond}) > 0");

                        if (cond != 0)
                        {
                            index = (index + value) % input.Length;
                            //Console.WriteLine($" -> new index {index}");
                            continue;
                        }
                        //Console.WriteLine();
                        break;
                }

                index = (index + 1) % input.Length;
            }
        }

        private string[] LoadInput() => File.ReadAllLines("input.txt");
    }
}