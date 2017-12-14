using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Puzzles
{
    internal class Day01 : Puzzle<string>
    {
        [Input("1122", "3")]
        [Input("1111", "4")]
        [Input("1234", "0")]
        [Input("91212129", "9")]
        public override string SolvePart1<TOutput>(string input) => input
            .Select((c, i) => (int.Parse(c.ToString()), int.Parse(input[(i + 1) % input.Length].ToString())))
            .Where(pair => pair.Item1 == pair.Item2).Sum(pair => pair.Item1).ToString();

        [Input("1212", "6")]
        [Input("1221", "0")]
        [Input("123425", "4")]
        [Input("123123", "12")]
        [Input("12131415", "4")]
        public override string SolvePart2<TOutput>(string input) => input
            .Select((c, i) =>
                (int.Parse(c.ToString()), int.Parse(input[(i + input.Length / 2) % input.Length].ToString())))
            .Where(pair => pair.Item1 == pair.Item2).Sum(pair => pair.Item1).ToString();
    }
}
