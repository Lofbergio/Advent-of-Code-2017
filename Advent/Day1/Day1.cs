#region

using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Windows.Forms;
using Helper;
using static System.Console;

#endregion

namespace Day1
{
    internal static class Day1
    {
        private static void Main()
        {
            var input = Utilities.GetInput(typeof(Day1).Name);

            var test1 = "1122".TestInput(Solve) == 3;


            WriteLine($"First part answer: {input}");
            Clipboard.SetText(input);

            ReadKey();
        }

        private static int Solve(string entry)
        {

            return -1;
        }
    }
}