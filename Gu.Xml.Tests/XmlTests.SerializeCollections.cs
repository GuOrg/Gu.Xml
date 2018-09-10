namespace Gu.Xml.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using NUnit.Framework;

    public partial class XmlTests
    {
        public class SerializeCollections
        {
            private static readonly TestCaseData[] Values =
            {
                new TestCaseData(new string[] { "a", null, string.Empty }),
                new TestCaseData(new[] { new Foo(1), new Foo(2), new Foo(3) }),
                new TestCaseData(new List<int> { 1, 2, 3 }),
                new TestCaseData(new List<Foo> { new Foo(1), new Foo(2), new Foo(3) }),
                new TestCaseData(new HashSet<Foo> { new Foo(1), new Foo(2), new Foo(3) }),
                new TestCaseData(new Stack<Foo>(new[] { new Foo(1), new Foo(2), new Foo(3) })),
                new TestCaseData(new Queue<Foo>(new[] { new Foo(1), new Foo(2), new Foo(3) })),
                new TestCaseData(new ArrayList { 1, 2, 3 }),
            };

            [TestCaseSource(nameof(Values))]
            public void Serialize(object value)
            {
                var expected = Reference.XmlSerializer(value);
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

            [Test]
            public void ArrayOfInt32Empty()
            {
                var value = new int[0];
                var actual = Xml.Serialize(value);
                Console.WriteLine(actual);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<ArrayInt32 />";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ArrayOfInts()
            {
                var value = new[] { 1, 2, 3 };
                var actual = Xml.Serialize(value);
                Console.WriteLine(actual);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<ArrayInt32 />";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void DictionaryOfInt32StringEmpty()
            {
                var value = new Dictionary<int, string>();
                var actual = Xml.Serialize(value);
                Console.WriteLine(actual);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<DictionaryOfInt32String />";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void DictionaryOfInt32StringOneEntry()
            {
                var value = new Dictionary<int, string> { { 1, "a" } };
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<DictionaryOfInt32String>" + Environment.NewLine +
                               "  <KeyValuePairOfInt32String>" + Environment.NewLine +
                               "    <Key>1</Key>" + Environment.NewLine +
                               "    <Value>a</Value>" + Environment.NewLine +
                               "  </KeyValuePairOfInt32String>" + Environment.NewLine +
                               "</DictionaryOfInt32String>";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void DictionaryOfInt32StringTwoEntries()
            {
                var value = new Dictionary<int, string> { { 1, "a" }, { 2, "b" } };
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<DictionaryOfInt32String>" + Environment.NewLine +
                               "  <KeyValuePairOfInt32String>" + Environment.NewLine +
                               "    <Key>1</Key>" + Environment.NewLine +
                               "    <Value>a</Value>" + Environment.NewLine +
                               "  </KeyValuePairOfInt32String>" + Environment.NewLine +
                               "  <KeyValuePairOfInt32String>" + Environment.NewLine +
                               "    <Key>2</Key>" + Environment.NewLine +
                               "    <Value>b</Value>" + Environment.NewLine +
                               "  </KeyValuePairOfInt32String>" + Environment.NewLine +
                               "</DictionaryOfInt32String>";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ConcurrentDictionaryOfInt32StringTwoEntries()
            {
                var value = new ConcurrentDictionary<int, string>(new Dictionary<int, string> { { 1, "a" }, { 2, "b" } });
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<ConcurrentDictionaryOfInt32String>" + Environment.NewLine +
                               "  <KeyValuePairOfInt32String>" + Environment.NewLine +
                               "    <Key>1</Key>" + Environment.NewLine +
                               "    <Value>a</Value>" + Environment.NewLine +
                               "  </KeyValuePairOfInt32String>" + Environment.NewLine +
                               "  <KeyValuePairOfInt32String>" + Environment.NewLine +
                               "    <Key>2</Key>" + Environment.NewLine +
                               "    <Value>b</Value>" + Environment.NewLine +
                               "  </KeyValuePairOfInt32String>" + Environment.NewLine +
                               "</ConcurrentDictionaryOfInt32String>";
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