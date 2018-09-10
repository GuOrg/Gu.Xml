namespace Gu.Xml.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using NUnit.Framework;

    public class Dump
    {
        public static void Xml(string text)
        {
            using (var reader = new StringReader(text))
            {
                while (reader.ReadLine() is string line)
                {
                    Console.WriteLine($"\"{line.Replace("\"", "\\\"")}\"" + " + Environment.NewLine +");
                }
            }
        }

        [Explicit("Script")]
        [TestCase(typeof(IEnumerable<>))]
        [TestCase(typeof(Queue<>))]
        [TestCase(typeof(HashSet<>))]
        [TestCase(typeof(ConcurrentDictionary<,>))]
        public void MarkdownList(Type source)
        {
            foreach (var type in source.Assembly
                                       .GetExportedTypes()
                                       .Where(x => typeof(IEnumerable).IsAssignableFrom(x) &&
                                                   !x.IsInterface &&
                                                   !x.IsAbstract &&
                                                   x.Namespace == source.Namespace))
            {
                Console.WriteLine("- [ ] " + type);
            }
        }
    }
}
