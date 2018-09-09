namespace Gu.Xml.Tests
{
    using System;
    using System.Globalization;
    using System.Xml;
    using NUnit.Framework;

    public partial class XmlTests
    {
        public class SerializePrimitiveTypes
        {
            private static readonly int[] IntSource = { int.MinValue, -1, -0, 0, 1, int.MaxValue };
            private static readonly decimal[] DecimalSource = { decimal.MinValue, -1, decimal.MinusOne, -0M, 0M, decimal.Zero, 1, decimal.MaxValue };

            [TestCase(null)]
            [TestCase("abc")]
            [TestCase(" abc")]
            [TestCase("abc ")]
            [TestCase("1\u00A0mm")]
            [TestCase("1\u00B0")]
            [TestCase("abc\r\ncde")]
            public void String(string value)
            {
                var with = new WithMutable<string> { Value = value };
                var actual = Xml.Serialize(with);
                var expected = Reference.Xml(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCase(byte.MinValue)]
            [TestCase(0)]
            [TestCase(1)]
            [TestCase(byte.MaxValue)]
            public void Byte(byte value)
            {
                var with = new WithMutable<byte> { Value = value };
                var actual = Xml.Serialize(with);
                var expected = Reference.Xml(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCase(sbyte.MinValue)]
            [TestCase(-1)]
            [TestCase(-0)]
            [TestCase(0)]
            [TestCase(1)]
            [TestCase(sbyte.MaxValue)]
            public void SByte(sbyte value)
            {
                var with = new WithMutable<sbyte> { Value = value };
                var actual = Xml.Serialize(with);
                var expected = Reference.Xml(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCase(true)]
            [TestCase(false)]
            public void Boolean(bool value)
            {
                var with = new WithMutable<bool> { Value = value };
                var actual = Xml.Serialize(with);
                var expected = Reference.Xml(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCase(true)]
            [TestCase(false)]
            [TestCase(null)]
            public void BooleanNullable(bool? value)
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
            public void BooleanBoxed(bool value)
            {
                var with = new WithMutableBoxed { Value = value };
                var actual = Xml.Serialize(with);
                var expected = Reference.Xml(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCase(true)]
            [TestCase(false)]
            [TestCase(null)]
            public void BoxedNullableBoolean(bool? value)
            {
                var with = new WithMutableBoxed { Value = value };
                var actual = Xml.Serialize(with);
                var expected = Reference.Xml(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCase('a')]
            [TestCase('\t')]
            public void Char(char value)
            {
                var with = new WithMutable<char> { Value = value };
                var actual = Xml.Serialize(with);
                var expected = Reference.Xml(with);
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

            [TestCaseSource(nameof(DecimalSource))]
            public void Decimal(decimal value)
            {
                var with = new WithMutable<decimal> { Value = value };
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<WithMutableOfDecimal>" + Environment.NewLine +
                               $"  <Value>{XmlConvert.ToString(value)}</Value>" + Environment.NewLine +
                               "</WithMutableOfDecimal>";

                var actual = Xml.Serialize(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCase(StringComparison.InvariantCulture)]
            [TestCase(StringComparison.CurrentCultureIgnoreCase)]
            public void EnumStringComparison(StringComparison value)
            {
                var with = new WithMutable<StringComparison> { Value = value };
                var actual = Xml.Serialize(with);
                var expected = Reference.Xml(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCase(CultureTypes.NeutralCultures)]
            [TestCase(CultureTypes.SpecificCultures)]
            [TestCase(CultureTypes.NeutralCultures | CultureTypes.SpecificCultures)]
            [TestCase(CultureTypes.AllCultures)]
            public void EnumCultureTypes(CultureTypes value)
            {
                var with = new WithMutable<CultureTypes> { Value = value };
                var actual = Xml.Serialize(with);
                var expected = Reference.Xml(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCase(float.NegativeInfinity)]
            [TestCase(float.MinValue)]
            [TestCase(int.MinValue)]
            [TestCase(-1.2f)]
            [TestCase(-1)]
            [TestCase(-0.1f)]
            [TestCase(-1.2E-34f)]
            [TestCase(-0.0f)]
            [TestCase(0.0f)]
            [TestCase(1.2E-123f)]
            [TestCase(0.1f)]
            [TestCase(1)]
            [TestCase(1.2f)]
            [TestCase(1.2E34f)]
            [TestCase(int.MaxValue)]
            [TestCase(float.MaxValue)]
            [TestCase(float.NaN)]
            public void Float(float value)
            {
                var with = new WithMutable<float> { Value = value };
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<WithMutableOfSingle>" + Environment.NewLine +
                               $"  <Value>{XmlConvert.ToString(value)}</Value>" + Environment.NewLine +
                               "</WithMutableOfSingle>";

                var actual = Xml.Serialize(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCaseSource(nameof(IntSource))]
            public void Int32(int value)
            {
                var with = new WithMutable<int> { Value = value };
                var actual = Xml.Serialize(with);
                var expected = Reference.Xml(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCaseSource(nameof(IntSource))]
            public void Int32Nullable(int value)
            {
                var with = new WithMutable<int?> { Value = value };
                var actual = Xml.Serialize(with);
                var expected = Reference.Xml(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCaseSource(nameof(IntSource))]
            public void Int32Boxed(int value)
            {
                var with = new WithMutableBoxed { Value = value };
                var actual = Xml.Serialize(with);
                var expected = Reference.Xml(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCase(long.MinValue)]
            [TestCase(int.MinValue)]
            [TestCase(-1)]
            [TestCase(-0)]
            [TestCase(0)]
            [TestCase(1)]
            [TestCase(int.MaxValue)]
            [TestCase(long.MaxValue)]
            public void Int64(long value)
            {
                var with = new WithMutable<long> { Value = value };
                var actual = Xml.Serialize(with);
                var expected = Reference.Xml(with);
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
