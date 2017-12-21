using System;
using System.Data;
using System.IO;
using System.Linq;

namespace Day21
{
    internal class Program
    {
        internal class Art
        {
            
        }

        internal class Rule
        {
            private Pattern match;
            private Pattern result;

            public Rule(string ruleString)
            {
                var ruleArgs = ruleString.Split(" => ");
                match = new Pattern(ruleArgs[0]);
                result = new Pattern(ruleArgs[1]);
            }

            public Pattern Match(Pattern pattern)
            {
                throw new NotImplementedException();
            }
        }

        internal class Pattern : IEquatable<Pattern>
        {
            private readonly string[] rows;

            public Pattern(string patternString)
            {
                rows = patternString.Split('/');
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
        }

        private string[] LoadInput() => File.ReadAllLines("input.txt");
    }
}
