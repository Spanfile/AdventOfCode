using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Day11
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
            var input = LoadInput().Split(',');
            var x = 0;
            var y = 0;
            var maxDist = 0;

            foreach (var dir in input)
            {
                switch (dir)
                {
                    case "n":
                        y += 1;
                        break;

                    case "s":
                        y -= 1;
                        break;

                    case "ne":
                        //y += 1;
                        x += 1;
                        break;

                    case "se":
                        y -= 1;
                        x += 1;
                        break;

                    case "sw":
                        //y -= 1;
                        x -= 1;
                        break;

                    case "nw":
                        y += 1;
                        x -= 1;
                        break;
                }

                var dist = (Math.Abs(x) + Math.Abs(y) + Math.Abs(x + y)) / 2;
                //Console.WriteLine($"Current distance: {dist}, maximum distance so far: {maxDist}");
                maxDist = Math.Max(maxDist, dist);
            }

            Console.WriteLine($"Distance: {(Math.Abs(x) + Math.Abs(y) + Math.Abs(x + y)) / 2}, maximum distance: {maxDist}");
        }

        private string LoadInput() => File.ReadAllText("input.txt");
        private string SampleInput1() => "ne,ne,ne";
        private string SampleInput2() => "ne,ne,sw,sw";
        private string SampleInput3() => "ne,ne,s,s";
        private string SampleInput4() => "se,sw,se,sw,sw";
    }
}
