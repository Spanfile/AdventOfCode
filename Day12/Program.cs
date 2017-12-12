using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day12
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
            var conns = new Dictionary<int, List<int>>();

            foreach (var line in input)
            {
                var args = line.Split(' ');
                var id = int.Parse(args[0]);
                var connections = args.Skip(2).Select(s => int.Parse(s.Trim(',')));

                conns.Add(id, new List<int>());
                var connList = conns[id];
                connList.AddRange(connections);
            }

            var seen = new HashSet<int>();

            void RecurseConns(int id)
            {
                if (seen.Contains(id))
                    return;

                seen.Add(id);
                foreach (var conn in conns[id])
                    RecurseConns(conn);
            }

            RecurseConns(0);

            Console.WriteLine($"Connections to 0: {seen.Count}");
        }

        private void SolvePart2()
        {
            var input = LoadInput();
            var conns = new Dictionary<int, List<int>>();

            foreach (var line in input)
            {
                var args = line.Split(' ');
                var id = int.Parse(args[0]);
                var connections = args.Skip(2).Select(s => int.Parse(s.Trim(',')));

                conns.Add(id, new List<int>());
                var connList = conns[id];
                connList.AddRange(connections);
            }

            var max = conns.Keys.Max();
            var groups = new List<HashSet<int>>();

            for (var i = 0; i < max; i++)
            {
                var group = groups.FirstOrDefault(s => s.Contains(i) || conns[i].Any(s.Contains));

                if (group == null)
                {
                    group = new HashSet<int> {i};
                    groups.Add(group);
                }

                foreach (var conn in conns[i])
                    group.Add(conn);
            }

            Console.WriteLine($"Groups: {groups.Count}");
        }

        private IEnumerable<string> LoadInput() => File.ReadLines("input.txt");

        private string[] SampleInput1() => new[] {
            "0 <-> 2",
            "1 <-> 1",
            "2 <-> 0, 3, 4",
            "3 <-> 2, 4",
            "4 <-> 2, 3, 6",
            "5 <-> 6",
            "6 <-> 4, 5"
        };
    }
}
