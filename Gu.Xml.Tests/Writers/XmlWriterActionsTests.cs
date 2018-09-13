namespace Gu.Xml.Tests.Writers
{
    using System.Globalization;
    using System.IO;
    using System.Text;
    using NUnit.Framework;

    public class XmlWriterActionsTests
    {
        [Test]
        public void RegisterSimpleNullableInt()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                var maps = new WriteMaps().RegisterSimple<int?>((writer, value) => writer.Write(value?.ToString(NumberFormatInfo.InvariantInfo)));
                int? nullableValue = 1;

                Assert.AreEqual(true, maps.TryGetSimple(nullableValue, out var map));
                map.Write.Invoke(stringWriter, nullableValue);
                Assert.AreEqual("1", sb.ToString());

                var intValue = 2;
                Assert.AreEqual(true, maps.TryGetSimple(intValue, out map));
                map.Write.Invoke(stringWriter, intValue);
                Assert.AreEqual("12", sb.ToString());
            }
        }

        [Test]
        public void RegisterSimpleInt()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                var maps = new WriteMaps().RegisterSimple<int>((writer, value) => writer.Write(value.ToString(NumberFormatInfo.InvariantInfo)));

                var intValue = 1;
                Assert.AreEqual(true, maps.TryGetSimple(intValue, out var map));
                map.Write.Invoke(stringWriter, intValue);
                Assert.AreEqual("1", sb.ToString());

                int? nullableValue = 2;
                Assert.AreEqual(true, maps.TryGetSimple(nullableValue, out map));
                map.Write.Invoke(stringWriter, nullableValue);
                Assert.AreEqual("12", sb.ToString());
            }
        }
    }
}