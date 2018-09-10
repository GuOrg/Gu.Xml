#pragma warning disable SA1201 // Elements should appear in the correct order
namespace Gu.Xml.Tests
{
    using System;
    using System.Diagnostics;
    using NUnit.Framework;

    public partial class XmlTests
    {
        public class SerializeComplexTypes
        {
            private static readonly TestCaseData[] Values =
            {
                new TestCaseData(new WithPublicGetSet { Value = 1 }),
                new TestCaseData(new WithPublicMutableField { Value = 1 }),
                new TestCaseData(new WithTwoPublicMutableProperties { Value1 = 1, Value2 = 2 }),
                new TestCaseData(new WithFieldBeforeProperty { Value1 = 1, Value2 = 2 }),
                new TestCaseData(new WithPropertyBeforeField { Value1 = 1, Value2 = 2 }),
                new TestCaseData(new ConcreteWithProperties { Value1 = 1, Value2 = 2, Value3 = 3, Value4 = 4 }),
                new TestCaseData(new ConcreteWithFields { Value1 = 1, Value2 = 2, Value3 = 3, Value4 = 4 }),
                new TestCaseData(new Virtual { Value = 1 }),
                new TestCaseData(new Override { Value = 1 }),
                new TestCaseData(new WithExplicitInterface()),
                new TestCaseData(new WithImplicitInterface { Value = 1 }),
                new TestCaseData(new WithExplicitGetOnly()),
                new TestCaseData(new GenericWithPublicMutableProperty<int> { Value = 1 }),
                new TestCaseData(new GenericWithPublicMutableProperty<string>()),
                new TestCaseData(new GenericWithPublicMutableProperty<string> { Value = "abc" }),
                new TestCaseData(new GenericWithPublicMutableProperty<GenericWithPublicMutableProperty<double>> { Value = new GenericWithPublicMutableProperty<double>() }),
                new TestCaseData(new GenericWithPublicMutableProperty<WithTwoPublicMutableProperties>()),
                new TestCaseData(new GenericWithPublicMutableProperty<WithTwoPublicMutableProperties> { Value = new WithTwoPublicMutableProperties { Value1 = 1, Value2 = 2 } }),
            };

            [TestCaseSource(nameof(Values))]
            public void Serialize(object value)
            {
                var expected = Reference.XmlSerializer(value);
                var actual = Xml.Serialize(value);
                if (actual == expected)
                {
                    if (Debugger.IsAttached)
                    {
                        Console.WriteLine(expected);
                    }

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

            [Test]
            public void WithoutDefaultConstructor()
            {
                var value = new WithoutDefaultCtor(1);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<WithoutDefaultCtor>" + Environment.NewLine +
                               "  <Value>1</Value>" + Environment.NewLine +
                               "</WithoutDefaultCtor>";

                var actual = Xml.Serialize(value);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void TupleOfInt32String()
            {
                var value = Tuple.Create(1, "a");
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<TupleOfInt32String>" + Environment.NewLine +
                               "  <Item1>1</Item1>" + Environment.NewLine +
                               "  <Item2>a</Item2>" + Environment.NewLine +
                               "</TupleOfInt32String>";
                var actual = Xml.Serialize(value);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void AnonymousOfInt32String()
            {
                var value = new { Number = 1, Text = "a" };
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<AnonymousOfInt32String>" + Environment.NewLine +
                               "  <Number>1</Number>" + Environment.NewLine +
                               "  <Text>a</Text>" + Environment.NewLine +
                               "</AnonymousOfInt32String>";
                var actual = Xml.Serialize(value);
                Assert.AreEqual(expected, actual);
            }

            public class WithoutDefaultCtor
            {
                public WithoutDefaultCtor(int value)
                {
                    this.Value = value;
                }

                public int Value { get; }
            }

            public class WithPublicGetSet
            {
                public int Value { get; set; } = 1;
            }

            public class WithPublicMutableField
            {
                public int Value = 1;
            }

            public class GenericWithPublicMutableProperty<T>
            {
                public T Value { get; set; } = default(T);
            }

            public class WithTwoPublicMutableProperties
            {
                public int Value1 { get; set; } = 1;

                public int Value2 { get; set; } = 2;
            }

            public class WithFieldBeforeProperty
            {
                public int Value1 = 1;

                public int Value2 { get; set; } = 2;
            }

            public class WithPropertyBeforeField
            {
                public int Value2 { get; set; } = 2;

                public int Value1 = 1;
            }

            public abstract class AbstractWithProperty
            {
                public int Value1 { get; set; } = 1;

                public int Value2 { get; set; } = 2;
            }

            public class ConcreteWithProperties : AbstractWithProperty
            {
                public int Value3 { get; set; } = 3;

                public int Value4 { get; set; } = 3;
            }

            public abstract class AbstractWithField
            {
                public int Value1 = 1;
                public int Value2 = 2;
            }

            public class ConcreteWithFields : AbstractWithField
            {
                public int Value3 = 3;
                public int Value4 = 4;
            }

            public class Virtual
            {
                public virtual int Value { get; set; } = 1;
            }

            public class Override : Virtual
            {
                public override int Value { get; set; } = 2;
            }

            public interface IValue
            {
                // ReSharper disable once UnusedMember.Global
                int Value { get; set; }
            }

            public class WithExplicitInterface : IValue
            {
                int IValue.Value { get; set; }
            }

            public class WithImplicitInterface : IValue
            {
                public int Value { get; set; }
            }

            public interface IGetOnlyValue
            {
                // ReSharper disable once UnusedMember.Global
                int Value { get; }
            }

            public class WithExplicitGetOnly : IGetOnlyValue
            {
                int IGetOnlyValue.Value => 1;
            }
        }
    }
}