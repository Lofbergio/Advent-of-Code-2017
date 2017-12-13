#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;
using static System.Console;

#endregion

namespace Day13
{
    internal static class Day13
    {
        [STAThread]
        private static void Main()
        {
            var input = Task.Run(async () => await Utilities.GetInput(typeof(Day13).Name)).Result;

            var part1Tests = new Dictionary<string, int>
                             {
                                 { @"0: 3
1: 2
4: 4
6: 4", 24 }
                             };

            if (part1Tests.Any(t => t.Key.TestResultOf(Part1) != t.Value))
                throw new Exception("Failed Part1 tests");

            var p1 = Part1(input);

            WriteLine($"Part1 answer: {p1}");
            Clipboard.SetText(p1.ToString());

            var part2Tests = new Dictionary<string, int>
                                {
                                 { @"0: 3
1: 2
4: 4
6: 4", 10 }
                                };


            if (part2Tests.Any(t => t.Key.TestResultOf(Part2) != t.Value))
                throw new Exception("Failed Part2 tests");

            var p2 = Part2(input);
            WriteLine($"Part2 answer: {p2}");
            Clipboard.SetText(p2.ToString());

            ReadKey();
        }

        private static int Part1(string input) => new LayerManager(input).GetSeverity;

        private static int Part2(string input)
        {
            var layerMgr = new LayerManager(input);

            var delay = 0;
            while (layerMgr.Layers.Any(l => l.IsBusted(delay)))
                delay++;

            return delay;
        }
    }

    public class LayerManager
    {
        public IEnumerable<Layer> Layers { get; }

        public LayerManager(string input)
        {
            var lines = input.Replace(":", "").Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            Layers = lines.Select(line => new Layer(line));
        }

        public int GetSeverity => Layers.Where(l => l.IsBusted()).Sum(l => l.Depth * l.Range);
    }

    public class Layer
    {
        public int Depth { get; }
        public int Range { get; }
        private int RangeMultiplier => (Range - 1) * 2;

        public bool IsBusted(int delay = 0) => ((Depth + delay) % RangeMultiplier) == 0;

        public Layer(string line)
        {
            var digits = line.Split(' ').Select(int.Parse).ToArray();

            Depth = digits[0];
            Range = digits[1];
        }
    }
}