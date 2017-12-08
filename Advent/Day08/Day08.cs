#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;
using static System.Console;

#endregion

namespace Day08
{
    internal static class Day08
    {
        [STAThread]
        private static void Main()
        {
            var input = Task.Run(async () => await Utilities.GetInput(typeof(Day08).Name)).Result;

            var part1Tests = new Dictionary<string, int>
                             {
                                 { @"b inc 5 if a > 1
a inc 1 if b < 5
c dec -10 if a >= 1
c inc -20 if c == 10", 1 }
                             };

            if (part1Tests.Any(t => t.Key.TestResultOf(Part1) != t.Value))
                throw new Exception("Failed Part1 tests");

            var p1 = Part1(input);

            WriteLine($"Part1 answer: {p1}");
            Clipboard.SetText(p1.ToString());

            var part2Tests = new Dictionary<string, int>
                                {
                                    { @"b inc 5 if a > 1
a inc 1 if b < 5
c dec -10 if a >= 1
c inc -20 if c == 10", 10 }
                                };


            if (part2Tests.Any(t => t.Key.TestResultOf(Part2) != t.Value))
                throw new Exception("Failed Part2 tests");

            var p2 = Part2(input);
            WriteLine($"Part2 answer: {p2}");
            Clipboard.SetText(p2.ToString());

            ReadKey();
        }

        private static int Part1(string input) => Execute(input);

        private static int Part2(string input) => Execute(input, true);

        private static int Execute(string input, bool part2 = false)
        {
            var instructions = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(line => new Instructions(line));
            var variables = new Dictionary<string, int>();

            var highest = 0;
            foreach (var instruction in instructions)
            {
                if (!variables.ContainsKey(instruction.Variable))
                    variables.Add(instruction.Variable, 0);

                if (!variables.ContainsKey(instruction.If.Variable))
                    variables.Add(instruction.If.Variable, 0);

                if (!instruction.If.Compare(variables[instruction.If.Variable]))
                    continue;

                if (instruction.Increase)
                    variables[instruction.Variable] += instruction.Amount;
                else
                    variables[instruction.Variable] -= instruction.Amount;

                if (variables[instruction.Variable] > highest)
                    highest = variables[instruction.Variable];
            }

            return part2 ? highest : variables.Max(e => e.Value);
        }
    }

    public class Instructions
    {
        public struct Condition
        {
            public string Variable;
            public string Operation;
            public int Value;

            public bool Compare(int a)
            {
                switch (Operation)
                {
                    case ">": return a > Value;
                    case ">=": return a >= Value;
                    case "<": return a < Value;
                    case "<=": return a <= Value;
                    case "==": return a == Value;
                    case "!=": return a != Value;
                    default: return false;
                }
            }

            public override string ToString() => $"if {Variable} {Operation} {Value}";
        }

        public string Variable { get; }
        public bool Increase { get; }
        public int Amount { get; }
        public Condition If { get; }


        public Instructions(string line)
        {
            var split = line.Split(' ');
            Variable = split[0];
            Increase = split[1] == "inc";
            Amount = int.Parse(split[2]);
            If = new Condition
                 {
                     Variable = split[4],
                     Operation = split[5],
                     Value = int.Parse(split[6])
                };
        }

        public override string ToString() => $"{(Increase ? "In" : "De") + "crease"} {Variable} {If}";
    }
}