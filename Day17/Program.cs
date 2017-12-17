using System;
using System.Collections.Generic;

namespace Day17
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
            var buffer = new List<int> {0};
            var index = 0;

            for (var i = 1; i < 2018; i++)
            {
                index = (index + input) % buffer.Count + 1;
                if (index >= buffer.Count)
                    buffer.Add(i);
                else
                    buffer.Insert(index, i);
            }

            Console.WriteLine($"Value after 2017: {buffer[(index + 1) % buffer.Count]}");
        }

        private void SolvePart2()
        {
            var input = LoadInput();
            var index = 0;
            var bufferLength = 1;
            var afterZero = -1;

            for (var i = 1; i < 50_000_001; i++)
            {
                index = (index + input) % bufferLength + 1;

                //Console.WriteLine($"{i} -> {index} ({bufferLength})");

                bufferLength += 1;

                if (index == 1)
                    afterZero = i;
            }

            Console.WriteLine($"Value after 0: {afterZero}");
        }

        private int LoadInput() => 348;
        private int SampleInput1() => 3;
    }
}
