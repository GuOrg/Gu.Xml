// ReSharper disable UnusedMember.Local
// ReSharper disable HeuristicUnreachableCode
#pragma warning disable 162
namespace Gu.Xml.Benchmarks
{
    using System;
    using System.IO;
    using System.Linq;
    using BenchmarkDotNet.Reports;
    using BenchmarkDotNet.Running;

    public class Program
    {
        public static void Main()
        {
            if (true)
            {
                CopyResult(BenchmarkRunner.Run<Collection>());
            }
            else
            {
                foreach (var summary in BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).RunAll())
                {
                    CopyResult(summary);
                }
            }
        }

        private static void CopyResult(Summary summary)
        {
            var sourceFileName = Directory.EnumerateFiles(summary.ResultsDirectoryPath, $"*{summary.Title}-report-github.md")
                                          .Single();
            var destinationFileName = Path.Combine(summary.ResultsDirectoryPath.Split(new[] { "\\bin\\" }, StringSplitOptions.RemoveEmptyEntries).First(), "Benchmarks", summary.Title.Split('.').Last() + ".md");
            Console.WriteLine($"Copy: {sourceFileName} -> {destinationFileName}");
            File.Copy(sourceFileName, destinationFileName, overwrite: true);
        }
    }
}
