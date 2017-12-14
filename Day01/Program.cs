using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Day1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            new Program().SolvePart2();
            Console.ReadKey();
        }

        private Program()
        {
            
        }

        private void SolvePart1()
        {
            var input = LoadInput();

            var captcha = input.Select((c, i) => (int.Parse(c.ToString()), int.Parse(input[(i + 1) % input.Length].ToString()))).Where(pair => pair.Item1 == pair.Item2).Sum(pair => pair.Item1);
            Console.WriteLine(captcha);
        }

        private void SolvePart2()
        {
            var input = LoadInput();

            var captcha = input.Select((c, i) => (int.Parse(c.ToString()), int.Parse(input[(i + input.Length / 2) % input.Length].ToString()))).Where(pair => pair.Item1 == pair.Item2).Sum(pair => pair.Item1);
            Console.WriteLine(captcha);
        }

        private string LoadInput() => File.ReadAllText("input.txt");
        private string SampleInput1() => "1122";
        private string SampleInput2() => "1111";
        private string SampleInput3() => "1234";
        private string SampleInput4() => "91212129";
        private string SampleInput5() => "1212";
        private string SampleInput6() => "1221";
        private string SampleInput7() => "123425";
        private string SampleInput8() => "123123";
        private string SampleInput9() => "12131415";
    }
}
