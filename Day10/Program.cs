using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
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

            var lengths = input.Split(',').Select(int.Parse).ToList();
            var numbers = Enumerable.Range(0, 256).ToList();
            //var numbers = Enumerable.Range(0, 5).ToList();
            var currentPos = 0;
            var skipSize = 0;

            foreach (var length in lengths)
            {
                Console.WriteLine($"Current pos: {currentPos}, length: {length}, skip size: {skipSize}");
                var sublist = new List<int>();

                for (var i = currentPos; i < currentPos + length; i++)
                    sublist.Add(numbers[i % numbers.Count]);

                Console.WriteLine($"\tSublist: {string.Join(" ", sublist)}");
                sublist.Reverse();
                Console.WriteLine($"\tReversed: {string.Join(" ", sublist)}");

                for (var i = 0; i < length; i++)
                    numbers[(currentPos + i) % numbers.Count] = sublist[i];

                Console.WriteLine($"\tNumbers: {string.Join(" ", numbers)}");

                currentPos += length + skipSize;
                skipSize += 1;
            }

            Console.WriteLine($"First two numbers multiplied: {numbers[0] * numbers[1]}");
        }

        private void SolvePart2()
        {
            var input = LoadInput();

            var ascii = input.Select(n => (int)n).Concat(new[] {17, 31, 73, 47, 23}).ToList();
            var numbers = Enumerable.Range(0, 256).ToList();
            var currentPos = 0;
            var skipSize = 0;

            for (var i = 0; i < 64; i++)
            {
                foreach (var character in ascii)
                {
                    var sublist = new List<int>();

                    for (var n = currentPos; n < currentPos + character; n++)
                        sublist.Add(numbers[n % numbers.Count]);

                    sublist.Reverse();

                    for (var n = 0; n < character; n++)
                        numbers[(currentPos + n) % numbers.Count] = sublist[n];

                    currentPos += character + skipSize;
                    skipSize += 1;
                }
            }

            var hashValues = new int[16];
            for (var i = 0; i < 16; i++)
                hashValues[i] = numbers.Skip(i * 16).Take(16).Aggregate((a, b) => a ^ b);

            var hash = string.Join("", hashValues.Select(i => i.ToString("X")));
            Console.WriteLine($"Hash: {hash}");
        }

        private string LoadInput() => File.ReadAllText("input.txt");
        private string SampleInput1() => "3,4,1,5";
        private string SampleInput2() => "";
        private string SampleInput3() => "AoC 2017";
        private string SampleInput4() => "1,2,3";
        private string SampleInput5() => "1,2,4";
    }
}
