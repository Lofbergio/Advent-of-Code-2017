#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Helper;
using static System.Console;

#endregion

namespace Day6
{
    internal static class Day6
    {
        [STAThread]
        private static void Main()
        {
            var input = Utilities.GetInput(typeof (Day6).Name);

            var part1Tests = new Dictionary<string, int>
                             {
                                 { @"0	2	7	0", 5 }
                             };

            if (part1Tests.Any(t => t.Key.TestResultOf(Part1) != t.Value))
                throw new Exception("Failed Part1 tests");

            var p1 = Part1(input);
            WriteLine($"Part1 answer: {p1}");
            Clipboard.SetText(p1.ToString());

            var part2Tests = new Dictionary<string, int>
                             {
                                 { @"0	2	7	0", 4 }
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
            var banks = input.Split('\t').Select(int.Parse).ToArray();
            var cycles = Redistribute(banks);

            return cycles.Count;
        }

        private static int Part2(string input)
        {
            var banks = input.Split('\t').Select(int.Parse).ToArray();
            var cycles = Redistribute(banks);

            var twinIndex = cycles.IndexOf(cycles.First(b => b.SequenceEqual(banks)));
            var cyclesBetweenTwins = cycles.Count - twinIndex;

            return cyclesBetweenTwins;
        }

        private static List<int[]> Redistribute(IList<int> banks)
        {
            var cycles = new List<int[]>();

            while (!cycles.Any(b => b.SequenceEqual(banks)))
            {
                // Note down the banks before we start since we mess with the original banks variable
                cycles.Add(banks.ToArray());

                var targetIndex = banks.IndexOf(banks.Max());
                var redistributionBlocks = banks[targetIndex];

                banks[targetIndex++] = 0;

                while (redistributionBlocks > 0)
                {
                    if (targetIndex >= banks.Count)
                        targetIndex = 0;

                    banks[targetIndex++]++;
                    redistributionBlocks--;
                }
            }

            return cycles;
        }
    }
}