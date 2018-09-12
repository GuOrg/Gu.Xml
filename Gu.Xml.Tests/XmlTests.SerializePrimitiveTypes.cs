// ReSharper disable MemberCanBePrivate.Global
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
            private static readonly TestCaseData[] DateTimeSource =
            {
                new TestCaseData(System.DateTime.MinValue, "0001-01-01T00:00:00.0000000"),
                new TestCaseData(System.DateTime.ParseExact("2018-09-01T09:43:15.1230000+02:00", "O", NumberFormatInfo.InvariantInfo), "2018-09-01T09:43:15.1230000+02:00"),
                new TestCaseData(System.DateTime.ParseExact("2018-09-02T09:43:15.1230000", "O", NumberFormatInfo.InvariantInfo), "2018-09-02T09:43:15.1230000"),
                new TestCaseData(new DateTime(2018, 09, 03, 09, 43, 15, 123, DateTimeKind.Utc), "2018-09-03T09:43:15.1230000Z"),
                new TestCaseData(System.DateTime.MaxValue, "9999-12-31T23:59:59.9999999"),
            };

            private static readonly TestCaseData[] DateTimeOffsetSource =
            {
                new TestCaseData(System.DateTimeOffset.MinValue, "0001-01-01T00:00:00.0000000+00:00"),
                new TestCaseData(System.DateTimeOffset.ParseExact("2018-09-01T09:43:15.1230000+00:00", "O", NumberFormatInfo.InvariantInfo), "2018-09-01T09:43:15.1230000+00:00"),
                new TestCaseData(System.DateTimeOffset.ParseExact("2018-09-02T09:43:15.1230000+02:00", "O", NumberFormatInfo.InvariantInfo), "2018-09-02T09:43:15.1230000+02:00"),
                new TestCaseData(System.DateTimeOffset.ParseExact("2018-09-03T09:43:15.1230000+00:00", "O", NumberFormatInfo.InvariantInfo), "2018-09-03T09:43:15.1230000+00:00"),
                new TestCaseData(System.DateTimeOffset.MaxValue, "9999-12-31T23:59:59.9999999+00:00"),
            };

            private static readonly decimal[] DecimalSource =
            {
                decimal.MinValue,
                -1,
                decimal.MinusOne,
                -0M,
                0M,
                decimal.Zero,
                1,
                decimal.MaxValue,
            };

            private static readonly int[] IntSource =
            {
                int.MinValue,
                -1,
                -0,
                0,
                1,
                int.MaxValue,
            };

            [TestCase(byte.MinValue)]
            [TestCase(0)]
            [TestCase(1)]
            [TestCase(byte.MaxValue)]
            public void Byte(byte value)
            {
                var with = new WithMutable<byte> { Value = value };
                var actual = Xml.Serialize(with);
                var expected = Reference.XmlSerializer(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCase(true)]
            [TestCase(false)]
            public void Boolean(bool value)
            {
                var with = new WithMutable<bool> { Value = value };
                var actual = Xml.Serialize(with);
                var expected = Reference.XmlSerializer(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCase(true)]
            [TestCase(false)]
            [TestCase(null)]
            public void BooleanNullable(bool? value)
            {
                var with = new WithMutable<bool?> { Value = value };
                var expected = value.HasValue
                    ? "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                      "<WithMutableOfNullableOfBoolean>" + Environment.NewLine +
                     $"  <Value>{XmlConvert.ToString(value.Value)}</Value>" + Environment.NewLine +
                      "</WithMutableOfNullableOfBoolean>"
                    : "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                      "<WithMutableOfNullableOfBoolean />";

                var actual = Xml.Serialize(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCase(true)]
            [TestCase(false)]
            public void BooleanBoxed(bool value)
            {
                var with = new WithMutableBoxed { Value = value };
                var actual = Xml.Serialize(with);
                var expected = Reference.XmlSerializer(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCase(true)]
            [TestCase(false)]
            [TestCase(null)]
            public void BoxedNullableBoolean(bool? value)
            {
                var with = new WithMutableBoxed { Value = value };
                var actual = Xml.Serialize(with);
                var expected = Reference.XmlSerializer(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCase('a')]
            [TestCase('\t')]
            public void Char(char value)
            {
                var with = new WithMutable<char> { Value = value };
                var actual = Xml.Serialize(with);
                var expected = Reference.XmlSerializer(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCaseSource(nameof(DateTimeSource))]
            public void DateTime(DateTime value, string text)
            {
                var with = new WithMutable<DateTime> { Value = value };
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<WithMutableOfDateTime>" + Environment.NewLine +
                              $"  <Value>{text}</Value>" + Environment.NewLine +
                               "</WithMutableOfDateTime>";
                var actual = Xml.Serialize(with);
                Dump.Xml(actual);

                Assert.AreEqual(expected, actual);
            }

            [TestCaseSource(nameof(DateTimeOffsetSource))]
            public void DateTimeOffset(DateTimeOffset value, string text)
            {
                var with = new WithMutable<DateTimeOffset> { Value = value };
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<WithMutableOfDateTimeOffset>" + Environment.NewLine +
                               $"  <Value>{text}</Value>" + Environment.NewLine +
                               "</WithMutableOfDateTimeOffset>";
                var actual = Xml.Serialize(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCaseSource(nameof(DecimalSource))]
            public void Decimal(decimal value)
            {
                var with = new WithMutable<decimal> { Value = value };
                var expected = Reference.XmlSerializer(with);
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
                var expected = Reference.XmlSerializer(with);
                var actual = Xml.Serialize(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCase(StringComparison.InvariantCulture)]
            [TestCase(StringComparison.CurrentCultureIgnoreCase)]
            public void EnumStringComparison(StringComparison value)
            {
                var with = new WithMutable<StringComparison> { Value = value };
                var actual = Xml.Serialize(with);
                var expected = Reference.XmlSerializer(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCase(StringComparison.InvariantCulture)]
            [TestCase(StringComparison.CurrentCultureIgnoreCase)]
            public void EnumStringComparisonBoxed(StringComparison value)
            {
                var with = new WithMutableBoxed { Value = value };
                var actual = Xml.Serialize(with);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<WithMutableBoxed>" + Environment.NewLine +
                              $"  <Value>{value.ToString()}</Value>" + Environment.NewLine +
                               "</WithMutableBoxed>";
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
                var expected = Reference.XmlSerializer(with);
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
                var expected = Reference.XmlSerializer(with);
                var actual = Xml.Serialize(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCase("00000000-0000-0000-0000-000000000000")]
            [TestCase("9fef4efa-262b-4b4a-a754-446c0a85fd72")]
            public void Guid(string value)
            {
                var with = new WithMutable<Guid> { Value = System.Guid.Parse(value) };
                var actual = Xml.Serialize(with);
                var expected = Reference.XmlSerializer(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCase(short.MinValue)]
            [TestCase(-1)]
            [TestCase(0)]
            [TestCase(1)]
            [TestCase(short.MaxValue)]
            public void Int16(short value)
            {
                var with = new WithMutable<short> { Value = value };
                var actual = Xml.Serialize(with);
                var expected = Reference.XmlSerializer(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCaseSource(nameof(IntSource))]
            public void Int32(int value)
            {
                var with = new WithMutable<int> { Value = value };
                var actual = Xml.Serialize(with);
                var expected = Reference.XmlSerializer(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCaseSource(nameof(IntSource))]
            public void Int32Nullable(int value)
            {
                var with = new WithMutable<int?> { Value = value };
                var actual = Xml.Serialize(with);
                var expected = Reference.XmlSerializer(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCaseSource(nameof(IntSource))]
            public void Int32Boxed(int value)
            {
                var with = new WithMutableBoxed { Value = value };
                var actual = Xml.Serialize(with);
                var expected = Reference.XmlSerializer(with);
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
                var expected = Reference.XmlSerializer(with);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void Object()
            {
                var with = new WithMutable<object> { Value = new object() };
                var actual = Xml.Serialize(with);
                var expected = Reference.XmlSerializer(with);
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
                var expected = Reference.XmlSerializer(with);
                Assert.AreEqual(expected, actual);
            }

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
                var expected = Reference.XmlSerializer(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCase(ushort.MinValue)]
            [TestCase(ushort.MaxValue)]
            public void UInt16(ushort value)
            {
                var with = new WithMutable<ushort> { Value = value };
                var actual = Xml.Serialize(with);
                var expected = Reference.XmlSerializer(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCase(uint.MinValue)]
            [TestCase(0u)]
            [TestCase(1u)]
            [TestCase(uint.MaxValue)]
            public void UInt32(uint value)
            {
                var with = new WithMutable<uint> { Value = value };
                var actual = Xml.Serialize(with);
                var expected = Reference.XmlSerializer(with);
                Assert.AreEqual(expected, actual);
            }

            [TestCase(ulong.MinValue)]
            [TestCase(ulong.MaxValue)]
            public void UInt64(ulong value)
            {
                var with = new WithMutable<ulong> { Value = value };
                var actual = Xml.Serialize(with);
                var expected = Reference.XmlSerializer(with);
                Assert.AreEqual(expected, actual);
            }

            public class WithMutableBoxed
            {
                // ReSharper disable once UnusedAutoPropertyAccessor.Global
                public object Value { get; set; }
            }

            public class WithMutable<T>
            {
                // ReSharper disable once UnusedAutoPropertyAccessor.Global
                public T Value { get; set; }
            }
        }
    }
}
