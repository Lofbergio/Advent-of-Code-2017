#region

using System;
using System.IO;

#endregion

namespace Helper
{
    public static class Utilities
    {
        private static readonly string InputsFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}Inputs");

        public static string GetInput(string projectName)
        {
            var filepath = Path.Combine(InputsFolderPath, $"{projectName}.txt");

            if (!File.Exists(filepath))
                throw new FileNotFoundException($"Could not find {filepath}");

            string input;
            using (var fs = new StreamReader(filepath))
                input = fs.ReadToEnd();

            Console.WriteLine("------- Start of input -------");
            Console.WriteLine(input);
            Console.WriteLine("-------  End of input  -------");

            return input;
        }
    }
}