#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Helper;
using static System.Console;

#endregion

namespace Day3
{
    internal static class Day3
    {
        [STAThread]
        private static void Main()
        {
            var input = Utilities.GetInput(typeof(Day3).Name);

            var part1Tests = new Dictionary<string, int>
            {
                { "1", 0 },
                { "12", 3 },
                { "23", 2 },
                { "1024", 31 }
            };

            if (part1Tests.All(t => t.Key.TestResultOf(Part1) == t.Value))
            {
                var distance = Part1(input);

                WriteLine($"First part answer: {distance}");
                Clipboard.SetText(distance.ToString());

                var sum = Part2(input);

                WriteLine($"First part answer: {sum}");
                Clipboard.SetText(sum.ToString());
            }

            ReadKey();
        }

        private static int Part1(string input)
        {
            var targetNode = new Grid().FindNode(int.Parse(input)).Key;

            return Math.Abs(targetNode.X) + Math.Abs(targetNode.Y);
        }

        private static int Part2(string input) => new Grid().FindNode(int.Parse(input), true).Value;
    }

    public class Grid
    {
        private enum Direction
        {
            North,
            West,
            South,
            East
        }

        private Direction _dir = Direction.North;
        private readonly Node _currentPoint = new Node(1, 0);
        private readonly Dictionary<Node, int> _steps = new Dictionary<Node, int>
        {
            { new Node{ Step = 1, X = 0, Y = 0 }, 1 },
            { new Node{ Step = 2, X = 1, Y = 0 }, 1 }
        };

        private void HandleDirection()
        {
            switch (_dir)
            {
                case Direction.North:
                    _currentPoint.Y++;
                    break;
                case Direction.East:
                    _currentPoint.X++;
                    break;
                case Direction.South:
                    _currentPoint.Y--;
                    break;
                case Direction.West:
                    _currentPoint.X--;
                    break;
            }
        }

        public KeyValuePair<Node, int> FindNode(int target, bool searchForBigger = false)
        {
            if (target == 1) return _steps.First();

            var step = 2;
            var stepsPerDir = 2;
            var stepsInDir = 1;

            while (step < target)
            {
                if (stepsInDir == stepsPerDir)
                {
                    stepsInDir = 0;
                    _dir++;

                    if ((int)_dir > 3)
                        _dir = 0;

                    if (_dir == Direction.North)
                    {
                        _currentPoint.X++;
                        stepsPerDir = _currentPoint.X * 2;
                    }
                    else HandleDirection();
                }
                else HandleDirection();

                var sum = searchForBigger ? SumNearby() : 0;

                _steps.Add(new Node { Step = step, X = _currentPoint.X, Y = _currentPoint.Y }, sum);

                if (sum > target)
                    return _steps.Last();

                step++;
                stepsInDir++;
            }

            return _steps.Last();
        }

        private int SumNearby() => new List<int>
        {
            _steps.FirstOrDefault(entry => entry.Key.X == _currentPoint.X - 1 && entry.Key.Y == _currentPoint.Y + 1).Value,
            _steps.FirstOrDefault(entry => entry.Key.X == _currentPoint.X - 1 && entry.Key.Y == _currentPoint.Y).Value,
            _steps.FirstOrDefault(entry => entry.Key.X == _currentPoint.X - 1 && entry.Key.Y == _currentPoint.Y - 1).Value,

            _steps.FirstOrDefault(entry => entry.Key.X == _currentPoint.X && entry.Key.Y == _currentPoint.Y + 1).Value,
            _steps.FirstOrDefault(entry => entry.Key.X == _currentPoint.X && entry.Key.Y == _currentPoint.Y - 1).Value,

            _steps.FirstOrDefault(entry => entry.Key.X == _currentPoint.X + 1 && entry.Key.Y == _currentPoint.Y + 1).Value,
            _steps.FirstOrDefault(entry => entry.Key.X == _currentPoint.X + 1 && entry.Key.Y == _currentPoint.Y).Value,
            _steps.FirstOrDefault(entry => entry.Key.X == _currentPoint.X + 1 && entry.Key.Y == _currentPoint.Y - 1).Value,
        }.Sum();
    }

    public class Node
    {
        public Node(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Node() { }

        public int X { get; set; }
        public int Y { get; set; }
        public int Step { get; set; }

        public override string ToString() => $"#{Step} ({X},{Y})";
    }
}