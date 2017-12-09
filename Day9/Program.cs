using System;
using System.IO;

namespace Day9
{
    internal enum State
    {
        Group, Garbage, Escape
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            new Program().Solve();
            Console.ReadLine();
        }

        private Program()
        {
        }

        private void Solve()
        {
            var input = LoadInput();
            var state = State.Group;
            var score = 0;
            var depth = 0;
            var garbageCounter = 0;

            foreach (var c in input)
            {
                switch (state)
                {
                    case State.Group:
                        if (c == '{')
                            depth += 1;
                        else if (c == '}')
                        {
                            score += depth;
                            depth -= 1;
                        }
                        else if (c == '<')
                            state = State.Garbage;
                        continue;

                    case State.Garbage:
                        if (c == '>')
                            state = State.Group;
                        else if (c == '!')
                            state = State.Escape;
                        else
                            garbageCounter += 1;
                        continue;

                    case State.Escape:
                        state = State.Garbage;
                        continue;
                }
            }

            Console.WriteLine($"Score: {score}, total garbage: {garbageCounter}");
        }

        private string LoadInput() => File.ReadAllText("input.txt");
        private string SampleInput1() => "{}"; // 1
        private string SampleInput2() => "{{{}}}"; // 1 + 2 + 3 = 6
        private string SampleInput3() => "{{},{}}"; // 1 + 2 + 2 = 5
        private string SampleInput4() => "{{{},{},{{}}}}"; // 1 + 2 + 3 + 3 + 3 + 4 = 16
        private string SampleInput5() => "{<a>,<a>,<a>,<a>}"; // 1
        private string SampleInput6() => "{{<ab>},{<ab>},{<ab>},{<ab>}}"; // 1 + 2 + 2 + 2 + 2 = 9
        private string SampleInput7() => "{{<!!>},{<!!>},{<!!>},{<!!>}}"; // 1 + 2 + 2 + 2 + 2 = 9
        private string SampleInput8() => "{{<a!>},{<a!>},{<a!>},{<ab>}}"; // 1 + 2 = 3
    }
}
