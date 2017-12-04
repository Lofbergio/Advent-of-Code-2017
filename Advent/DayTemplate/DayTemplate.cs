#region

using System;
using System.Collections.Generic;
using System.Linq;
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
            var input = Utilities.GetInput(typeof (DayTemplate).Name);

            var part1Tests = new Dictionary<string, int>
                             {
                                 { "1", 2 }
                             };

            if (part1Tests.All(t => t.Key.TestResultOf(Part1) == t.Value))
            {
                var p1 = Part1(input);

                WriteLine($"First part answer: {p1}");
                Clipboard.SetText(p1.ToString());

                var part2Tests = new Dictionary<string, int>
                                 {
                                     { "1", 2 }
                                 };

                if (part2Tests.All(t => t.Key.TestResultOf(Part2) == t.Value))
                {
                    var p2 = Part2(input);

                    WriteLine($"Second part answer: {p2}");
                    Clipboard.SetText(p2.ToString());
                }
            }

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