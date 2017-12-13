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
            new Program().SolvePart1();
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

            var groups = new List<HashSet<int>>();

            void RecurseConns(ISet<int> ids, int id)
            {
                if (ids.Contains(id))
                    return;

                ids.Add(id);
                foreach (var conn in conns[id])
                    RecurseConns(ids, conn);
            }

            foreach (var key in conns.Keys)
            {
                if (groups.Any(s => s.Contains(key)))
                    continue;

                var group = new HashSet<int>();
                groups.Add(group);
                RecurseConns(group, key);
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
