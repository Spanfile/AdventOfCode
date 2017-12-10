using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Day4
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
            var valids = input.Where(phrase =>
            {
                var words = phrase.Split(' ');
                return words.SequenceEqual(words.Distinct());
            }).Count();
            Console.WriteLine(valids);
        }

        private void SolvePart2()
        {
            var input = LoadInput();
            var valids = input.Where(phrase =>
            {
                var wordSets = phrase.Split(' ').Select(word => word.ToHashSet()).ToList();
                return !(from s1 in wordSets from s2 in wordSets where !s1.Equals(s2) where s1.SetEquals(s2) select s1).Any();
            }).Count();
            Console.WriteLine(valids);
        }

        private string[] LoadInput() => File.ReadAllLines("input.txt");
        private string[] SampleInput1() => new[] {"aa bb cc dd ee"};
        private string[] SampleInput2() => new[] {"aa bb cc dd aa"};
        private string[] SampleInput3() => new[] {"aa bb cc dd aaa"};

        private string[] SampleInput4() => new[] {"abcde fghij"};
        private string[] SampleInput5() => new[] { "abcde ecdab" };
        private string[] SampleInput6() => new[] { "a ab abc abd abf abj" };
        private string[] SampleInput7() => new[] {"iiii oiii ooii oooi oooo"};
        private string[] SampleInput8() => new[] {"oiii ioii iioi iiio"};
    }
}
