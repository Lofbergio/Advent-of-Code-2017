#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;
using static System.Console;

#endregion

namespace Day10
{
    internal static class Day10
    {
        [STAThread]
        private static void Main()
        {
            var input = Task.Run(async () => await Utilities.GetInput(typeof(Day10).Name)).Result;

            var part1Tests = new Dictionary<string, int>
                             {
                                 { "3,4,1,5", 12 }
                             };

            if (part1Tests.Any(t => t.Key.TestResultOf(Part1) != t.Value))
                throw new Exception("Failed Part1 tests");

            var p1 = Part1(input);

            WriteLine($"Part1 answer: {p1}");
            Clipboard.SetText(p1.ToString());
            
            var p2 = Part2(input);
            WriteLine($"Part2 answer: {p2}");
            Clipboard.SetText(p2);

            ReadKey();
        }

        private static int Part1(string input)
        {
            var processedList = ProcessList(input);
            return processedList[0] * processedList[1];
        }

        private static string Part2(string input)
        {
            var processedList = ProcessList(input, true);

            var hashes = new List<int>();

            for (var x = 0; x < processedList.Count; x += 16)
            {
                var hash = 0;

                for (var i = 0; i < 16; i++)
                    hash ^= processedList[x + i];

                hashes.Add(hash);
            }

            return string.Join("", hashes.Select(x => x.ToString("X").PadLeft(2, '0'))).ToLower();
        }

        private static List<int> ProcessList(string input, bool part2 = false)
        {
            var lengths = input.Split(',').Select(int.Parse).ToList();

            if (part2)
                lengths = input.Select(c => (int)c).ToList().Concat(new[] { 17, 31, 73, 47, 23 }).ToList();

            var list = Enumerable.Range(0, lengths.Count == 4 ? 5 : 256).ToList();
            var currentPos = 0;
            var skipSize = 0;

            void Reverse(int length)
            {
                var subList = new int[length];

                for (var i = 0; i < length; i++)
                    subList[i] = list[(currentPos + i) % list.Count];

                subList = subList.Reverse().ToArray();

                for (var i = 0; i < length; i++)
                    list[(currentPos + i) % list.Count] = subList[i];
            }

            for (var round = 0; round < (part2 ? 64 : 1); round++)
            {
                foreach (var length in lengths)
                {
                    Reverse(length);
                    currentPos += length + skipSize++;
                    currentPos %= list.Count;
                }
            }

            return list;
        }
    }
}