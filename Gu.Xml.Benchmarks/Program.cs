﻿// ReSharper disable UnusedMember.Local
namespace Gu.Xml.Benchmarks
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using BenchmarkDotNet.Reports;
    using BenchmarkDotNet.Running;

    public class Program
    {
        public static void Main()
        {
            foreach (var summary in RunSingle<MemoizeEnumToString>())
            {
                CopyResult(summary);
            }
        }

        private static IEnumerable<Summary> RunAll()
        {
            var switcher = new BenchmarkSwitcher(typeof(Program).Assembly);
            var summaries = switcher.RunAll();
            return summaries;
        }

        private static IEnumerable<Summary> RunSingle<T>()
        {
            var summaries = new[] { BenchmarkRunner.Run<T>() };
            return summaries;
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
