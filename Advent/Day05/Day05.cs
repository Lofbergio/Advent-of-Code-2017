#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;
using static System.Console;

#endregion

namespace Day05
{
    internal static class Day05
    {
        [STAThread]
        private static void Main()
        {
            var input = Task.Run(async () => await Utilities.GetInput(typeof(Day05).Name)).Result;

            var part1Tests = new Dictionary<string, int>
                             {
                                 { @"0
3
0
1
-3", 5 }
                             };

            if (part1Tests.Any(t => t.Key.TestResultOf(Part1) != t.Value))
                throw new Exception("Failed Part1 tests");

            var p1 = Part1(input);
            WriteLine($"Part1 answer: {p1}");
            Clipboard.SetText(p1.ToString());

            var part2Tests = new Dictionary<string, int>
                             {
                                 { @"0
3
0
1
-3", 10 }
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
            var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            return JumpAround(lines.Select(int.Parse).ToArray());
        }

        private static int Part2(string input)
        {
            var lines = input.Split(new[] { Environment.NewLine },StringSplitOptions.RemoveEmptyEntries);

            return JumpAround(lines.Select(int.Parse).ToArray(), true);
        }

        private static int JumpAround(IList<int> instructions, bool part2 = false)
        {
            var pos = 0;
            var jumps = 0;

            while (pos < instructions.Count)
            {
                var oldPos = pos;
                pos += instructions[pos];

                if (part2 && instructions[oldPos] >= 3)
                    instructions[oldPos]--;
                else
                    instructions[oldPos]++;

                jumps++;
            }

            return jumps;
        }
    }
}