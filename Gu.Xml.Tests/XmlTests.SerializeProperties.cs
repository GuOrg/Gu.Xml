namespace Gu.Xml.Tests
{
    using System;
    using System.Diagnostics;
    using NUnit.Framework;

    public partial class XmlTests
    {
        public class SerializeProperties
        {
            private static readonly TestCaseData[] Values =
            {
                new TestCaseData(new WithPublicGetSet { Value = 1 }),
                new TestCaseData(new SerializeWithXmlAttributes.PropertyWithXmlIgnore { Value = 1 }),
                new TestCaseData(new WithGetSetAndCalculated { Value = 1 }),
            };

            [TestCaseSource(nameof(Values))]
            public void Serialize(object value)
            {
                var expected = Reference.Xml(value);
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
            public void InternalGetSet()
            {
                var with = new InternalWithGetSet { Value = 1 };
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<InternalWithGetSet>" + Environment.NewLine +
                               "  <Value>1</Value>" + Environment.NewLine +
                               "</InternalWithGetSet>";

                var actual = Xml.Serialize(with);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void GetPrivateSet()
            {
                var with = new WithGetPrivateSet(1);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<WithGetPrivateSet>" + Environment.NewLine +
                               "  <Value>1</Value>" + Environment.NewLine +
                               "</WithGetPrivateSet>";

                var actual = Xml.Serialize(with);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void GetOnly()
            {
                var with = new WithGetOnly(1);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<WithGetOnly>" + Environment.NewLine +
                               "  <Value>1</Value>" + Environment.NewLine +
                               "</WithGetOnly>";

                var actual = Xml.Serialize(with);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ExcludeCalculated()
            {
                var with = new WithGetOnlyAndCalculated(1);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<WithGetOnlyAndCalculated>" + Environment.NewLine +
                               "  <Value>1</Value>" + Environment.NewLine +
                               "</WithGetOnlyAndCalculated>";

                var actual = Xml.Serialize(with);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ExcludePrivateProtectedAndStatic()
            {
                var with = new WithPrivateProtectedStatic();
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<WithPrivateProtectedStatic />";

                var actual = Xml.Serialize(with);
                Assert.AreEqual(expected, actual);
            }

            public class WithPublicGetSet
            {
                public int Value { get; set; }
            }

            internal class InternalWithGetSet
            {
                internal int Value { get; set; }
            }

            public class WithGetPrivateSet
            {
                public WithGetPrivateSet(int value)
                {
                    this.Value = value;
                }

                public int Value { get; private set; }
            }

            public class WithGetOnly
            {
                public WithGetOnly(int value)
                {
                    this.Value = value;
                }

                public int Value { get; }
            }

            public class WithGetSetAndCalculated
            {
                public int Value { get; set; }

                // ReSharper disable once UnusedMember.Global
                public int Negated => -this.Value;
            }

            public class WithGetOnlyAndCalculated
            {
                public WithGetOnlyAndCalculated(int value)
                {
                    this.Value = value;
                }

                public int Value { get; }

                // ReSharper disable once UnusedMember.Global
                public int Negated => -this.Value;
            }

            public class WithPrivateProtectedStatic
            {
                public WithPrivateProtectedStatic()
                {
                    StaticValue = 1;
                    this.PrivateValue = 2;
                    this.ProtectedValue = 3;
                }

                public static int StaticValue { get; set; } = 1;

                private int PrivateValue { get; set; } = 2;

                protected int ProtectedValue { get; set; } = 3;
            }
        }
    }
}