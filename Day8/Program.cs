using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day8
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            new Program().Solve();
            Console.ReadLine();
        }

        private Program()
        {
        }

        private void Solve()
        {
            var input = LoadInput();
            var registers = new Dictionary<string, int>();
            var highest = int.MinValue;

            foreach (var line in input)
            {
                var args = line.Split(' ');
                var reg = args[0];
                var action = args[1];
                var amount = int.Parse(args[2]);
                var targetReg = args[4];
                var comparison = args[5];
                var comparisonAmount = int.Parse(args[6]);

                if (!registers.ContainsKey(reg))
                    registers.Add(reg, 0);

                if (!registers.ContainsKey(targetReg))
                    registers.Add(targetReg, 0);

                switch (comparison)
                {
                    case "<":
                        if (registers[targetReg] < comparisonAmount)
                            break;
                        continue;

                    case ">":
                        if (registers[targetReg] > comparisonAmount)
                            break;
                        continue;

                    case ">=":
                        if (registers[targetReg] >= comparisonAmount)
                            break;
                        continue;

                    case "<=":
                        if (registers[targetReg] <= comparisonAmount)
                            break;
                        continue;

                    case "==":
                        if (registers[targetReg] == comparisonAmount)
                            break;
                        continue;

                    case "!=":
                        if (registers[targetReg] != comparisonAmount)
                            break;
                        continue;
                }

                if (action == "inc")
                    registers[reg] += amount;
                else
                    registers[reg] -= amount;

                if (registers[reg] > highest)
                    highest = registers[reg];
            }

            var largest = registers.Values.Max();
            Console.WriteLine($"Largest: {largest}, highest: {highest}");
        }

        private IEnumerable<string> LoadInput() => File.ReadLines("input.txt");

        private string[] SampleInput1() => new[] {
            "b inc 5 if a > 1",
            "a inc 1 if b < 5",
            "c dec -10 if a >= 1",
            "c inc -20 if c == 10"
        };
    }
}
