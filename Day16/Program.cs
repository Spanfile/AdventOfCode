using System;
using System.IO;
using System.Linq;

namespace Day16
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
            var input = SampleInput1();
            var programs = "ABCDE".Select(c => c.ToString()).ToList();

            string[] args;
            foreach (var move in input.Split(','))
            {
                switch (move[0])
                {
                    case 's':
                        var amount = int.Parse(move.Substring(1));
                        var sublist = programs.Take(programs.Count - amount).ToList();
                        programs.RemoveRange(programs.Count, amount);
                        programs.InsertRange(0, sublist);
                        break;
                        
                    case 'x':
                        args = move.Substring(1).Split('/');
                        var indexA = int.Parse(args[0]);
                        var indexB = int.Parse(args[1]);
                        var temp = programs[indexA];
                        programs[indexA] = programs[indexB];
                        programs[indexB] = temp;
                        break;
                        
                    case 'p':
                        args = move.Substring(1).Split('/');
                        var progAIndex = programs.IndexOf(args[0]);
                        var progBIndex = programs.IndexOf(args[1]);
                        programs[progAIndex] = args[1];
                        programs[progBIndex] = args[0];
                        break;
                }
            }
            
            Console.WriteLine($"Final: {programs.Aggregate((s1, s2) => s1 + s2)}");
        }

        private string LoadInput() => File.ReadAllText("input.txt");
        private string SampleInput1() => "s1,x3/4,pe/b";
    }
}