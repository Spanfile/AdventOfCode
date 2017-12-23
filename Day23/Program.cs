using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace Day23
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            new Program().SolvePart2AlternateAlternate();
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
                var jump = 1L;
                
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
                            jump = value;
                        //Console.WriteLine();
                        break;
                }

                index += jump;
                if (index < input.Length)
                    continue;
                
                Console.WriteLine($"Jumped outside of program. Mul invokes: {mulInvokes}");
                return;
            }
        }
        
        private void SolvePart2()
        {
            var input = LoadInput();
            var registers = new Dictionary<string, long> {
                {"a", 1L},
                {"b", 0L},
                {"c", 0L},
                {"d", 0L},
                {"e", 0L},
                {"f", 0L},
                {"g", 0L},
                {"h", 0L}
            };
            var index = 0L;
            
            while (true)
            {
                var ins = input[index];
                var args = ins.Split(' ');
                var jump = 1L;
                
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
                        break;

                    case "jnz":
                        if (!long.TryParse(args[1], out var cond))
                            cond = registers[args[1]];
                        if (!long.TryParse(args[2], out value))
                            value = registers[args[2]];
                        //Console.Write($"{pid}: jump (from {index}) {value} if {args[1]} ({cond}) > 0");

                        if (cond != 0)
                            jump = value;
                        //Console.WriteLine();
                        break;
                }

                index += jump;
                if (index < input.Length)
                    continue;
                
                Console.WriteLine($"Jumped outside of program. Register h: {registers["h"]}");
                return;
            }
        }

        private void SolvePart2Alternate()
        {
            var h = 0L;
            var b = 99L * 100L + 100000L;
            var c = b + 17000L;
            do
            {
                var f = true;
                var d = 2L;
                do
                {
                    if (b % d == 0)
                        f = false;
                    
                    d += 1;
                } while (d != b && f);

                if (f == false)
                {
                    h += 1;
                    Console.WriteLine($"{b} {h}");
                }

                if (b == c)
                {
                    Console.WriteLine($"Final value of h: {h}");
                    return;
                }
                b += 17;
            } while (true);
        }

        private void SolvePart2AlternateAlternate()
        {
            var min = 99;
            min *= 100;
            min += 100000;
            var max = min + 17000;
            var nonPrimes = 0L;
            Console.WriteLine($"Checking {max - min} values from {min} to {max}");

            for (var primeCandidate = min; primeCandidate <= max; primeCandidate += 17)
            {
                var isPrime = true;
                for (var a = 2; a != primeCandidate; a += 1)
                {
                    if (primeCandidate % a != 0)
                        continue;
                    
                    isPrime = false;
                    break;
                }

                if (!isPrime)
                {
                    nonPrimes += 1;
                    //Console.WriteLine($"{primeCandidate} is prime");
                }
            }
            
            Console.WriteLine($"Non-primes: {nonPrimes}");
        }

        private string[] LoadInput() => File.ReadAllLines("input.txt");
    }
}