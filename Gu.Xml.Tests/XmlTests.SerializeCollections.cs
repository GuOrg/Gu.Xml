namespace Gu.Xml.Tests
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using NUnit.Framework;

    public partial class XmlTests
    {
        public class SerializeCollections
        {
            [Test]
            public void ArrayOfInt32Empty()
            {
                var value = new int[0];
                var actual = Xml.Serialize(value);
                Console.WriteLine(actual);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<ArrayOfInt32 />";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ArrayOfInt32()
            {
                var value = new[] { 1, 2, 3 };
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<ArrayOfInt32>" + Environment.NewLine +
                               "  <Int32>1</Int32>" + Environment.NewLine +
                               "  <Int32>2</Int32>" + Environment.NewLine +
                               "  <Int32>3</Int32>" + Environment.NewLine +
                               "</ArrayOfInt32>";
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

            [Test]
            public void ConcurrentStackOfFooOfInt32()
            {
                var value = new ConcurrentStack<Foo>(new[] { new Foo(3), new Foo(2), new Foo(1) });
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<ConcurrentStackOfFoo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>1</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>2</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>3</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "</ConcurrentStackOfFoo>";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ConcurrentQueueOfFooOfInt32()
            {
                var value = new ConcurrentQueue<Foo>(new[] { new Foo(1), new Foo(2), new Foo(3) });
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<ConcurrentQueueOfFoo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>1</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>2</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>3</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "</ConcurrentQueueOfFoo>";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void DictionaryOfInt32StringEmpty()
            {
                var value = new Dictionary<int, string>();
                var actual = Xml.Serialize(value);
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
            public void HashSetOfFooOfInt32()
            {
                var value = new HashSet<Foo>(new[] { new Foo(1), new Foo(2), new Foo(3) });
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<HashSetOfFoo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>1</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>2</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>3</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "</HashSetOfFoo>";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void LinkedListOfFooOfInt32()
            {
                var value = new LinkedList<Foo>(new[] { new Foo(1), new Foo(2), new Foo(3) });
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<LinkedListOfFoo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>1</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>2</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>3</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "</LinkedListOfFoo>";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ListOfFooOfInt32()
            {
                var value = new List<Foo> { new Foo(1), new Foo(2), new Foo(3) };
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<ListOfFoo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>1</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>2</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>3</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "</ListOfFoo>";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void StackOfFooOfInt32()
            {
                var value = new Stack<Foo>(new[] { new Foo(3), new Foo(2), new Foo(1) });
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<StackOfFoo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>1</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>2</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>3</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "</StackOfFoo>";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void QueueOfFooOfInt32()
            {
                var value = new Queue<Foo>(new[] { new Foo(1), new Foo(2), new Foo(3) });
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<QueueOfFoo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>1</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>2</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>3</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "</QueueOfFoo>";
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