namespace Gu.Xml.Tests
{
    using System;
    using System.Xml;
    using NUnit.Framework;

    public partial class XmlTests
    {
        public class SerializePrimitiveTypes
        {
            private static readonly int[] IntSource = { int.MinValue, -1, -0, 0, 1, int.MaxValue };

            [TestCase(true)]
            [TestCase(false)]
            public void Boolean(bool value)
            {
                var with = new WithMutable<bool> { Value = value };
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<WithMutableOfBoolean>" + Environment.NewLine +
                              $"  <Value>{XmlConvert.ToString(value)}</Value>" + Environment.NewLine +
                               "</WithMutableOfBoolean>";

                var actual = Xml.Serialize(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCase(true)]
            [TestCase(false)]
            [TestCase(null)]
            public void NullableBoolean(bool? value)
            {
                var with = new WithMutableNullable<bool> { Value = value };
                var expected = value.HasValue
                    ? "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                      "<WithMutableNullableOfBoolean>" + Environment.NewLine +
                     $"  <Value>{XmlConvert.ToString(value.Value)}</Value>" + Environment.NewLine +
                      "</WithMutableNullableOfBoolean>"
                    : "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                      "<WithMutableNullableOfBoolean />";

                var actual = Xml.Serialize(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCase(true)]
            [TestCase(false)]
            public void BoxedBoolean(bool value)
            {
                var with = new WithMutableBoxed { Value = value };
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<WithMutableBoxed>" + Environment.NewLine +
                               $"  <Value>{XmlConvert.ToString(value)}</Value>" + Environment.NewLine +
                               "</WithMutableBoxed>";

                var actual = Xml.Serialize(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCase(true)]
            [TestCase(false)]
            [TestCase(null)]
            public void BoxedNullableBoolean(bool? value)
            {
                var with = new WithMutableBoxed { Value = value };
                var expected = value.HasValue
                    ? "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                      "<WithMutableBoxed>" + Environment.NewLine +
                      $"  <Value>{XmlConvert.ToString(value.Value)}</Value>" + Environment.NewLine +
                      "</WithMutableBoxed>"
                    : "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                      "<WithMutableBoxed />";

                var actual = Xml.Serialize(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCaseSource(nameof(IntSource))]
            public void Int32(int value)
            {
                var with = new WithMutable<int> { Value = value };
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<WithMutableOfInt32>" + Environment.NewLine +
                              $"  <Value>{XmlConvert.ToString(value)}</Value>" + Environment.NewLine +
                               "</WithMutableOfInt32>";

                var actual = Xml.Serialize(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCaseSource(nameof(IntSource))]
            public void BoxedInt32(int value)
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
            public void Double(double value)
            {
                var with = new WithMutable<double> { Value = value };
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<WithMutableOfDouble>" + Environment.NewLine +
                               $"  <Value>{XmlConvert.ToString(value)}</Value>" + Environment.NewLine +
                               "</WithMutableOfDouble>";

                var actual = Xml.Serialize(with);
                Assert.AreEqual(expected, actual);
            }

            public class WithMutableBoxed
            {
                public object Value { get; set; }
            }

            public class WithMutable<T>
            {
                public T Value { get; set; }
            }

            public class WithMutableNullable<T> 
                where T : struct 
            {
                public T? Value { get; set; }
            }
        }
    }
}
