#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;
using static System.Console;

#endregion

namespace Day09
{
    internal static class Day09
    {
        [STAThread]
        private static void Main()
        {
            var input = Task.Run(async () => await Utilities.GetInput(typeof(Day09).Name)).Result;

            var part1Tests = new Dictionary<string, int>
                             {
                                 { "{}", 1 },
                                 { "{{{}}}", 6 },
                                 { "{{},{}}", 5 },
                                 { "{{{},{},{{}}}}", 16 },
                                 { "{<a>,<a>,<a>,<a>}", 1 },
                                 { "{{<ab>},{<ab>},{<ab>},{<ab>}}", 9 },
                                 { "{{<!!>},{<!!>},{<!!>},{<!!>}}", 9 },
                                 { "{{<a!>},{<a!>},{<a!>},{<ab>}}", 3 }
                             };

            if (part1Tests.Any(t => t.Key.TestResultOf(Part1) != t.Value))
                throw new Exception("Failed Part1 tests");

            var p1 = Part1(input);

            WriteLine($"Part1 answer: {p1}");
            Clipboard.SetText(p1.ToString());

            var part2Tests = new Dictionary<string, int>
                             {
                                 { "<>", 0 },
                                 { "<random characters>", 17 },
                                 { "<<<<>", 3 },
                                 { "<{!>}>", 2 },
                                 { "<!!>", 0 },
                                 { "<!!!>>", 0 },
                                 { "<{o\"i!a,<{i<a>", 10 }
                             };


            if (part2Tests.Any(t => t.Key.TestResultOf(Part2) != t.Value))
                throw new Exception("Failed Part2 tests");

            var p2 = Part2(input);
            WriteLine($"Part2 answer: {p2}");
            Clipboard.SetText(p2.ToString());

            ReadKey();
        }

        private static int Part1(string input) => ObserveGarbage(input);

        private static int Part2(string input) => ObserveGarbage(input, true);

        private static int ObserveGarbage(string input, bool part2 = false)
        {
            var score = 0;
            var charsInGarbage = 0;
            var currentDepth = 0;
            var withinGarbage = false;
            var skipNext = false;

            foreach (var character in input)
            {
                if (skipNext)
                {
                    skipNext = false;
                    continue;
                }

                if (withinGarbage)
                    switch (character)
                    {
                        case '!':
                            skipNext = true;
                            continue;
                        case '>':
                            withinGarbage = false;
                            continue;
                        default:
                            charsInGarbage++;
                            continue;
                    }

                switch (character)
                {
                    case '{':
                        currentDepth++;
                        break;
                    case '}':
                        score += currentDepth--;
                        break;
                    case '<':
                        withinGarbage = true;
                        break;
                }
            }


            return part2 ? charsInGarbage : score;
        }
    }
}