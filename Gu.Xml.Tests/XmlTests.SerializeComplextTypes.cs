namespace Gu.Xml.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;
    using NUnit.Framework;

    public partial class XmlTests
    {
        public class SerializeComplextTypes
        {
            private static readonly TestCaseData[] Values =
            {
                new TestCaseData(new Foo()),
                //new TestCaseData(new List<int> { 1, 2 }),
            };

            [TestCaseSource(nameof(Values))]
            public void SerializeValue(object value)
            {
                var expected = ReferenceXml(value);
                var actual = Xml.Serialize(value);
                Assert.AreEqual(expected, actual);
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
        }
    }
}