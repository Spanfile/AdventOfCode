using System;
using System.IO;
using System.Linq;

namespace Day5
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
            var input = LoadInput().Select(int.Parse).ToList();
            var index = 0;
            var jumps = 0;

            while (index < input.Count && index >= 0)
            {
                Console.WriteLine($"On index {index}, jumping {input[index]} steps to {index + input[index]}. New value here: {input[index] + 1}");
                var temp = input[index];
                input[index] += 1;
                index += temp;

                jumps += 1;
            }

            Console.WriteLine($"Final jumps: {jumps}");
        }

        private void SolvePart2()
        {
            var input = LoadInput().Select(int.Parse).ToList();
            var index = 0;
            var jumps = 0;

            while (index < input.Count && index >= 0)
            {
                //Console.WriteLine($"On index {index}, jumping {input[index]} steps to {index + input[index]}");
                var temp = input[index];
                if (temp >= 3)
                    input[index] -= 1;
                else
                    input[index] += 1;
                index += temp;

                jumps += 1;
            }

            Console.WriteLine($"Final jumps: {jumps}");
        }

        private string[] LoadInput() => File.ReadAllLines("input.txt");
        private string[] SampleInput1() => new[] {"0", "3", "0", "1", "-3"};
    }
}
