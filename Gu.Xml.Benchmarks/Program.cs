namespace Gu.Xml.Benchmarks
{
    using System;

    public class Program
    {
        //public static void Main()
        //{
        //    foreach (var summary in RunSingle<FindVsFirstOrDefault>())
        //    {
        //        CopyResult(summary);
        //    }
        //}

        //// ReSharper disable once UnusedMember.Local
        //private static IEnumerable<Summary> RunAll()
        //{
        //    var switcher = new BenchmarkSwitcher(typeof(Program).Assembly);
        //    var summaries = switcher.RunAll();
        //    return summaries;
        //}

        //private static IEnumerable<Summary> RunSingle<T>()
        //{
        //    var summaries = new[] { BenchmarkRunner.Run<T>() };
        //    return summaries;
        //}

        //private static void CopyResult(Summary summary)
        //{
        //    var sourceFileName = Directory.EnumerateFiles(summary.ResultsDirectoryPath, $"*{summary.Title}-report-github.md")
        //                                  .Single();
        //    var destinationFileName = Path.Combine(summary.ResultsDirectoryPath, "..\\..\\..\\..\\..\\", summary.Title + ".md");
        //    Console.WriteLine($"Copy: {sourceFileName} -> {destinationFileName}");
        //    File.Copy(sourceFileName, destinationFileName, overwrite: true);
        //}
    }
}
