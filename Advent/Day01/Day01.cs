#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;
using static System.Console;

#endregion

namespace Day01
{
    internal static class Day01
    {
        [STAThread]
        private static void Main()
        {
            var input = Task.Run(async () => await Utilities.GetInput(typeof(Day01).Name)).Result;

            var part1Tests = new Dictionary<string, int>
                             {
                                 { "1122", 3 },
                                 { "1111", 4 },
                                 { "1234", 0 },
                                 { "91212129", 9 }
                             };

            if (part1Tests.Any(t => t.Key.TestResultOf(Part1) != t.Value))
                throw new Exception("Failed Part1 tests");

            var p1 = Part1(input);
            WriteLine($"Part1 answer: {p1}");
            Clipboard.SetText(p1.ToString());

            var part2Tests = new Dictionary<string, int>
                             {
                                 { "1212", 6 },
                                 { "1221", 0 },
                                 { "123425", 4 },
                                 { "123123", 12 },
                                 { "12131415", 4 }
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
            var list = input.ToCharArray().Select(c => (int)char.GetNumericValue(c)).ToList();
            list.Add(list.First()); // Copy the first entry into the back of the array to make it circular

            // Create a new array but shift it 1 step and then save the number if both arrays match on the same position
            return list.Zip(list.Skip(1), (a, b) => a == b ? a : 0).Sum();
        }

        private static int Part2(string input)
        {
            var list = input.ToCharArray().Select(c => (int)char.GetNumericValue(c)).ToList();
            var halfwayCount = list.Count()/2;
            var halfwayList = list.Concat(list.Take(halfwayCount)); // Copy the first half of the list and append it into a new list

            // And now we shift the new list 50% of the length because 
            return list.Zip(halfwayList.Skip(halfwayCount), (a, b) => a == b ? a : 0).Sum();
        }
    }
}