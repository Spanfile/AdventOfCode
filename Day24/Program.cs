using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day24
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
            var input = LoadInput().Select(l =>
            {
                var args = l.Split('/');
                return (int.Parse(args[0]), int.Parse(args[1]));
            });

            var components = new Dictionary<int, List<int>>();

            foreach (var component in input)
            {
                if (components.ContainsKey(component.Item1))
                    components[component.Item1].Add(component.Item2);
                else
                    components.Add(component.Item1, new List<int> {component.Item2});
                
                if (components.ContainsKey(component.Item2))
                    components[component.Item2].Add(component.Item1);
                else
                    components.Add(component.Item2, new List<int> {component.Item1});
            }

            var bridgeInfos = BuildBridges(components)
                .Select(bridge => (bridge.Count, bridge.Sum(component => component.Item1 + component.Item2)))
                .ToArray();
            var strongest = bridgeInfos.OrderByDescending(i => i.Item2).First().Item2;
            var longest = bridgeInfos.OrderByDescending(i => i.Item1).First().Item2;
            Console.WriteLine($"Strongest: {strongest}, longest and strongest: {longest}");
        }

        private IEnumerable<List<(int, int)>> BuildBridges(Dictionary<int, List<int>> components,
            List<(int, int)> bridge = null)
        {
            if (bridge == null)
                bridge = new List<(int, int)> {(0, 0)};

            var current = bridge[bridge.Count - 1].Item2;
            foreach (var b in components[current])
            {
                if (bridge.Contains((current, b)) || bridge.Contains((b, current)))
                    continue;
                
                var newBridge = new List<(int, int)>(bridge).Append((current, b)).ToList();
                yield return newBridge;
                foreach (var other in BuildBridges(components, newBridge))
                    yield return other;
            }
        }

        private string[] LoadInput() => File.ReadAllLines("input.txt");

        private string[] SampleInput1() => new[]
        {
            "0/2",
            "2/2",
            "2/3",
            "3/4",
            "3/5",
            "0/1",
            "10/1",
            "9/10"
        };
    }
}
