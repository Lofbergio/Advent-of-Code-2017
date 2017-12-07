#region

using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;
using static System.Console;

#endregion

namespace Day07
{
    internal static class Day07
    {
        [STAThread]
        private static void Main()
        {
            var input = Task.Run(async () => await Utilities.GetInput(typeof(Day07).Name)).Result;

            var part1Tests = new Dictionary<string, string>
                             {
                                 { @"pbga (66)
xhth (57)
ebii (61)
havc (66)
ktlj (57)
fwft (72) -> ktlj, cntj, xhth
qoyq (66)
padx (45) -> pbga, havc, qoyq
tknk (41) -> ugml, padx, fwft
jptl (61)
ugml (68) -> gyxo, ebii, jptl
gyxo (61)
cntj (57)", "tknk" }
                             };

            if (part1Tests.Any(t => t.Key.TestResultOf(Part1) != t.Value))
                throw new Exception("Failed Part1 tests");

            var p1 = Part1(input);

            WriteLine($"Part1 answer: {p1}");
            Clipboard.SetText(p1);

            var part2Tests = new Dictionary<string, string>
                             {
                                 { "1", "" }
                             };


            if (part2Tests.Any(t => t.Key.TestResultOf(Part2) != t.Value))
                throw new Exception("Failed Part2 tests");

            var p2 = Part2(input);
            WriteLine($"Part2 answer: {p2}");
            Clipboard.SetText(p2);

            ReadKey();
        }

        private static string Part1(string input)
        {
            var programs = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(line => new Program(line)).ToList();
            var dependable = programs.Where(p => p.HoldingUp != null);
            foreach (var dep in dependable)
            {
                if (dependable.All(p => !p.HoldingUp.Contains(dep.Name)))
                    return dep.Name;
            }
        }

        private static string Part2(string input)
        {
            return "";
        }
    }

    public class Program
    {
        public string Name { get; }
        public int Position { get; }
        public List<string> HoldingUp { get; }

        public Program(string line)
        {
            var split = line.Replace(",", "").Split(' ');
            Name = split[0];
            Position = int.Parse(split[1].Replace("(", "").Replace(")", ""));
            if (split.Length > 2)
                HoldingUp = new List<string>(split.Skip(3));
        }
    }
}