namespace Gu.Xml.Tests
{
    using System.Xml;
    using NUnit.Framework;

    public class XmlTests
    {
        public class Serialize
        {
            [TestCase(int.MinValue)]
            [TestCase(-1)]
            [TestCase(0)]
            [TestCase(1)]
            [TestCase(int.MaxValue)]
            public void WithInt(int value)
            {
                var with = new WithMutableInt { Value = value };
                var expected = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<WithMutableInt>
  <Value>{XmlConvert.ToString(value)}</Value>
</WithMutableInt>";
                var actual = Xml.Serialize(with);
                Assert.AreEqual(expected, actual);
            }

            public class WithMutableInt
            {
                public int Value { get; set; }
            }
        }
    }
}
