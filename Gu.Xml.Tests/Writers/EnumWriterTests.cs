namespace Gu.Xml.Tests.Writers
{
    using System;
    using System.IO;
    using System.Text;
    using NUnit.Framework;

    public class EnumWriterTests
    {
        [TestCase(StringComparison.Ordinal, "4")]
        [TestCase(StringComparison.InvariantCulture, "2")]
        public void IntegerWrite(StringComparison stringComparison, string text)
        {
            var sb = new StringBuilder();
            using var writer = new StringWriter(sb);
            EnumFormatter<StringComparison>.Integer.Write(writer, stringComparison);
            Assert.AreEqual(text, sb.ToString());
        }

        [TestCase(StringComparison.Ordinal, "Ordinal")]
        [TestCase(StringComparison.InvariantCulture, "InvariantCulture")]
        public void StringWrite(StringComparison stringComparison, string text)
        {
            var sb = new StringBuilder();
            using var writer = new StringWriter(sb);
            EnumFormatter<StringComparison>.String.Write(writer, stringComparison);
            Assert.AreEqual(text, sb.ToString());
        }
    }
}
