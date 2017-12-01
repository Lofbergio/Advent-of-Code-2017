#region

using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Windows.Forms;
using Helper;
using static System.Console;

#endregion

namespace Advent
{
    internal static class DayTemplate
    {
        private static void Main()
        {
            var input = Utilities.GetInput(typeof(DayTemplate).Name);

            WriteLine($"First part answer: {input}");
            Clipboard.SetText(input);

            ReadKey();
        }
    }
}