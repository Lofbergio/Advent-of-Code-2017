#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;
using static System.Console;

#endregion

namespace Day17
{
    internal static class Day17
    {
        [STAThread]
        private static void Main()
        {
            var input = Task.Run(async () => await Utilities.GetInput(typeof(Day17).Name)).Result;

            var part1Tests = new Dictionary<string, int>
                             {
                                 { "3", 638 }
                             };

            if (part1Tests.Any(t => t.Key.TestResultOf(Part1) != t.Value))
                throw new Exception("Failed Part1 tests");

            var p1 = Part1(input);

            WriteLine($"Part1 answer: {p1}");
            Clipboard.SetText(p1.ToString());

            var p2 = Part2(input);
            WriteLine($"Part2 answer: {p2}");
            Clipboard.SetText(p2.ToString());

            ReadKey();
        }

        private static int Part1(string input)
        {
            var stepSize = int.Parse(input);
            var buffer = new List<int>{0};
            var curPos = 0;
            for (var i = 0; i < 2017; i++)
            {
                var newPos = (curPos + stepSize) % buffer.Count + 1;
                buffer.Insert(newPos, i + 1);
                curPos = newPos;
            }

            return buffer[curPos+1];
        }

        private static int Part2(string input)
        {
            var stepSize = int.Parse(input);
            var position = 0;
            var num = 0;
            for (var i = 1; i <= 50_000_000; i++)
            {
                position = (position + stepSize) % i;
                if (position == 0)
                    num =  i;
                position++;
            }

            return num;
        }
    }
}