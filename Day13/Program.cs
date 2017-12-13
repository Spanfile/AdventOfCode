using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day13
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
            var input = LoadInput();
            var firewall = new List<int>();

            foreach (var line in input)
            {
                var args = line.Split(": ");
                var layer = int.Parse(args[0]);
                var range = int.Parse(args[1]);
                
                while (firewall.Count < layer)
                    firewall.Add(0);

                firewall.Add(range);
            }

            var depth = 0;
            var tripSeverity = 0;
            foreach (var range in firewall)
            {
                if (range == 0)
                {
                    depth += 1;
                    continue;
                }

                var period = range * 2 - 2;

                Console.WriteLine($"Depth: {depth}, range {range}, period: {period}");

                if (depth % period == 0)
                {
                    Console.WriteLine($"\tHit for severity {depth * range}");
                    tripSeverity += depth * range;
                }

                depth += 1;
            }

            Console.WriteLine($"Trip severity: {tripSeverity}");
        }

        private void SolvePart2()
        {
            var input = LoadInput();
            var firewall = new List<int>();

            foreach (var line in input)
            {
                var args = line.Split(": ");
                var layer = int.Parse(args[0]);
                var range = int.Parse(args[1]);

                while (firewall.Count < layer)
                    firewall.Add(0);

                firewall.Add(range);
            }

            var delay = 0L;
            while (true)
            {
                var depth = 0;
                var hit = false;
                foreach (var range in firewall)
                {
                    if (range == 0)
                    {
                        depth += 1;
                        continue;
                    }

                    var period = range * 2 - 2;

                    if ((depth + delay) % period == 0)
                    {
                        hit = true;
                        //Console.WriteLine($"Hit with range {range} (period {period}) at depth {depth} (delay {delay})");
                        break;
                    }

                    depth += 1;
                }

                if (!hit)
                    break;

                delay += 1;
            }

            Console.WriteLine($"Delay: {delay}");
        }

        private string[] LoadInput() => File.ReadAllLines("input.txt");

        private string[] SampleInput1() => new[] {
            "0: 3",
            "1: 2",
            "4: 4",
            "6: 4"
        };
    }
}
