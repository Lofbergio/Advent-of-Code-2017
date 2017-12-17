#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;
using static System.Console;

#endregion

namespace Day15
{
    internal static class Day15
    {
        [STAThread]
        private static void Main()
        {
            var input = Task.Run(async () => await Utilities.GetInput(typeof(Day15).Name)).Result;

            var part1Tests = new Dictionary<string, int>
                             {
                                 { @"Generator A starts with 65
Generator B starts with 8921", 588 }
                             };

            if (part1Tests.Any(t => t.Key.TestResultOf(Part1) != t.Value))
                throw new Exception("Failed Part1 tests");

            var p1 = Part1(input);

            WriteLine($"Part1 answer: {p1}");
            Clipboard.SetText(p1.ToString());

            var p2 = Part2(input);
            WriteLine($"Part2 answer: {p2}");
            Clipboard.SetText(p2.ToString());

            ReadKey();
        }

        private static int Part1(string input) => JudgeValues(input);

        private static int Part2(string input) => JudgeValues(input, true);

        private static int JudgeValues(string input, bool part2 = false)
        {
            var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var a = long.Parse(lines[0].Split(' ').Last());
            var b = long.Parse(lines[1].Split(' ').Last());

            var matchCount = 0;
            var aFactor = 16807;
            var bFactor = 48271;
            var divisor = 2147483647;

            for (var i = 0; i < (part2 ? 5000000 : 40000000); ++i)
            {
                if (part2)
                {
                    do
                        a = a * aFactor % divisor;
                    while (a % 4 != 0);

                    do
                        b = b * bFactor % divisor;
                    while (b % 8 != 0);
                }
                else
                {
                    a = a * aFactor % divisor;
                    b = b * bFactor % divisor;
                }

                if ((a & 0xFFFF) == (b & 0xFFFF))
                    matchCount++;
            }

            return matchCount;
        }
    }
}