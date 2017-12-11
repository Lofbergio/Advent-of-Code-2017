#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;
using static System.Console;

#endregion

namespace Day11
{
    internal static class Day11
    {
        [STAThread]
        private static void Main()
        {
            var input = Task.Run(async () => await Utilities.GetInput(typeof(Day11).Name)).Result;

            var part1Tests = new Dictionary<string, int>
                             {
                                 { "ne,ne,ne", 3 },
                                 { "ne,ne,sw,sw", 0 },
                                 { "ne,ne,s,s", 2 },
                                 { "se,sw,se,sw,sw", 3 },
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

        private static int Part1(string input) => new Hex(input).Steps.Last().DistanceFromOrigin;

        private static int Part2(string input) => new Hex(input).Steps.Max(s => s.DistanceFromOrigin);
    }

    public class Hex
    {
        public List<Step> Steps = new List<Step> { new Step { X = 0, Y = 0, Z = 0 } };

        public Hex(string input)
        {
            Step TakeAStep(string dir)
            {
                var currentStep = new Step { X = Steps.Last().X, Y = Steps.Last().Y, Z = Steps.Last().Z };

                switch (dir)
                {
                    case "nw":
                        currentStep.X--;
                        currentStep.Y++;
                        break;
                    case "n":
                        currentStep.Y++;
                        currentStep.Z--;
                        break;
                    case "ne":
                        currentStep.X++;
                        currentStep.Z--;
                        break;

                    case "sw":
                        currentStep.X--;
                        currentStep.Z++;
                        break;
                    case "s":
                        currentStep.Y--;
                        currentStep.Z++;
                        break;
                    case "se":
                        currentStep.X++;
                        currentStep.Y--;
                        break;
                }

                return currentStep;
            }

            Steps.AddRange(input.Split(',').Select(TakeAStep));
        }
    }

    public class Step
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public override string ToString() => $"({X},{Y},{Z}) Distance:{DistanceFromOrigin}";

        public int DistanceFromOrigin => (Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z)) / 2;
    }
}