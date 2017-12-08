using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;

namespace Day6
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
            var memory = input.Split('\t').Select(int.Parse).ToList();
            var seen = new List<List<int>> {memory.ToList()};
            var cycles = 1;

            while (true)
            {
                //Console.WriteLine($"Memory: {string.Join(", ", memory)}");
                var (max, maxIndex) = FindMaxAndIndex(memory);
                memory[maxIndex] = 0;
                //Console.WriteLine($"\tMax: {max} at {maxIndex}");

                for (var counter = 1; counter < max + 1; counter++)
                    memory[(maxIndex + counter) % memory.Count] += 1;

                //Console.WriteLine($"\tNew memory: {string.Join(", ", memory)}");
                var newMemory = memory.ToList();

                if (seen.Any(m => m.SequenceEqual(newMemory)))
                    break;

                seen.Add(newMemory);
                cycles += 1;
            }

            var indexOfLoop = seen.FindIndex(l => l.SequenceEqual(memory));

            Console.WriteLine($"Cycles in loop: {cycles - indexOfLoop}");
        }

        private (int Max, int Index) FindMaxAndIndex(IEnumerable<int> list)
        {
            var max = -1;
            var index = -1;
            var i = 0;
            foreach (var num in list)
            {
                if (num > max)
                {
                    max = num;
                    index = i;
                }

                i += 1;
            }

            return (max, index);
        }

        private string LoadInput() => File.ReadAllText("input.txt");
        private string SampleInput1() => "0\t2\t7\t0";
    }
}
