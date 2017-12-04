using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            new Program().SolvePart1();
            Console.ReadKey();
        }

        private Program()
        {
        }

        private void SolvePart1()
        {
            var input = LoadInput();
            var checksum = input.Select(line => line.Split('\t').Select(int.Parse)).Sum(n => n.Max() - n.Min());
            Console.WriteLine(checksum);
        }

        private void SolvePart2()
        {
            var input = LoadInput();
            var rows = input.Select(line => line.Split('\t').Select(int.Parse));
            var checksum = 0;
            foreach (var row in rows)
            {
                foreach (var (n1, n2) in
                    from n1 in row
                    from n2 in row
                    where n1 < n2
                    select (n1, n2))
                {
                    var min = Math.Min(n1, n2);
                    var max = Math.Max(n1, n2);
                    if (max % min == 0)
                        checksum += max / min;
                }
            }
            Console.WriteLine(checksum);
        }

        private IEnumerable<string> LoadInput() => File.ReadAllLines("input.txt");

        private string[] SampleInput1() => new[] {
            "5\t1\t9\t5",
            "7\t5\t3",
            "2\t4\t6\t8"
        };

        private string[] SampleInput2() => new[] {
            "5\t9\t2\t8",
            "9\t4\t7\t3",
            "3\t8\t6\t5"
        };
    }
}
