using System;
using System.Linq;
using System.Reflection;

namespace AdventOfCode
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    internal sealed class InputAttribute : Attribute
    {
        public object Input { get; }
        public string ExpectedOutput { get; }

        public InputAttribute(object input, string expectedOutput)
        {
            Input = input;
            ExpectedOutput = expectedOutput;
        }
    }

    internal abstract class Puzzle<TInput>
    {
        public abstract string SolvePart1<TOutput>(TInput input);
        public abstract string SolvePart2<TOutput>(TInput input);
    }

    internal class Program
    {
        private class PuzzleInfo
        {
            public int Day { get; }

            public PuzzleInfo(int day)
            {
                Day = day;
            }
        }

        private class PuzzleInput
        {
            public string Name { get; }
            public object Value { get; }
        }

        private static void Main(string[] args)
        {
            foreach (var puzzleClass in Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsClass && t.Namespace == "AdventOfCode.Puzzles" && t.IsSubclassOf(typeof(Puzzle<>))))
            {
                if (!int.TryParse(puzzleClass.Name.Substring(2), out var day))
                {
                    Console.WriteLine($"Invalid puzzle class: {puzzleClass.FullName}");
                    continue;
                }

                var puzzle = new PuzzleInfo(day);
                var part1Method = puzzleClass.GetMethod("SolvePart1");
            }
        }
    }
}
