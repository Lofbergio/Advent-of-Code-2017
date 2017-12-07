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

            var part2Tests = new Dictionary<string, int>
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
cntj (57)", 60 }
                             };


            if (part2Tests.Any(t => t.Key.TestResultOf(Part2) != t.Value))
                throw new Exception("Failed Part2 tests");

            var p2 = Part2(input);
            WriteLine($"Part2 answer: {p2}");
            Clipboard.SetText(p2.ToString());

            ReadKey();
        }

        private static string Part1(string input) => new ProgramManager(input).Root.Name;

        private static int Part2(string input) => new ProgramManager(input).OverweightProgram.Item1;
    }



    public class ProgramManager
    {
        private readonly List<Program> _programs;

        public readonly Program Root;
        public Tuple<int, Program> OverweightProgram;

        public ProgramManager(string input)
        {
            var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            _programs = lines.Select(line => new Program(line)).ToList();

            var parents = _programs.Where(p => p.ChildrenNames != null).ToList();

            // Just find the only program that has children but is not a child of any other program
            Root =  parents.First(dep => parents.All(p => p.ChildrenNames.All(c => c != dep.Name)));
            
            FillChildren(Root);
        }

        private Program FillChildren(Program parent)
        {
            if (parent.ChildrenNames == null)
                return parent;

            foreach (var name in parent.ChildrenNames)
            {
                if (parent.Children == null)
                    parent.Children = new List<Program>();

                parent.Children.Add(FillChildren(_programs.First(c => c.Name == name)));
            }

            if (parent.Children == null || OverweightProgram != null)
                return parent;

            var totals = parent.Children.Select(c => c.TotalWeight).ToList();
            if (totals.Max() == totals.Min())
                return parent;

            var overWeightProgram = parent.Children[totals.IndexOf(totals.Max())];
            OverweightProgram = new Tuple<int, Program>(overWeightProgram.Weight - (totals.Max() - totals.Min()), overWeightProgram);

            return parent;
        }
    }

    public class Program
    {
        public string Name { get; }
        public int Weight { get; }
        public List<Program> Children { get; set; }
        public int TotalWeight => Children?.Sum(c => c.TotalWeight) + Weight ?? Weight;

        public readonly IEnumerable<string> ChildrenNames;

        public Program(string line)
        {
            var split = line.Replace(",", "").Split(' ');
            Name = split[0];
            Weight = int.Parse(Regex.Replace(split[1], @"\s+|\(|\)", ""));
            if (split.Length > 2)
                ChildrenNames = split.Skip(3);
        }

        public override string ToString()
        {
            return $"#{Name} ({Weight}){(Children != null ? $" Total:{TotalWeight}" : "")}";
        }
    }
}