using System;
using System.IO;

namespace Day3
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
            var ring = Math.Ceiling(0.5 * (Math.Sqrt(input) + 1));
            var minDist = ring - 1;
            var sideLength = 2 * ring - 1;
            var corner = sideLength * sideLength;
            var maxToCorner = Math.Floor(sideLength / 2);
            var toCorner = corner - input;

            Console.WriteLine($"Ring: {ring}, side length: {sideLength}, ring lower right corner: {corner}, max to corner: {maxToCorner}, input to corner: {toCorner}");

            while (toCorner > maxToCorner)
                toCorner -= sideLength;

            var loc = Math.Abs(toCorner);
            var toCenter = maxToCorner - loc;
            var dist = toCenter + minDist;
            Console.WriteLine($"Input location: {loc}, to center: {toCenter}, dist: {dist}");
        }

        private int LoadInput() => int.Parse(File.ReadAllText("input.txt"));
        private int SampleInput1() => 1;
        private int SampleInput2() => 12;
        private int SampleInput3() => 23;
        private int SampleInput4() => 1024;
    }
}
