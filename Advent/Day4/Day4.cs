#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Helper;
using static System.Console;

#endregion

namespace Day4
{
    internal static class Day4
    {
        [STAThread]
        private static void Main()
        {
            var input = Utilities.GetInput(typeof(Day4).Name);

            var part1Tests = new Dictionary<string, int>
            {
                { "aa bb cc dd ee", 1 },
                { "aa bb cc dd aa", 0 },
                { "aa bb cc dd aaa", 1 }
            };

            if (part1Tests.All(t => t.Key.TestResultOf(Part1) == t.Value))
            {
                var p1 = Part1(input);

                WriteLine($"First part answer: {p1}");
                Clipboard.SetText(p1.ToString());

                var part2Tests = new Dictionary<string, int>
                                 {
                                     { "abcde fghij", 1 },
                                     { "abcde xyz ecdab", 0 },
                                     { "a ab abc abd abf abj", 1 },
                                     { "iiii oiii ooii oooi oooo", 0 },
                                     { "oiii ioii iioi iiio", 0 }
                                 };

                if (part2Tests.All(t => t.Key.TestResultOf(Part2) == t.Value))
                {
                    var p2 = Part2(input);

                    WriteLine($"First part answer: {p2}");
                    Clipboard.SetText(p2.ToString());
                }
            }

            ReadKey();
        }

        private static int Part1(string input)
        {
            var words = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Split(' '));

            // How many words are there in the paragraph? Zip that with the distinct count and sum if they match
            return words.Select(w => w.Length).Zip(words.Select(w => w.Distinct().Count()), (a,b) => a == b ? 1 : 0).Sum();
        }

        private static int Part2(string input)
        {
            var words = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Split(' '));
            
            // Same as Part1 except sort all distinct letters alphabetically
            return (from word in words
                    let sorted = word.Select(letter => letter.OrderBy(c => c).Distinct().ToArray()).Select(array => new string(array))
                    where sorted.Distinct().Count() == word.Count()
                    select word).Count();
        }
    }
}