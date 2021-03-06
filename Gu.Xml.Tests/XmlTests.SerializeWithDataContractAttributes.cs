﻿// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable SA1201 // Elements should appear in the correct order
namespace Gu.Xml.Tests
{
    using System;
    using System.Diagnostics;
    using System.Runtime.Serialization;
    using NUnit.Framework;

    public partial class XmlTests
    {
        public class SerializeWithDataContractAttributes
        {
            private static readonly TestCaseData[] TestCases =
            {
                new TestCaseData(new WithDataContractAttribute { Value = 1 }),
                new TestCaseData(new WithDataContractAttributeExplicitName { Value = 1 }),
                new TestCaseData(new PropertyWithIgnoreDataMemberAttribute { Value = 1 }),
                new TestCaseData(new PropertyWithDataMemberAttribute { Value = 1 }),
                new TestCaseData(new PropertyWithDataMemberAttributeExplicitName { Value = 1 }),
                new TestCaseData(new ExplicitInterfaceWithDataMemberAttribute()),
                new TestCaseData(new FieldWithIgnoreDataMemberAttribute { Value = 1 }),
                new TestCaseData(new FieldWithDataMemberAttribute { Value = 1 }),
                new TestCaseData(new FieldWithDataMemberAttributeExplicitName { Value = 1 }),
                new TestCaseData(new WithWithEnumMemberAttribute { Value = WithEnumMemberAttribute.One }),
                new TestCaseData(new WithWithEnumMemberAttribute { Value = WithEnumMemberAttribute.Two }),
            };

            [TestCaseSource(nameof(TestCases))]
            public void Serialize(object value)
            {
                var expected = Reference.DataContractSerializer(value)
                                        .Replace("XmlTests.SerializeWithDataContractAttributes.", string.Empty, StringComparison.Ordinal);
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

            [DataContract]
            public class WithDataContractAttribute
            {
                [DataMember]
                public int Value { get; set; } = 1;
            }

            [DataContract(Name = "Name")]
            public class WithDataContractAttributeExplicitName
            {
                [DataMember]
                public int Value { get; set; } = 1;
            }

            [DataContract]
            public class PropertyWithDataMemberAttribute
            {
                [DataMember]
                public int Value { get; set; }
            }

            [DataContract]
            public class PropertyWithDataMemberAttributeExplicitName
            {
                [DataMember(Name = "Name")]
                public int Value { get; set; }
            }

            [DataContract]
            public class FieldWithDataMemberAttribute
            {
                [DataMember]
                public int Value = 1;
            }

            [DataContract]
            public class FieldWithDataMemberAttributeExplicitName
            {
                [DataMember(Name = "Name")]
                public int Value = 1;
            }

            public interface IValue
            {
                // ReSharper disable once UnusedMember.Global
                // ReSharper disable UnusedMemberInSuper.Global
                [DataMember(Name = "Value")]
                int Value { get; set; }
                //// ReSharper restore UnusedMemberInSuper.Global
            }

            [DataContract]
            public class ExplicitInterfaceWithDataMemberAttribute : IValue
            {
                [DataMember(Name = "Value")]
                int IValue.Value { get; set; }
            }

            [DataContract]
            public class PropertyWithIgnoreDataMemberAttribute
            {
                [IgnoreDataMember]
                public int Value { get; set; }
            }

            [DataContract]
            public class FieldWithIgnoreDataMemberAttribute
            {
                [IgnoreDataMember]
                public int Value { get; set; }
            }

            [DataContract]
            public class WithWithEnumMemberAttribute
            {
                [DataMember]
                public WithEnumMemberAttribute Value { get; set; }
            }

            [DataContract]
            public enum WithEnumMemberAttribute
            {
                [EnumMember(Value = "Single")]
                One,
                [EnumMember(Value = "Double")]
                Two,
            }
        }
    }
}
