namespace Gu.Xml.Tests
{
    using System;
    using System.Xml;
    using NUnit.Framework;

    public class XmlTests
    {
        public class Serialize
        {
            private static readonly int[] IntSource = { int.MinValue, -1, -0, 0, 1, int.MaxValue };

            [TestCaseSource(nameof(IntSource))]
            public void WithInt(int value)
            {
                var with = new WithMutableInt { Value = value };
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<WithMutableInt>" + Environment.NewLine +
                              $"  <Value>{XmlConvert.ToString(value)}</Value>" + Environment.NewLine +
                               "</WithMutableInt>";

                var actual = Xml.Serialize(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCaseSource(nameof(IntSource))]
            public void WithBoxedInt(int value)
            {
                var with = new WithMutableBoxed { Value = value };
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<WithMutableBoxed>" + Environment.NewLine +
                               $"  <Value>{XmlConvert.ToString(value)}</Value>" + Environment.NewLine +
                               "</WithMutableBoxed>";

                var actual = Xml.Serialize(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCase(double.NegativeInfinity)]
            [TestCase(double.MinValue)]
            [TestCase(int.MinValue)]
            [TestCase(-1.2)]
            [TestCase(-1)]
            [TestCase(-0.1)]
            [TestCase(-1.2E-123)]
            [TestCase(-0.0)]
            [TestCase(0.0)]
            [TestCase(1.2E-123)]
            [TestCase(0.1)]
            [TestCase(1)]
            [TestCase(1.2)]
            [TestCase(1.2E123)]
            [TestCase(int.MaxValue)]
            [TestCase(double.MaxValue)]
            [TestCase(double.NaN)]
            public void WithDouble(double value)
            {
                var with = new WithMutableDouble { Value = value };
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<WithMutableDouble>" + Environment.NewLine +
                               $"  <Value>{XmlConvert.ToString(value)}</Value>" + Environment.NewLine +
                               "</WithMutableDouble>";

                var actual = Xml.Serialize(with);
                Assert.AreEqual(expected, actual);
            }

            public class WithMutableBoxed
            {
                public object Value { get; set; }
            }

            public class WithMutableInt
            {
                public int Value { get; set; }
            }

            public class WithMutableDouble
            {
                public double Value { get; set; }
            }
        }
    }
}
