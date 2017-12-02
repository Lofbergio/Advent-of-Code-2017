#region

using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Helper;
using static System.Console;

#endregion

namespace Day2
{
    internal static class Day2
    {
        [STAThread]
        private static void Main()
        {
            var input = Utilities.GetInput(typeof (Day2).Name);

            var part1Tests = new Dictionary<string, int>
                             {
                                 { @"5 1 9 5
7 5 3
2 4 6 8", 18 }
                             };

            if (part1Tests.All(t => t.Key.TestResultOf(Part1) == t.Value))
            {
                var sum = Part1(input);

                WriteLine($"First part answer: {sum}");
                Clipboard.SetText(sum.ToString());
            }

            var part2Tests = new Dictionary<string, int>
                             {
                                 { @"5 9 2 8
9 4 7 3
3 8 6 5", 9 }
                             };

            if (part2Tests.All(t => t.Key.TestResultOf(Part2) == t.Value))
            {
                var sum = Part2(input);

                WriteLine($"First part answer: {sum}");
                Clipboard.SetText(sum.ToString());
            }

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