// ReSharper disable UnusedMember.Global
namespace Gu.Xml.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.IO;
    using System.Linq;
    using NUnit.Framework;

    public class Dump
    {
        public static void Xml(string text) => Console.Write(text);

        public static void XmlAsCode(string text)
        {
            var lines = Lines().ToArray();
            for (var i = 0; i < lines.Length; i++)
            {
                Console.Write($"\"{lines[i].Replace("\"", "\\\"")}\"");
                if (i == lines.Length - 1)
                {
                    Console.WriteLine(";");
                }
                else
                {
                    Console.WriteLine(" + Environment.NewLine +");
                }
            }

            IEnumerable<string> Lines()
            {
                using (var reader = new StringReader(text))
                {
                    while (reader.ReadLine() is string line)
                    {
                        yield return line;
                    }
                }
            }
        }

        [Explicit("Script")]
        [TestCase(typeof(ImmutableArray<>))]
        public void MarkdownList(Type source)
        {
            foreach (var type in source.Assembly
                                       .GetExportedTypes()
                                       .Where(x => typeof(IEnumerable).IsAssignableFrom(x) &&
                                                   !x.IsInterface &&
                                                   !x.IsAbstract &&
                                                   !x.Name.Contains("Builder", StringComparison.Ordinal) &&
                                                   x.Namespace == source.Namespace)
                                       .OrderBy(x => x.Name))
            {
                Console.WriteLine("- [ ] `" + type.ToString().Replace("`1[T]", "<T>", StringComparison.Ordinal).Replace("`2[TKey,TValue]", "<TKey, TValue>", StringComparison.Ordinal) + "`");
            }
        }
    }
}
