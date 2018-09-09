namespace Gu.Xml.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;
    using NUnit.Framework;

    public partial class XmlTests
    {
        public class SerializeComplexTypes
        {
            private static readonly TestCaseData[] Values =
            {
                new TestCaseData(new Foo()),
                new TestCaseData(new Bar()),
                new TestCaseData(new Foo<int>()),
                new TestCaseData(new Foo<string>()),
                new TestCaseData(new Foo<KeyValuePair<int, double>> { Value = new KeyValuePair<int, double>(1, 2) }),
            };

            [TestCaseSource(nameof(Values))]
            public void SerializeValue(object value)
            {
                var expected = ReferenceXml(value);
                try
                {
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
                catch
                {
                    Console.WriteLine(expected);
                    throw;
                }
            }

            private static string ReferenceXml(object value)
            {
                var sb = new StringBuilder();
                var serializer = new XmlSerializer(value.GetType());
                using (var writer = new StringWriter(sb))
                {
                    serializer.Serialize(writer, value);
                }

                return sb.Replace("utf-16", "utf-8")
                         .Replace(" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "")
                         .ToString();
            }

            public class Foo
            {
                public int Value { get; set; } = 1;
            }

            public class Foo<T>
            {
                public T Value { get; set; } = default(T);
            }

            public class Bar
            {
                public int Value1 { get; set; } = 1;

                public int Value2 { get; set; } = 2;
            }
        }
    }
}