namespace Gu.Xml.Tests
{
    using System;
    using System.Xml.Serialization;
    using NUnit.Framework;

    public partial class XmlTests
    {
        public class Properties
        {
            [Test]
            public void GetSet()
            {
                var with = new WithGetSet { Value = 1 };
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<WithGetSet>" + Environment.NewLine +
                               "  <Value>1</Value>" + Environment.NewLine +
                               "</WithGetSet>";

                var actual = Xml.Serialize(with);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void GetPrivateSet()
            {
                var with = new WithGetPrivateSet(1);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<WithGetPrivateSet>" + Environment.NewLine +
                               "  <Value>1</Value>" + Environment.NewLine +
                               "</WithGetPrivateSet>";

                var actual = Xml.Serialize(with);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void GetOnly()
            {
                var with = new WithGetOnly(1);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<WithGetOnly>" + Environment.NewLine +
                               "  <Value>1</Value>" + Environment.NewLine +
                               "</WithGetOnly>";

                var actual = Xml.Serialize(with);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ExcludeCalculated()
            {
                var with = new WithGetOnlyAndCalculated(1);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<WithGetOnlyAndCalculated>" + Environment.NewLine +
                               "  <Value>1</Value>" + Environment.NewLine +
                               "</WithGetOnlyAndCalculated>";

                var actual = Xml.Serialize(with);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void XmlElementAttribute()
            {
                var with = new WithXmlElement { Value = 1 };
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<WithXmlElement>" + Environment.NewLine +
                               "  <Name>1</Name>" + Environment.NewLine +
                               "</WithXmlElement>";

                var actual = Xml.Serialize(with);
                Assert.AreEqual(expected, actual);
            }

            public class WithGetSet
            {
                public int Value { get; set; }
            }

            public class WithGetPrivateSet
            {
                public WithGetPrivateSet(int value)
                {
                    Value = value;
                }

                public int Value { get; private set; }
            }

            public class WithGetOnly
            {
                public WithGetOnly(int value)
                {
                    this.Value = value;
                }

                public int Value { get; }
            }

            public class WithGetOnlyAndCalculated
            {
                public WithGetOnlyAndCalculated(int value)
                {
                    this.Value = value;
                }

                public int Value { get; }

                public int Negated => -this.Value;
            }

            public class WithXmlElement
            {
                [XmlElement("Name")]
                public int Value { get; set; }
            }
        }
    }
}