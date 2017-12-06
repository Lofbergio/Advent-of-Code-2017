#region

using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

#endregion

namespace Helper
{
    public static class Utilities
    {
        private static readonly string SolutionFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}");

        public static async Task<string> GetInput(string projectName)
        {
            var inputsDirectory = Path.Combine(SolutionFolderPath, "Inputs");
            if (!Directory.Exists(inputsDirectory))
                Directory.CreateDirectory(inputsDirectory);

            var filepath = Path.Combine(inputsDirectory, $"{projectName}.txt");

            if (!File.Exists(filepath))
                await DownloadInput(filepath, projectName);

            var input = File.ReadAllText(filepath);

            Console.WriteLine("------- Start of input -------");
            Console.WriteLine(input);
            Console.WriteLine("-------  End of input  -------");

            return input;
        }

        public static TResult TestResultOf<TResult>(this string entry, Func<string, TResult> action) => action(entry);

        public static async Task DownloadInput(string saveFilePath, string projectName)
        {
            var tokenPath = Path.Combine(SolutionFolderPath, $"session.token");
            if (!File.Exists(tokenPath))
            {
                using (var fs = File.CreateText(tokenPath))
                    fs.Write("REPLACE THIS TEXT WITH SESSION COOKIE VALUE");

                Process.Start(tokenPath);
                Environment.Exit(0);
            }

            var day = int.Parse(Regex.Replace(projectName, @"\D+", ""));

            var cookies = new CookieContainer();
            var handler = new HttpClientHandler
                          {
                              CookieContainer = cookies,
                              UseCookies = true
                          };

            cookies.Add(new Uri("http://adventofcode.com"), new Cookie("session", File.ReadAllText(tokenPath))
                                                            {
                                                                Domain = "adventofcode.com",
                                                                Path = "/"
                                                            });
                using (var client = new HttpClient(handler))
                {
                    var response = await client.GetAsync($"http://adventofcode.com/2017/day/{day}/input");
                    if (!response.IsSuccessStatusCode)
                    {
                        switch (response.StatusCode)
                        {
                            case HttpStatusCode.NotFound:
                                Console.WriteLine("The challenge data could not be found. Did you request a valid day?");
                                break;
                            default:
                                Console.WriteLine("The request could not be completed successfully. Did you supply a valid session token?");
                                break;
                        }
                        throw new IOException(response.ReasonPhrase);
                    }
                    var content = await response.Content.ReadAsStringAsync();

                    // Web content always contains an extra newline at the end
                    using (var fs = File.CreateText(saveFilePath))
                        fs.Write(content.TrimEnd('\n'));
                }
        }
    }
}