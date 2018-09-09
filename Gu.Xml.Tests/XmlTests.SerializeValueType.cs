﻿namespace Gu.Xml.Tests
{
    using System;
    using NUnit.Framework;

    public partial class XmlTests
    {
        public class SerializeValueType
        {
            [Test]
            public void WithGetOnlyProperty()
            {
                var value = new StructWithGetOnlyProperty(1);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<StructWithGetOnlyProperty>" + Environment.NewLine +
                               "  <Value>1</Value>" + Environment.NewLine +
                               "</StructWithGetOnlyProperty>";

                var actual = Xml.Serialize(value);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void WithReadonlyField()
            {
                var value = new StructWithReadonlyField(1);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<StructWithReadonlyField>" + Environment.NewLine +
                               "  <Value>1</Value>" + Environment.NewLine +
                               "</StructWithReadonlyField>";

                var actual = Xml.Serialize(value);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void WithStructWithGetOnlyProperty()
            {
                var value = new With<StructWithGetOnlyProperty> { Value = new StructWithGetOnlyProperty(1) };
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<WithOfStructWithGetOnlyProperty>" + Environment.NewLine +
                               "  <Value>" + Environment.NewLine +
                               "    <Value>1</Value>" + Environment.NewLine +
                               "  </Value>" + Environment.NewLine +
                               "</WithOfStructWithGetOnlyProperty>";

                var actual = Xml.Serialize(value);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void Empty()
            {
                var value = default(EmptyStruct);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<EmptyStruct />";

                var actual = Xml.Serialize(value);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void WithEmptyStruct()
            {
                var value = new With<EmptyStruct> { Value = default(EmptyStruct) };
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<WithOfEmptyStruct>" + Environment.NewLine +
                               "  <Value />" + Environment.NewLine +
                               "</WithOfEmptyStruct>";

                var actual = Xml.Serialize(value);
                Assert.AreEqual(expected, actual);
            }

            public struct StructWithGetOnlyProperty
            {
                public StructWithGetOnlyProperty(int value)
                {
                    this.Value = value;
                }

                public int Value { get; }
            }

            public struct StructWithReadonlyField
            {
                public readonly int Value;

                public StructWithReadonlyField(int value)
                {
                    this.Value = value;
                }
            }

            public struct EmptyStruct
            {
            }

            public class With<T>
            {
                public T Value { get; set; } = default(T);
            }
        }
    }
}