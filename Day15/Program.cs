using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Day15
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
            var gen = Combine(Generator(input.Gen1Start, 16807).GetEnumerator(), Generator(input.Gen2Start, 48271).GetEnumerator());

            var total = gen.Take(40_000_000).Count(vals => vals.Item1 << 48 == vals.Item2 << 48);
            Console.WriteLine($"Total: {total}");
        }

        private void SolvePart2()
        {
            var input = LoadInput();
            var gen = Combine(Generator(input.Gen1Start, 16807, 4).GetEnumerator(), Generator(input.Gen2Start, 48271, 8).GetEnumerator());

            var total = gen.Take(5_000_000).Count(vals => vals.Item1 << 48 == vals.Item2 << 48);
            Console.WriteLine($"Total: {total}");
        }

        private IEnumerable<(T, T)> Combine<T>(IEnumerator<T> first, IEnumerator<T> second)
        {
            while (first.MoveNext() && second.MoveNext())
                yield return (first.Current, second.Current);
        }

        private IEnumerable<long> Generator(int startingValue, int factor, int requiredMultiple = 1)
        {
            long prev = startingValue;

            while (true)
            {
                long val;

                while ((val = prev * factor % 2147483647) % requiredMultiple != 0)
                    prev = val;

                yield return val;
                prev = val;
            }
        }

        private (int Gen1Start, int Gen2Start) LoadInput() => (722, 354);
        private (int Gen1Start, int Gen2Start) SampleInput1() => (65, 8921);
    }
}
