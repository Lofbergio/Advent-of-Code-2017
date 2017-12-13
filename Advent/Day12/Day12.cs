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

namespace Day12
{
    internal static class Day12
    {
        [STAThread]
        private static void Main()
        {
            var input = Task.Run(async () => await Utilities.GetInput(typeof(Day12).Name)).Result;

            var p1 = Part1(input);

            WriteLine($"Part1 answer: {p1}");
            Clipboard.SetText(p1.ToString());

            var part2Tests = new Dictionary<string, int>
                                {
                                    { @"0 <-> 2
1 <-> 1
2 <-> 0, 3, 4
3 <-> 2, 4
4 <-> 2, 3, 6
5 <-> 6
6 <-> 4, 5", 2 }
                                };


            if (part2Tests.Any(t => t.Key.TestResultOf(Part2) != t.Value))
                throw new Exception("Failed Part2 tests");

            var p2 = Part2(input);
            WriteLine($"Part2 answer: {p2}");
            Clipboard.SetText(p2.ToString());

            ReadKey();
        }

        private static int Part1(string input) => new ProgramManager(input).Roots.First().ConnectedWithCount;

        private static int Part2(string input) => new ProgramManager(input).Roots.Count;
    }

    public class ProgramManager
    {
        private readonly HashSet<Program> _avaiablePrograms = new HashSet<Program>();
        public readonly List<Program> Roots = new List<Program>();
        
        public ProgramManager(string input)
        {
            var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            lines.Select(line => new Program(line)).ToList().ForEach(p => _avaiablePrograms.Add(p));

            // Since we cant loop a collection that is going to be changed we have to clone it
            var clone = _avaiablePrograms.ToList();
            foreach (var potentialParent in clone)
            {
                var parent = _avaiablePrograms.FirstOrDefault(p => p.Id == potentialParent.Id);
                if (parent == null)
                    continue;

                _avaiablePrograms.Remove(parent);
                Roots.Add(Connect(parent));
            }
        }

        private Program Connect(Program parent)
        {
            if (!parent.RelatedIds.Any())
                return parent;

            foreach (var id in parent.RelatedIds)
            {
                var child = _avaiablePrograms.FirstOrDefault(p => p.Id == id);
                if (child == null)
                    continue;

                _avaiablePrograms.Remove(child);
                parent.Connected.Add(Connect(child));
            }

            return parent;
        }
    }

    public class Program
    {
        public int Id { get; }
        public readonly IEnumerable<int> RelatedIds;

        public List<Program> Connected { get; } = new List<Program>();
        public int ConnectedWithCount => Connected.Sum(c => c.ConnectedWithCount) + 1;

        public Program(string line)
        {
            var split = line.Replace(",", "").Split(' ');
            Id = int.Parse(split[0]);
            RelatedIds = split.Skip(2).Select(int.Parse);
        }

        public override string ToString()
        {
            return $"#{Id}";
        }
    }
}