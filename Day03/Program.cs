using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day3
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

        private void SolvePart2()
        {
            var input = LoadInput();
            var firstLarger = MemoryValues().SkipWhile(i => i < input).First();
            Console.WriteLine($"First value larger than {input}: {firstLarger}");
        }

        private IEnumerable<int> MemoryValues()
        {
            var memory = new List<int> {0, 1, 2, 4, 5, 10, 11, 23, 25};

            // explicit ring 1
            //yield return 1;

            var index = 10;
            var ring = 3;
            while (true)
            {
                Console.WriteLine($"Ring {ring}");
                var ringInfo = GetRingInfo(ring);
                var innerRing = GetRingInfo(ring - 1);

                var innerCorner = innerRing.SideLength * innerRing.SideLength;
                var innerMinIndex = innerCorner - (innerRing.SideLength - 1) * 4 + 1;
                var innerSideCenter = innerCorner - innerRing.SideLength / 2;
                Console.WriteLine($"Inner: corner: {innerCorner}, min index: {innerMinIndex}, max to corner: {innerRing.MaxToCorner}, side 4 center: {innerSideCenter}");

                for (var side = 0; side < 4; side++)
                {
                    Console.WriteLine($"\tSide {side}");
                    var currentSideLocation = (ringInfo.MaxToCorner - 1) * (2 - side > 0 ? 1 : -1);
                    for (var i = 0; i < ringInfo.SideLength - 1; i++)
                    {
                        Console.WriteLine($"\t\t{currentSideLocation} -> {index}");
                        var value = memory[index - 2];
                        if (i == 0)
                        {
                            if (side == 0)
                            {
                                Console.WriteLine("\t\tFirst value in side and ring: no initial value (0)");
                                value = 0;
                            }
                            else
                            {
                                Console.WriteLine($"\t\tFirst value in side: initial value is the sum of two previous values ({index - 1}: {memory[index - 2]} + {index - 2}: {memory[index - 3]})");
                                value += memory[index - 3];
                            }
                        }
                        else if (i == ringInfo.SideLength - 2)
                            Console.WriteLine($"\t\tLast value in side");
                        else if (i == ringInfo.SideLength - 3)
                            Console.WriteLine($"\t\tSecond to last value in side");
                        else
                            Console.WriteLine($"\t\tMiddle value in side");

                        Console.WriteLine($"\t\tInitial value: {value}");

                        var correspondingSideCenter = innerSideCenter - (innerRing.SideLength - 1) * (3 - side);
                        var correspondingSideOffset =
                            correspondingSideCenter + currentSideLocation * (side < 2 ? -1 : 1);

                        Console.WriteLine($"\t\t\tCurrent side location: {currentSideLocation}, corresponding side center: {correspondingSideCenter}, corresponding side offset: {correspondingSideOffset}");

                        var nStart = -1;
                        if (i == 0)
                            nStart = 0;

                        var nStop = 2;
                        if (i == ringInfo.SideLength - 2)
                            nStop = side == 3 ? 1 : 0;
                        else if (i == ringInfo.SideLength - 3 && side != 3)
                            nStop = 1;

                        Console.WriteLine($"\t\t\tChecking neighbors from {nStart} to {nStop}");

                        for (var n = nStart; n < nStop; n++)
                        {
                            var innerSideNeighbor = correspondingSideOffset + n;

                            if (innerSideNeighbor < innerMinIndex)
                            {
                                //if (i == 0)
                                //{
                                //    Console.WriteLine($"\t\t\tSkipping neighbor {innerSideNeighbor}: out of its ring ({ring - 1}) range ({innerCorner}) and current index is the first in side");
                                //    continue;
                                //}

                                innerSideNeighbor = innerCorner;
                            }

                            //if (innerSideNeighbor > innerCorner)
                            //{
                            //    Console.WriteLine($"\t\t\tSkipping neighbor {innerSideNeighbor}: out of its ring ({ring - 1}) range ({innerCorner})");
                            //    continue;
                            //}

                            value += memory[innerSideNeighbor - 1];
                            Console.WriteLine($"\t\t\tInner ring neighbor: {innerSideNeighbor}, its value: {memory[innerSideNeighbor - 1]}, value so far: {value}");
                        }

                        memory.Add(value);
                        Console.WriteLine($"\t\tValue for {index}: {value}");

                        currentSideLocation -= 2 - side > 0 ? 1 : -1;
                        index += 1;
                        yield return value;
                    }
                }

                ring += 1;
            }
        }

        private (int SideLength, int RingLength, int MaxToCorner) GetRingInfo(int ring)
        {
            var sideLength = 2 * ring - 1;
            return (sideLength, sideLength * 4 - 2, sideLength / 2);
        }

        private int LoadInput() => int.Parse(File.ReadAllText("input.txt"));
        private int SampleInput1() => 1;
        private int SampleInput2() => 12;
        private int SampleInput3() => 23;
        private int SampleInput4() => 1024;
    }
}
