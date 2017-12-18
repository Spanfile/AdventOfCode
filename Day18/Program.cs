using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day18
{
    internal enum ProgramState
    {
        Send, Receive
    }

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
            var registers = new Dictionary<string, long> {
                {"a", 0L},
                {"b", 0L},
                {"f", 0L},
                {"i", 0L},
                {"p", 0L}
            };

            var index = 0L;
            var lastSoundFreq = 0L;
            while (true)
            {
                var ins = input[index];
                var args = ins.Split(' ');

                long value;
                switch (args[0])
                {
                    case "set":
                        if (!long.TryParse(args[2], out value))
                            value = registers[args[2]];
                        Console.WriteLine($"set reg {args[1]} value {value} ({args[2]})");
                        registers[args[1]] = value;
                        break;

                    case "add":
                        if (!long.TryParse(args[2], out value))
                            value = registers[args[2]];
                        Console.WriteLine($"add reg {args[1]} value {value} ({args[2]})");
                        registers[args[1]] += value;
                        break;

                    case "mul":
                        if (!long.TryParse(args[2], out value))
                            value = registers[args[2]];
                        Console.WriteLine($"mul reg {args[1]} value {value} ({args[2]})");
                        registers[args[1]] *= value;
                        break;

                    case "mod":
                        if (!long.TryParse(args[2], out value))
                            value = registers[args[2]];
                        Console.WriteLine($"mod reg {args[1]} value {value} ({args[2]})");
                        registers[args[1]] %= value;
                        break;

                    case "jgz":
                        if (!long.TryParse(args[2], out value))
                            value = registers[args[2]];
                        Console.Write($"jump (from {index}) {value} if {args[1]} ({registers[args[1]]}) > 0");

                        if (registers[args[1]] > 0)
                        {
                            index = (index + value) % input.Length;
                            Console.WriteLine($" -> new index {index}");
                            continue;
                        }
                        Console.WriteLine();
                        break;

                    case "snd":
                        lastSoundFreq = registers[args[1]];
                        Console.WriteLine($"snd {lastSoundFreq} ({args[1]})");
                        break;

                    case "rcv":
                        Console.WriteLine($"Frequency recovered: {lastSoundFreq}");
                        return;
                }

                index = (index + 1) % input.Length;
            }
        }

        private void SolvePart2()
        {
            var input = LoadInput();
            var pid0Queue = new Queue<long>();
            var pid1Queue = new Queue<long>();
            var pid0 = PuzzleProgram(input.ToArray(), 0, pid1Queue).GetEnumerator();
            var pid1 = PuzzleProgram(input.ToArray(), 1, pid0Queue).GetEnumerator();
            var pid1Sends = 0;

            var deadlock = false;
            var currentPid = pid0;
            while (true)
            {
                while (true)
                {
                    if (!currentPid.MoveNext())
                    {
                        deadlock = true;
                        Console.WriteLine("Deadlock reached");
                        break;
                    }

                    if (currentPid.Current.Item1 == ProgramState.Send)
                    {
                        if (ReferenceEquals(currentPid, pid0))
                            pid0Queue.Enqueue(currentPid.Current.Item2);
                        else
                        {
                            pid1Queue.Enqueue(currentPid.Current.Item2);
                            pid1Sends += 1;
                        }
                    }
                    else
                        break;
                }

                if (deadlock)
                    break;

                currentPid = ReferenceEquals(currentPid, pid0) ? pid1 : pid0;
            }

            Console.WriteLine($"PID 1 sent {pid1Sends} values");
        }

        IEnumerable<(ProgramState, long)> PuzzleProgram(string[] input, long pid, Queue<long> otherQueue)
        {
            var registers = new Dictionary<string, long> {
                {"a", 0L},
                {"b", 0L},
                {"f", 0L},
                {"i", 0L},
                {"p", pid}
            };
            var index = 0L;

            while (true)
            {
                var ins = input[index];
                var args = ins.Split(' ');

                long value;
                switch (args[0])
                {
                    case "set":
                        if (!long.TryParse(args[2], out value))
                            value = registers[args[2]];
                        //Console.WriteLine($"{pid}: set reg {args[1]} value {value} ({args[2]})");
                        registers[args[1]] = value;
                        break;

                    case "add":
                        if (!long.TryParse(args[2], out value))
                            value = registers[args[2]];
                        //Console.WriteLine($"{pid}: add reg {args[1]} value {value} ({args[2]})");
                        registers[args[1]] += value;
                        break;

                    case "mul":
                        if (!long.TryParse(args[2], out value))
                            value = registers[args[2]];
                        //Console.WriteLine($"{pid}: mul reg {args[1]} value {value} ({args[2]})");
                        registers[args[1]] *= value;
                        break;

                    case "mod":
                        if (!long.TryParse(args[2], out value))
                            value = registers[args[2]];
                        //Console.WriteLine($"{pid}: mod reg {args[1]} value {value} ({args[2]})");
                        registers[args[1]] %= value;
                        break;

                    case "jgz":
                        if (!long.TryParse(args[1], out var cond))
                            cond = registers[args[1]];
                        if (!long.TryParse(args[2], out value))
                            value = registers[args[2]];
                        //Console.Write($"{pid}: jump (from {index}) {value} if {args[1]} ({cond}) > 0");

                        if (cond > 0)
                        {
                            index = (index + value) % input.Length;
                            //Console.WriteLine($" -> new index {index}");
                            continue;
                        }
                        //Console.WriteLine();
                        break;

                    case "snd":
                        value = registers[args[1]];
                        //Console.WriteLine($"{pid} @ {index}: snd {value} ({args[1]})");
                        yield return (ProgramState.Send, value);
                        break;

                    case "rcv":
                        if (!otherQueue.TryDequeue(out value))
                        {
                            //Console.WriteLine($"{pid} @ {index}: rcv {args[1]} - no value in queue");
                            yield return (ProgramState.Receive, 0);

                            if (!otherQueue.TryDequeue(out value))
                                yield break;
                        }

                        registers[args[1]] = value;
                        //Console.WriteLine($"{pid} @ {index}: rcv {args[1]} ({registers[args[1]]})");
                        break;
                }

                index = (index + 1) % input.Length;
            }
        }

        private string[] LoadInput() => File.ReadAllLines("input.txt");

        private string[] SamleInput1() => new[] {
            "set a 1",
            "add a 2",
            "mul a a",
            "mod a 5",
            "snd a",
            "set a 0",
            "rcv a",
            "jgz a -1",
            "set a 1",
            "jgz a -2",
        };

        private string[] SampleInput2() => new[] {
            "snd 1",
            "snd 2",
            "snd p",
            "rcv a",
            "rcv b",
            "rcv c",
            "rcv d"
        };
    }
}
