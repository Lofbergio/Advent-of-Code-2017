#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;
using static System.Console;

#endregion

namespace Advent
{
    internal static class DayTemplate
    {
        [STAThread]
        private static void Main()
        {
            var input = Task.Run(async () => await Utilities.GetInput(typeof(DayTemplate).Name)).Result;

            var part1Tests = new Dictionary<string, int>
                             {
                                 { "1", 2 }
                             };

            if (part1Tests.Any(t => t.Key.TestResultOf(Part1) != t.Value))
                throw new Exception("Failed Part1 tests");

            var p1 = Part1(input);

            WriteLine($"Part1 answer: {p1}");
            Clipboard.SetText(p1.ToString());

            var part2Tests = new Dictionary<string, int>
                                {
                                    { "1", 2 }
                                };


            if (part2Tests.Any(t => t.Key.TestResultOf(Part2) != t.Value))
                throw new Exception("Failed Part2 tests");

            var p2 = Part2(input);
            WriteLine($"Part2 answer: {p2}");
            Clipboard.SetText(p2.ToString());

            ReadKey();
        }

        private static int Part1(string input)
        {
            return 0;
        }

        private static int Part2(string input)
        {
            return 0;
        }
    }
}