﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Day14
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

            var sum = 0;
            for (var i = 0; i < 128; i++)
            {
                var hash = KnotHash($"{input}-{i}");
                //Console.WriteLine($"Hash of {input}-{i}: {hash}");
                foreach (var b in hash.Select(c => "0123456789ABCDEF".IndexOf(char.ToUpper(c))))
                {
                    var val = b;
                    var bitsSet = 0;

                    while (val != 0)
                    {
                        bitsSet += 1;
                        val &= val - 1;
                    }

                    sum += bitsSet;
                    //Console.WriteLine($"\tBits set in {b:X}: {bitsSet}");
                }
            }

            Console.WriteLine($"Sum: {sum}");
        }

        private void SolvePart2()
        {
            var input = SampleInput1();
            var grid = new bool[128, 128];
            
            for (var i = 0; i < 128; i++)
            {
                var hash = KnotHash($"{input}-{i}");
                var index = 0;
                foreach (var b in hash.Select(c => "0123456789ABCDEF".IndexOf(char.ToUpper(c))))
                {
                    var bitIndex = 0;

                    foreach (var bit in Convert.ToString(b, 2).PadLeft(4, '0'))
                        grid[index * 4 + bitIndex++, i] = bit == '1';

                    index += 1;
                }
            }

            void AddNeighbors((int x, int y) coords, ISet<(int, int)> region)
            {
                if (coords.x > 0)
                {
                    (int x, int y) neighbor = (coords.x - 1, coords.y);
                    if (grid[neighbor.x, neighbor.y] && !region.Contains(neighbor))
                    {
                        region.Add(neighbor);
                        AddNeighbors(neighbor, region);
                    }
                }

                if (coords.y > 0)
                {
                    (int x, int y) neighbor = (coords.x, coords.y - 1);
                    if (grid[neighbor.x, neighbor.y] && !region.Contains(neighbor))
                    {
                        region.Add(neighbor);
                        AddNeighbors(neighbor, region);
                    }
                }

                if (coords.x < 127)
                {
                    (int x, int y) neighbor = (coords.x + 1, coords.y);
                    if (grid[neighbor.x, neighbor.y] && !region.Contains(neighbor))
                    {
                        region.Add(neighbor);
                        AddNeighbors(neighbor, region);
                    }
                }

                if (coords.y < 127)
                {
                    (int x, int y) neighbor = (coords.x, coords.y + 1);
                    if (grid[neighbor.x, neighbor.y] && !region.Contains(neighbor))
                    {
                        region.Add(neighbor);
                        AddNeighbors(neighbor, region);
                    }
                }
            }

            var regions = new List<HashSet<(int, int)>>();

            for (var y = 0; y < 128; y++)
            {
                for (var x = 0; x < 128; x++)
                {
                    if (!grid[x, y])
                        continue;

                    var coords = (x, y);
                    var region = regions.FirstOrDefault(g => g.Contains(coords));

                    if (region != null)
                        continue;

                    region = new HashSet<(int, int)> {coords};
                    regions.Add(region);
                    AddNeighbors(coords, region);
                }
            }

            var sb = new StringBuilder();
            for (var y = 0; y < 128; y++)
            {
                for (var x = 0; x < 128; x++)
                {
                    if (!grid[x, y])
                    {
                        sb.Append(".".PadRight(5, ' '));
                        continue;
                    }

                    sb.Append(regions.FindIndex(r => r.Contains((x, y))).ToString().PadRight(5, ' '));
                }

                sb.AppendLine();
            }

            File.WriteAllText("output.txt", sb.ToString());

            Console.WriteLine($"Total regions: {regions.Count}");
        }

        private string KnotHash(string input)
        {
            var ascii = input.Select(n => (int)n).Concat(new[] { 17, 31, 73, 47, 23 }).ToList();
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

            return string.Join("", hashValues.Select(i => i.ToString("X")));
        }

        private string LoadInput() => "ugkiagan";
        private string SampleInput1() => "flqrgnkx";
    }
}