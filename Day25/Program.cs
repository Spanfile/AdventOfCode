using System;
using System.Collections.Generic;
using System.Linq;

namespace Day25
{
    internal class Program
    {
        private class State
        {
            private (int, int, string) trueAction;
            private (int, int, string) falseAction;

            public State((int, int, string) trueAction, (int, int, string) falseAction)
            {
                this.trueAction = trueAction;
                this.falseAction = falseAction;
            }

            public (int, int, string) Evaluate(int value) => value == 1 ? trueAction : falseAction;
        }
        
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
            var diagnostic = input.Item1;
            var states = input.Item2;
            var tape = new Dictionary<int, int> {{0, 0}};
            var pos = 0;
            var stateLetter = "A";

            for (var step = 0; step < diagnostic; step++)
            {
                var state = states[stateLetter];
                (var value, var move, string newStateLetter) = state.Evaluate(tape[pos]);
                tape[pos] = value;
                pos += move;
                stateLetter = newStateLetter;
                
                if (!tape.ContainsKey(pos))
                    tape.Add(pos, 0);
            }

            var ones = tape.Values.Sum();
            Console.WriteLine($"Count of ones: {ones}");
        }
        
        private (int, Dictionary<string, State>) LoadInput() =>
            (12173597, new Dictionary<string, State>
            {
                {"A", new State((0, -1, "C"), (1, 1, "B"))},
                {"B", new State((1, 1, "D"), (1, -1, "A"))},
                {"C", new State((0, -1, "E"), (1, 1, "A"))},
                {"D", new State((0, 1, "B"), (1, 1, "A"))},
                {"E", new State((1, -1, "C"), (1, -1, "F"))},
                {"F", new State((1, 1, "A"), (1, 1, "D"))},
            });

        private (int, Dictionary<string, State>) SampleInput1() =>
            (6, new Dictionary<string, State>
            {
                {"A", new State((0, -1, "B"), (1, 1, "B"))},
                {"B", new State((1, 1, "A"), (1, -1, "A"))},
            });
    }
}