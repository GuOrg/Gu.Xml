namespace Gu.Xml.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;

    public partial class XmlTests
    {
        [Explicit("Not implemented yet.")]
        public class SerializeCollections
        {
            private static readonly TestCaseData[] Values =
            {
                new TestCaseData(new[] { 1, 2, 3 }),
                new TestCaseData(new[] { new Foo(1), new Foo(2), new Foo(3) }),
                new TestCaseData(new List<int> { 1, 2, 3 }),
                new TestCaseData(new List<Foo> { new Foo(1), new Foo(2), new Foo(3) }),
                new TestCaseData(new Dictionary<int, string> { { 1, "a" }, { 2, "b" } }),
                new TestCaseData(new ArrayList { 1, 2, 3 }),
            };

            [TestCaseSource(nameof(Values))]
            public void Serialize(object value)
            {
                var expected = Reference.Xml(value);
                var actual = Xml.Serialize(value);
                if (actual == expected)
                {
                    return;
                }

                Console.WriteLine("Expected:");
                Console.Write(expected);
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("Actual:");
                Console.Write(actual);
                Console.WriteLine();
                Console.WriteLine();

                Assert.AreEqual(expected, actual);
            }

            public class Foo
            {
                // ReSharper disable once UnusedMember.Local for XmlSerializer
                private Foo()
                {
                }

                public Foo(int value)
                {
                    this.Value = value;
                }

                public int Value { get; }
            }
        }
    }
}