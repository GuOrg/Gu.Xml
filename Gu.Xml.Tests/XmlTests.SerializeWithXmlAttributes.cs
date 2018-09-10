namespace Gu.Xml.Tests
{
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using NUnit.Framework;

    public partial class XmlTests
    {
        public class SerializeWithXmlAttributes
        {
            private static readonly TestCaseData[] Values =
            {
                new TestCaseData(new WithXmlRootAttribute { Value = 1 }),
                new TestCaseData(new WithXmlRootAttributeExplicitName { Value = 1 }),
                new TestCaseData(new PropertyWithXmlIgnore { Value = 1 }),
                new TestCaseData(new FieldWithXmlIgnore { Value = 1 }),
                new TestCaseData(new PropertyWithXmlElementAttribute { Value = 1 }),
                new TestCaseData(new PropertyWithXmlElementAttributeExplicitName { Value = 1 }),
                new TestCaseData(new PropertyWithXmlAttributeAttribute { Value = 1 }),
                new TestCaseData(new PropertyWithXmlAttributeAttributeExplicitName { Value = 1 }),
                new TestCaseData(new ExplicitInterfaceWithXmlElementAttribute()),
                new TestCaseData(new FieldWithXmlElementAttribute { Value = 1 }),
                new TestCaseData(new FieldWithXmlElementAttributeExplicitName { Value = 1 }),
                new TestCaseData(new FieldWithXmlAttributeAttribute { Value = 1 }),
                new TestCaseData(new FieldWithXmlAttributeAttributeExplicitName { Value = 1 }),
            };

            [TestCaseSource(nameof(Values))]
            public void Serialize(object value)
            {
                var expected = Reference.Xml(value);
                var actual = Xml.Serialize(value);
                if (actual == expected)
                {
                    if (Debugger.IsAttached)
                    {
                        Console.WriteLine(expected);
                    }

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

            [XmlRoot]
            public class WithXmlRootAttribute
            {
                public int Value { get; set; } = 1;
            }

            [XmlRoot("Name")]
            public class WithXmlRootAttributeExplicitName
            {
                public int Value { get; set; } = 1;
            }

            public class PropertyWithXmlElementAttribute
            {
                [XmlElement]
                public int Value { get; set; }
            }

            public class PropertyWithXmlElementAttributeExplicitName
            {
                [XmlElement("Name")]
                public int Value { get; set; }
            }

            public class FieldWithXmlElementAttribute
            {
                [XmlElement]
                public int Value = 1;
            }

            public class FieldWithXmlElementAttributeExplicitName
            {
                [XmlElement("Name")]
                public int Value = 1;
            }

            public interface IValue
            {
                // ReSharper disable once UnusedMember.Global
                int Value { get; set; }
            }

            public class ExplicitInterfaceWithXmlElementAttribute : IValue
            {
                [XmlElement]
                int IValue.Value { get; set; }
            }

            public class PropertyWithXmlAttributeAttribute
            {
                [XmlAttribute]
                public int Value { get; set; }
            }

            public class PropertyWithXmlAttributeAttributeExplicitName
            {
                [XmlAttribute("Name")]
                public int Value { get; set; }
            }

            public class FieldWithXmlAttributeAttribute
            {
                [XmlAttribute]
                public int Value;
            }

            public class FieldWithXmlAttributeAttributeExplicitName
            {
                [XmlAttribute("Name")]
                public int Value;
            }

            public class PropertyWithXmlIgnore
            {
                [XmlIgnore]
                public int Value { get; set; }
            }

            public class FieldWithXmlIgnore
            {
                [XmlIgnore]
                public int Value { get; set; }
            }
        }
    }
}