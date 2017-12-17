#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;
using static System.Console;

#endregion

namespace Day16
{
    internal static class Day16
    {
        [STAThread]
        private static void Main()
        {
            var input = Task.Run(async () => await Utilities.GetInput(typeof(Day16).Name)).Result;

            var part1Tests = new Dictionary<string, string>
                             {
                                 { "s1,x3/4,pe/b", "baedc" }
                             };

            if (part1Tests.Any(t => t.Key.TestResultOf(Part1) != t.Value))
                throw new Exception("Failed Part1 tests");

            var p1 = Part1(input);

            WriteLine($"Part1 answer: {p1}");
            Clipboard.SetText(p1);

            var p2 = Part2(input);
            WriteLine($"Part2 answer: {p2}");
            Clipboard.SetText(p2);

            ReadKey();
        }

        private static string Part1(string input)
        {
            var moves = input.Split(',').ToList();
            var dancers = (input.Length > 15 ? "abcdefghijklmnop" : "abcde").ToCharArray();
            moves.ForEach(m => ExecuteMove(m, dancers));
            return new string(dancers);
        }

        private static void ExecuteMove(string move, char[] dancers)
        {
            switch (move[0])
            {
                case 's':
                    Spin(int.Parse(move.Substring(1)));
                    break;
                case 'x':
                    var exchangeSplit = move.Substring(1).Split('/').Select(int.Parse).ToArray();
                    Exchange(exchangeSplit[0], exchangeSplit[1]);
                    break;
                case 'p':
                    var partnerSplit = move.Substring(1).Split('/');
                    Partner(Convert.ToChar(partnerSplit[0]), Convert.ToChar(partnerSplit[1]));
                    break;
            }

            void Spin(int size)
            {
                for (var step = 0; step < size; step++)
                {
                    var last = dancers[dancers.Length - 1];
                    for (var i = dancers.Length - 1; i > 0; i--)
                        dancers[i] = dancers[i - 1];

                    dancers[0] = last;
                }
            }

            void Partner(char a, char b) => Exchange(Array.IndexOf(dancers, a), Array.IndexOf(dancers, b));

            void Exchange(int posA, int posB)
            {
                var a = dancers[posA];
                var b = dancers[posB];
                dancers[posA] = b;
                dancers[posB] = a;
            }
        }

        private static string Part2(string input)
        {
            var moves = input.Split(',').ToList();
            var dancers = "abcdefghijklmnop".ToCharArray();
            var permutations = new List<string>();

            var currentFormation = new string(dancers);
            for (var i = 0; i < 1_000_000_000; i++)
            {
                currentFormation = new string(dancers);
                if (permutations.Contains(currentFormation))
                    return permutations[1_000_000_000 % i];
                permutations.Add(currentFormation);
                moves.ForEach(m => ExecuteMove(m, dancers));
            }

            return currentFormation;
        }
    }
}