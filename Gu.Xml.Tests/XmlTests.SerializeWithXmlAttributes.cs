// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable SA1201 // Elements should appear in the correct order
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
                new TestCaseData(new PropertyWithXmlIgnoreAttribute { Value = 1 }),
                new TestCaseData(new PropertyWithXmlElementAttribute { Value = 1 }),
                new TestCaseData(new PropertyWithXmlElementAttributeExplicitName { Value = 1 }),
                new TestCaseData(new PropertyWithXmlAttributeAttribute { Value = 1 }),
                new TestCaseData(new PropertyWithXmlAttributeAttributeExplicitName { Value = 1 }),
                ////new TestCaseData(new ExplicitInterfaceWithXmlElementAttribute()),
                new TestCaseData(new FieldWithXmlIgnoreAttribute { Value = 1 }),
                new TestCaseData(new FieldWithXmlElementAttribute { Value = 1 }),
                new TestCaseData(new FieldWithXmlElementAttributeExplicitName { Value = 1 }),
                new TestCaseData(new FieldWithXmlAttributeAttribute { Value = 1 }),
                new TestCaseData(new FieldWithXmlAttributeAttributeExplicitName { Value = 1 }),
                new TestCaseData(new FieldsWithXmlAttributeAttributeExplicitName { Value1 = 1, Value2 = 2 }),
                new TestCaseData(new WithArrayWithFieldsWithXmlAttributeAttributeExplicitName
                {
                    Items = new[]
                    {
                        new FieldsWithXmlAttributeAttributeExplicitName { Value1 = 1, Value2 = 2 },
                        new FieldsWithXmlAttributeAttributeExplicitName { Value1 = 3, Value2 = 4 },
                    },
                }),
                new TestCaseData(new With<WithXmlEnumAttribute> { Value = WithXmlEnumAttribute.One }),
                new TestCaseData(new With<WithXmlEnumAttribute> { Value = WithXmlEnumAttribute.Two }),
                new TestCaseData(new WithXmlArrayAndXmlArrayItemAttribute { Ints = new[] { 1, 2, 3 } }),
            };

            [TestCaseSource(nameof(Values))]
            public void Serialize(object value)
            {
                var expected = Reference.XmlSerializer(value);
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

            public class WithXmlArrayAndXmlArrayItemAttribute
            {
                [XmlArray(ElementName = "Numbers")]
                [XmlArrayItem(ElementName = "Int32")]
                public int[] Ints { get; set; }
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

            public class FieldsWithXmlAttributeAttributeExplicitName
            {
                [XmlAttribute("Value1")]
                public int Value1;

                [XmlAttribute("Value2")]
                public int Value2;
            }

            public class WithArrayWithFieldsWithXmlAttributeAttributeExplicitName
            {
                public FieldsWithXmlAttributeAttributeExplicitName[] Items { get; set; }
            }

            public class PropertyWithXmlIgnoreAttribute
            {
                [XmlIgnore]
                public int Value { get; set; }
            }

            public class FieldWithXmlIgnoreAttribute
            {
                [XmlIgnore]
                public int Value { get; set; }
            }

            public class With<T>
            {
                public T Value { get; set; }
            }

            public enum WithXmlEnumAttribute
            {
                [XmlEnum(Name = "Single")]
                One,
                [XmlEnum(Name = "Double")]
                Two,
            }
        }
    }
}