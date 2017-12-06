#region

using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Helper;
using static System.Console;
using System.Threading.Tasks;

#endregion

namespace Day02
{
    internal static class Day02
    {
        [STAThread]
        private static void Main()
        {
            var input = Task.Run(async () => await Utilities.GetInput(typeof(Day02).Name)).Result;

            var part1Tests = new Dictionary<string, int>
                             {
                                 { @"5 1 9 5
7 5 3
2 4 6 8", 18 }
                             };

            if (part1Tests.Any(t => t.Key.TestResultOf(Part1) != t.Value))
                throw new Exception("Failed Part1 tests");

            var p1 = Part1(input);
            WriteLine($"Part1 answer: {p1}");
            Clipboard.SetText(p1.ToString());

            var part2Tests = new Dictionary<string, int>
                             {
                                 { @"5 9 2 8
9 4 7 3
3 8 6 5", 9 }
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
            var lines = input.Split(new [] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            // Use Regex to split the string into digital numbers list, get the difference between the highest and the lowest number in each line and return the total sum.
            return lines.Select(line => Regex.Split(line, @"\D+").Select(int.Parse).ToList()).Select(numbers => numbers.Max() - numbers.Min()).Sum();
        }

        private static int Part2(string input)
        {
            var lines = input.Split(new [] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var sum = 0;
            foreach (var line in lines)
            {
                var numbers = Regex.Split(line, @"\D+").Select(int.Parse).ToList();
                numbers.ForEach(n1 =>
                    numbers.ForEach(n2 =>
                        {
                            if (n1 != n2 && n1 % n2 == 0)
                                sum += n1 / n2;
                        }));
            }

            return sum;
        }
    }
}