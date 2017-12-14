#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;
using static System.Console;

#endregion

namespace Day14
{
    internal static class Day14
    {
        [STAThread]
        private static void Main()
        {
            var input = Task.Run(async () => await Utilities.GetInput(typeof(Day14).Name)).Result;

            var part1Tests = new Dictionary<string, int>
                             {
                                 { "flqrgnkx", 8108 }
                             };

            if (part1Tests.Any(t => t.Key.TestResultOf(Part1) != t.Value))
                throw new Exception("Failed Part1 tests");

            var p1 = Part1(input);

            WriteLine($"Part1 answer: {p1}");
            Clipboard.SetText(p1.ToString());

            var part2Tests = new Dictionary<string, int>
                                {
                                    { "flqrgnkx", 1242 }
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
            var sum = 0;

            for (int i = 0; i < 128; i++)
            {
                var binary = ConvertHexToBinary(Day10.Day10.Part2($"{input}-{i}"));
                sum += binary.Count(x => x == '1');
            }

            return sum;
        }

        private static string ConvertHexToBinary(string hex) => string.Join(string.Empty, hex.Select(c => int.Parse(c.ToString(), System.Globalization.NumberStyles.HexNumber)).Select(value => Convert.ToString(value, 2).PadLeft(4, '0')));

        private static int Part2(string input)
        {
            var grid = new bool[128, 128];
            for (var row = 0; row < 128; row++)
            {
                var binary = ConvertHexToBinary(Day10.Day10.Part2($"{input}-{row}"));

                for (var col = 0; col < 128; col++)
                {
                    grid[row, col] = binary[col] == '1';
                }
            }

            void DimRegion(Point point)
            {
                grid[point.X, point.Y] = false;

                foreach (var neightbour in point.GetNeighbourLocations().Where(neightbour => grid[neightbour.X, neightbour.Y]))
                    DimRegion(neightbour);
            }

            var regionsDimmed = 0;
            for (var x = 0; x < 128; x++)
            {
                for (var y = 0; y < 128; y++)
                {
                    if (!grid[x, y])
                        continue;

                    DimRegion(new Point(x, y));

                    regionsDimmed++;
                }
            }

            return regionsDimmed;
        }
    }

    internal class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public IEnumerable<Point> GetNeighbourLocations()
        {
            var locations = new List<Point>();

            var left = new Point(X - 1, Y);
            if(left.X >= 0)
                locations.Add(left);

            var right = new Point(X + 1, Y);
            if(right.X < 128)
                locations.Add(right);

            var below = new Point(X, Y - 1);
            if(below.Y >= 0)
                locations.Add(below);

            var above = new Point(X, Y + 1);
            if(above.Y < 128)
                locations.Add(above);
            
            return locations;
        }
    }
}