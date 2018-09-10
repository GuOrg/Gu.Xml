namespace Gu.Xml.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Immutable;
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
        [TestCase(typeof(ImmutableArray<>))]
        public void MarkdownList(Type source)
        {
            foreach (var type in source.Assembly
                                       .GetExportedTypes()
                                       .Where(x => typeof(IEnumerable).IsAssignableFrom(x) &&
                                                   !x.IsInterface &&
                                                   !x.IsAbstract &&
                                                   !x.Name.Contains("Builder") &&
                                                   x.Namespace == source.Namespace)
                                       .OrderBy(x => x.Name))
            {
                Console.WriteLine("- [ ] `" + type.ToString().Replace("`1[T]", "<T>").Replace("`2[TKey,TValue]", "<TKey, TValue>") + "`");
            }
        }
    }
}
