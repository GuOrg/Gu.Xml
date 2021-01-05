namespace Gu.Xml.Tests
{
    using System;
    using NUnit.Framework;

    public partial class XmlTests
    {
        [Explicit]
        [TestCase(byte.MinValue)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(byte.MaxValue)]
        public void Byte(byte value)
        {
            var xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                      "<WithMutableOfByte>" + Environment.NewLine +
                     $"  <Value>{value}</Value>" + Environment.NewLine +
                      "</WithMutableOfByte>";
            var actual = Xml.Deserialize<WithMutable<byte>>(xml);
            Assert.AreEqual(value, actual.Value);
        }

        public class WithMutable<T>
        {
            // ReSharper disable once UnusedAutoPropertyAccessor.Global
            public T Value { get; set; }
        }
    }
}
