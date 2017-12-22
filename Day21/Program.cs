using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day21
{
    internal class Program
    {
        internal class Art
        {
            private int Size => rows.Length;
            private int SubpatternSize => Size % 2 == 0 ? 2 : 3;

            private readonly string[] rows;

            public Art(string[] rows)
            {
                this.rows = rows;
            }

            public Art ApplyRules(List<Rule> rules)
            {
                Console.WriteLine($"\tExisting size: {Size}, existing subpattern size: {SubpatternSize}. Determining subpatterns...");
                var subpatterns = Subpatterns().ToArray();
                Console.WriteLine($"\tEnhancing {subpatterns.Length} subpatterns with {rules.Count} rules...");
                var enhancedPatterns =
                    (from p in subpatterns.AsParallel()
                     from r in rules
                     let match = r.Match(p)
                     where match != null
                     select match).ToArray();

                var newSubpatternSize = SubpatternSize == 2 ? 3 : 4;
                var newSideLength = (Size * SubpatternSize + Size) / SubpatternSize / newSubpatternSize;
                var newCount = newSideLength * newSideLength;
                var newRows = new string[newSideLength * newSubpatternSize];

                Console.WriteLine($"\tNew subpattern size: {newSubpatternSize}, " +
                                  $"new side length: {newSideLength}, " +
                                  $"new subpattern count: {newCount}, " +
                                  $"new rows: {newRows.Length}");

                for (var i = 0; i < newSideLength * newSubpatternSize; i++)
                    newRows[i] = "";

                for (var i = 0; i < newCount; i++)
                {
                    //var pX = i % newSideLength;
                    var pY = i / newSideLength;
                    var p = enhancedPatterns[i];

                    for (var j = 0; j < newSubpatternSize; j++)
                        newRows[pY * newSubpatternSize + j] += p[j];
                }

                return new Art(newRows);
            }

            private IEnumerable<Pattern> Subpatterns()
            {
                var sideLength = Size / SubpatternSize;
                var count = sideLength * sideLength;

                for (var i = 0; i < count; i++)
                {
                    var pX = i % sideLength;
                    var pY = i / sideLength;

                    var newRows = new string[SubpatternSize];
                    for (var j = 0; j < SubpatternSize; j++)
                        newRows[j] = rows[pY * SubpatternSize + j].Substring(pX * SubpatternSize, SubpatternSize);

                    yield return new Pattern(newRows);
                }
            }

            public int PixelsOn() => Subpatterns().Sum(p => p.PixelsOn());
        }

        internal class Rule
        {
            public int Size => match.Size;

            private readonly Pattern match;
            private readonly Pattern result;

            public Rule(string ruleString)
            {
                var ruleArgs = ruleString.Split(" => ");
                match = new Pattern(ruleArgs[0]);
                result = new Pattern(ruleArgs[1]);
            }

            public Pattern Match(Pattern pattern) => match.Permutations().Any(perm => perm.Equals(pattern)) ? result : null;
        }

        internal class Pattern : IEquatable<Pattern>
        {
            public int Size => rows.Length;

            private readonly string[] rows;

            public Pattern(string patternString)
            {
                rows = patternString.Split('/');
            }

            public Pattern(string[] rows)
            {
                this.rows = rows;
            }

            public bool Equals(Pattern other)
            {
                if (other is null)
                    return false;
                return ReferenceEquals(this, other) || rows.SequenceEqual(other.rows);
            }

            public override bool Equals(object obj)
            {
                if (obj is null)
                    return false;
                if (ReferenceEquals(this, obj))
                    return true;
                return obj.GetType() == GetType() && Equals((Pattern)obj);
            }

            public override int GetHashCode()
            {
                var hash = 0;
                unchecked
                {
                    hash = rows.Aggregate(hash, (current, str) => (current * 397) ^ str.GetHashCode());
                }
                return hash;
            }

            public Pattern Mirror()
            {
                var newRows = rows.Select(r => new string(r.Reverse().ToArray())).ToArray();
                return new Pattern(newRows);
            }

            public Pattern Rotate()
            {
                var newRows = new string[Size];
                for (var column = Size - 1; column >= 0; column--)
                {
                    var row = "";
                    for (var rowIndex = 0; rowIndex < Size; rowIndex++)
                        row += rows[rowIndex][column];
                    newRows[Size - 1 - column] = row;
                }
                return new Pattern(newRows);
            }

            public IEnumerable<Pattern> Permutations()
            {
                var last = this;

                yield return last;
                yield return last.Mirror();

                for (var i = 0; i < 3; i++)
                {
                    last = last.Rotate();
                    yield return last;
                    yield return last.Mirror();
                }
            }

            public int PixelsOn() => rows.Select(r => r.Count(c => c == '#')).Sum();

            public string this[int rowIndex] => rows[rowIndex];

            public override string ToString() => string.Join("/", rows);
        }

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
            var rules = input.Select(line => new Rule(line)).ToList();
            var art = new Art(new [] {".#.", "..#", "###"});

            for (var i = 0; i < 5; i++)
            {
                art = art.ApplyRules(rules);
            }

            Console.WriteLine($"Pixels on: {art.PixelsOn()}");
        }

        private void SolvePart2()
        {
            var input = LoadInput();
            var rules = input.Select(line => new Rule(line)).ToList();
            var art = new Art(new[] { ".#.", "..#", "###" });

            for (var i = 0; i < 18; i++)
            {
                Console.WriteLine($"Iteration {i}");
                art = art.ApplyRules(rules);
            }

            Console.WriteLine($"Pixels on: {art.PixelsOn()}");
        }

        private string[] LoadInput() => File.ReadAllLines("input.txt");
    }
}
