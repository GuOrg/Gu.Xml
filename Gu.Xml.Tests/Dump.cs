namespace Gu.Xml.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;

    public class Dump
    {
        [Explicit("Script")]
        [TestCase(typeof(IEnumerable<>))]
        [TestCase(typeof(Queue<>))]
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
