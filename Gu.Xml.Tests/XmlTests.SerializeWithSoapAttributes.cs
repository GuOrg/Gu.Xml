// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable NotAccessedField.Global
#pragma warning disable SA1201 // Elements should appear in the correct order
namespace Gu.Xml.Tests
{
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;
    using NUnit.Framework;

    public partial class XmlTests
    {
        public class SerializeWithSoapAttributes
        {
            private static readonly TestCaseData[] TestCases =
            {
                new TestCaseData(new WithSoapTypeAttribute { Value = 1 }),
                new TestCaseData(new WithSoapTypeAttributeExplicitName { Value = 1 }),
                new TestCaseData(new PropertyWithSoapIgnoreAttribute { Value = 1 }),
                new TestCaseData(new PropertyWithSoapElementAttribute { Value = 1 }),
                new TestCaseData(new PropertyWithSoapElementAttributeExplicitName { Value = 1 }),
                new TestCaseData(new PropertyWithSoapAttributeAttribute { Value = 1 }),
                new TestCaseData(new PropertyWithSoapAttributeAttributeExplicitName { Value = 1 }),
                ////new TestCaseData(new ExplicitInterfaceWithSoapElementAttribute()),
                new TestCaseData(new FieldWithSoapIgnoreAttribute { Value = 1 }),
                new TestCaseData(new FieldWithSoapElementAttribute { Value = 1 }),
                new TestCaseData(new FieldWithSoapElementAttributeExplicitName { Value = 1 }),
                new TestCaseData(new FieldWithSoapAttributeAttribute { Value = 1 }),
                new TestCaseData(new FieldWithSoapAttributeAttributeExplicitName { Value = 1 }),
                new TestCaseData(new With<WithSoapEnumAttribute> { Value = WithSoapEnumAttribute.One }),
                new TestCaseData(new With<WithSoapEnumAttribute> { Value = WithSoapEnumAttribute.Two }),
            };

            [TestCaseSource(nameof(TestCases))]
            public void Serialize(object value)
            {
                var expected = Reference.XmlSerializerSoap(value).Replace(" xsi:type=\"WithSoapEnumAttribute\"", string.Empty, StringComparison.Ordinal);
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

            [SoapType]
            public class WithSoapTypeAttribute
            {
                public int Value { get; set; } = 1;
            }

            [SoapType("Name")]
            public class WithSoapTypeAttributeExplicitName
            {
                public int Value { get; set; } = 1;
            }

            public class PropertyWithSoapElementAttribute
            {
                [SoapElement]
                public int Value { get; set; }
            }

            public class PropertyWithSoapElementAttributeExplicitName
            {
                [SoapElement("Name")]
                public int Value { get; set; }
            }

            public class FieldWithSoapElementAttribute
            {
                [SoapElement]
                public int Value = 1;
            }

            public class FieldWithSoapElementAttributeExplicitName
            {
                [SoapElement("Name")]
                public int Value = 1;
            }

            public interface IValue
            {
                // ReSharper disable once UnusedMember.Global
                int Value { get; set; }
            }

            public class ExplicitInterfaceWithSoapElementAttribute : IValue
            {
                [SoapElement]
                int IValue.Value { get; set; }
            }

            public class PropertyWithSoapAttributeAttribute
            {
                [SoapAttribute]
                public int Value { get; set; }
            }

            public class PropertyWithSoapAttributeAttributeExplicitName
            {
                [SoapAttribute("Name")]
                public int Value { get; set; }
            }

            public class FieldWithSoapAttributeAttribute
            {
                [SoapAttribute]
                public int Value;
            }

            public class FieldWithSoapAttributeAttributeExplicitName
            {
                [SoapAttribute("Name")]
                public int Value;
            }

            public class PropertyWithSoapIgnoreAttribute
            {
                [SoapIgnore]
                public int Value { get; set; }
            }

            public class FieldWithSoapIgnoreAttribute
            {
                [SoapIgnore]
                public int Value { get; set; }
            }

            public class With<T>
            {
                public T? Value { get; set; }
            }

            public enum WithSoapEnumAttribute
            {
                [SoapEnum(Name = "Single")]
                One,
                [SoapEnum(Name = "Double")]
                Two,
            }
        }
    }
}
