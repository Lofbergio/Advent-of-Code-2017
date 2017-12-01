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

            var input = File.ReadAllText(filepath);

            Console.WriteLine("------- Start of input -------");
            Console.WriteLine(input);
            Console.WriteLine("-------  End of input  -------");

            return input;
        }

        public static TResult TestResultOf<TResult>(this string entry, Func<string, TResult> action) => action(entry);
    }
}