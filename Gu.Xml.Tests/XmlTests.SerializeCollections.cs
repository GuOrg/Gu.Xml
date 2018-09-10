namespace Gu.Xml.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
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
            public void ArrayList()
            {
                var value = new ArrayList { new Foo(1), new Foo(2), new Foo(3) };
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<ArrayList>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>1</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>2</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>3</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "</ArrayList>";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void BitArray()
            {
                var value = new BitArray(new[] { true, false });
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<BitArray>" + Environment.NewLine +
                               "  <Boolean>true</Boolean>" + Environment.NewLine +
                               "  <Boolean>false</Boolean>" + Environment.NewLine +
                               "</BitArray>";
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
            public void EnumerableRange()
            {
                var value = Enumerable.Range(1, 3);
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<IEnumerableOfInt32>" + Environment.NewLine +
                               "  <Int32>1</Int32>" + Environment.NewLine +
                               "  <Int32>2</Int32>" + Environment.NewLine +
                               "  <Int32>3</Int32>" + Environment.NewLine +
                               "</IEnumerableOfInt32>";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void EnumerableSelect()
            {
                var value = Enumerable.Range(1, 3).Select(x => x);
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<WhereSelectEnumerableIteratorOfInt32Int32>" + Environment.NewLine +
                               "  <Int32>1</Int32>" + Environment.NewLine +
                               "  <Int32>2</Int32>" + Environment.NewLine +
                               "  <Int32>3</Int32>" + Environment.NewLine +
                               "</WhereSelectEnumerableIteratorOfInt32Int32>";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void EnumerableWhere()
            {
                var value = Enumerable.Range(1, 3).Where(_ => true);
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<WhereEnumerableIteratorOfInt32>" + Environment.NewLine +
                               "  <Int32>1</Int32>" + Environment.NewLine +
                               "  <Int32>2</Int32>" + Environment.NewLine +
                               "  <Int32>3</Int32>" + Environment.NewLine +
                               "</WhereEnumerableIteratorOfInt32>";
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
            public void Hashtable()
            {
                var value = new Hashtable { { 1, "a" }, { 2, "b" } };
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<Hashtable>" + Environment.NewLine +
                               "  <DictionaryEntry>" + Environment.NewLine +
                               "    <Key>2</Key>" + Environment.NewLine +
                               "    <Value>b</Value>" + Environment.NewLine +
                               "  </DictionaryEntry>" + Environment.NewLine +
                               "  <DictionaryEntry>" + Environment.NewLine +
                               "    <Key>1</Key>" + Environment.NewLine +
                               "    <Value>a</Value>" + Environment.NewLine +
                               "  </DictionaryEntry>" + Environment.NewLine +
                               "</Hashtable>";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ImmutableArrayOfInt32()
            {
                var value = ImmutableArray.Create(new[] { 1, 2, 3 });
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<ImmutableArrayOfInt32>" + Environment.NewLine +
                               "  <Int32>1</Int32>" + Environment.NewLine +
                               "  <Int32>2</Int32>" + Environment.NewLine +
                               "  <Int32>3</Int32>" + Environment.NewLine +
                               "</ImmutableArrayOfInt32>";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ImmutableDictionaryOfInt32String()
            {
                var value = ImmutableDictionary.CreateRange(new[] { new KeyValuePair<int, string>(1, "a"), new KeyValuePair<int, string>(2, "b") });
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<ImmutableDictionaryOfInt32String>" + Environment.NewLine +
                               "  <KeyValuePairOfInt32String>" + Environment.NewLine +
                               "    <Key>1</Key>" + Environment.NewLine +
                               "    <Value>a</Value>" + Environment.NewLine +
                               "  </KeyValuePairOfInt32String>" + Environment.NewLine +
                               "  <KeyValuePairOfInt32String>" + Environment.NewLine +
                               "    <Key>2</Key>" + Environment.NewLine +
                               "    <Value>b</Value>" + Environment.NewLine +
                               "  </KeyValuePairOfInt32String>" + Environment.NewLine +
                               "</ImmutableDictionaryOfInt32String>";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ImmutableHashSetOfInt32()
            {
                var value = ImmutableHashSet.Create(1, 2, 3);
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<ImmutableHashSetOfInt32>" + Environment.NewLine +
                               "  <Int32>1</Int32>" + Environment.NewLine +
                               "  <Int32>2</Int32>" + Environment.NewLine +
                               "  <Int32>3</Int32>" + Environment.NewLine +
                               "</ImmutableHashSetOfInt32>";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ImmutableListOfInt32()
            {
                var value = ImmutableList.Create(1, 2, 3);
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<ImmutableListOfInt32>" + Environment.NewLine +
                               "  <Int32>1</Int32>" + Environment.NewLine +
                               "  <Int32>2</Int32>" + Environment.NewLine +
                               "  <Int32>3</Int32>" + Environment.NewLine +
                               "</ImmutableListOfInt32>";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ImmutableQueueOfInt32()
            {
                var value = ImmutableQueue.Create(1, 2, 3);
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<ImmutableQueueOfInt32>" + Environment.NewLine +
                               "  <Int32>1</Int32>" + Environment.NewLine +
                               "  <Int32>2</Int32>" + Environment.NewLine +
                               "  <Int32>3</Int32>" + Environment.NewLine +
                               "</ImmutableQueueOfInt32>";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ImmutableSortedDictionaryOfInt32String()
            {
                var value = ImmutableSortedDictionary.CreateRange(new[] { new KeyValuePair<int, string>(1, "a"), new KeyValuePair<int, string>(2, "b") });
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<ImmutableSortedDictionaryOfInt32String>" + Environment.NewLine +
                               "  <KeyValuePairOfInt32String>" + Environment.NewLine +
                               "    <Key>1</Key>" + Environment.NewLine +
                               "    <Value>a</Value>" + Environment.NewLine +
                               "  </KeyValuePairOfInt32String>" + Environment.NewLine +
                               "  <KeyValuePairOfInt32String>" + Environment.NewLine +
                               "    <Key>2</Key>" + Environment.NewLine +
                               "    <Value>b</Value>" + Environment.NewLine +
                               "  </KeyValuePairOfInt32String>" + Environment.NewLine +
                               "</ImmutableSortedDictionaryOfInt32String>";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ImmutableSortedSetOfInt32()
            {
                var value = ImmutableSortedSet.Create(1, 2, 3);
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<ImmutableSortedSetOfInt32>" + Environment.NewLine +
                               "  <Int32>1</Int32>" + Environment.NewLine +
                               "  <Int32>2</Int32>" + Environment.NewLine +
                               "  <Int32>3</Int32>" + Environment.NewLine +
                               "</ImmutableSortedSetOfInt32>";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ImmutableStackOfInt32()
            {
                var value = ImmutableStack.Create(3, 2, 1);
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<ImmutableStackOfInt32>" + Environment.NewLine +
                               "  <Int32>1</Int32>" + Environment.NewLine +
                               "  <Int32>2</Int32>" + Environment.NewLine +
                               "  <Int32>3</Int32>" + Environment.NewLine +
                               "</ImmutableStackOfInt32>";
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
            public void Stack()
            {
                var value = new Stack(new[] { new Foo(3), new Foo(2), new Foo(1) });
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<Stack>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>1</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>2</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>3</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "</Stack>";
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
            public void SortedDictionaryOfInt32String()
            {
                var value = new SortedDictionary<int, string>(new Dictionary<int, string> { { 1, "a" }, { 2, "b" } });
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<SortedDictionaryOfInt32String>" + Environment.NewLine +
                               "  <KeyValuePairOfInt32String>" + Environment.NewLine +
                               "    <Key>1</Key>" + Environment.NewLine +
                               "    <Value>a</Value>" + Environment.NewLine +
                               "  </KeyValuePairOfInt32String>" + Environment.NewLine +
                               "  <KeyValuePairOfInt32String>" + Environment.NewLine +
                               "    <Key>2</Key>" + Environment.NewLine +
                               "    <Value>b</Value>" + Environment.NewLine +
                               "  </KeyValuePairOfInt32String>" + Environment.NewLine +
                               "</SortedDictionaryOfInt32String>";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void SortedList()
            {
                var value = new SortedList(new Dictionary<int, string> { { 1, "a" }, { 2, "b" } });
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<SortedList>" + Environment.NewLine +
                               "  <DictionaryEntry>" + Environment.NewLine +
                               "    <Key>1</Key>" + Environment.NewLine +
                               "    <Value>a</Value>" + Environment.NewLine +
                               "  </DictionaryEntry>" + Environment.NewLine +
                               "  <DictionaryEntry>" + Environment.NewLine +
                               "    <Key>2</Key>" + Environment.NewLine +
                               "    <Value>b</Value>" + Environment.NewLine +
                               "  </DictionaryEntry>" + Environment.NewLine +
                               "</SortedList>";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void SortedListOfInt32String()
            {
                var value = new SortedList<int, string>(new Dictionary<int, string> { { 1, "a" }, { 2, "b" } });
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<SortedListOfInt32String>" + Environment.NewLine +
                               "  <KeyValuePairOfInt32String>" + Environment.NewLine +
                               "    <Key>1</Key>" + Environment.NewLine +
                               "    <Value>a</Value>" + Environment.NewLine +
                               "  </KeyValuePairOfInt32String>" + Environment.NewLine +
                               "  <KeyValuePairOfInt32String>" + Environment.NewLine +
                               "    <Key>2</Key>" + Environment.NewLine +
                               "    <Value>b</Value>" + Environment.NewLine +
                               "  </KeyValuePairOfInt32String>" + Environment.NewLine +
                               "</SortedListOfInt32String>";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void SortedSetOfFooOfInt32()
            {
                var value = new SortedSet<Foo>(new[] { new Foo(1), new Foo(2), new Foo(3) });
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<SortedSetOfFoo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>1</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>2</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>3</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "</SortedSetOfFoo>";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void Queue()
            {
                var value = new Queue(new[] { new Foo(1), new Foo(2), new Foo(3) });
                var actual = Xml.Serialize(value);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<Queue>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>1</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>2</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "  <Foo>" + Environment.NewLine +
                               "    <Value>3</Value>" + Environment.NewLine +
                               "  </Foo>" + Environment.NewLine +
                               "</Queue>";
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

            public class Foo : IComparable<Foo>
            {
                public Foo(int value)
                {
                    this.Value = value;
                }

                // ReSharper disable once UnusedMember.Local for XmlSerializer
                private Foo()
                {
                }

                public int Value { get; }

                public int CompareTo(Foo other) => this.Value.CompareTo(other.Value);
            }
        }
    }
}