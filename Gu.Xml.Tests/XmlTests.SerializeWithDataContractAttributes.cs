// ReSharper disable UnusedAutoPropertyAccessor.Global
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
            private static readonly TestCaseData[] Values =
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
            };

            [TestCaseSource(nameof(Values))]
            public void Serialize(object value)
            {
                var expected = Reference.DataContractSerializer(value)
                                        .Replace("XmlTests.SerializeWithDataContractAttributes.", string.Empty);
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

            public class PropertyWithDataMemberAttribute
            {
                [DataMember]
                public int Value { get; set; }
            }

            public class PropertyWithDataMemberAttributeExplicitName
            {
                [DataMember(Name = "Name")]
                public int Value { get; set; }
            }

            public class FieldWithDataMemberAttribute
            {
                [DataMember]
                public int Value = 1;
            }

            public class FieldWithDataMemberAttributeExplicitName
            {
                [DataMember(Name = "Name")]
                public int Value = 1;
            }

            public interface IValue
            {
                // ReSharper disable once UnusedMember.Global
                // ReSharper disable UnusedMemberInSuper.Global
                int Value { get; set; }
                // ReSharper restore UnusedMemberInSuper.Global
            }

            public class ExplicitInterfaceWithDataMemberAttribute : IValue
            {
                [DataMember]
                int IValue.Value { get; set; }
            }

            public class PropertyWithIgnoreDataMemberAttribute
            {
                [IgnoreDataMember]
                public int Value { get; set; }
            }

            public class FieldWithIgnoreDataMemberAttribute
            {
                [IgnoreDataMember]
                public int Value { get; set; }
            }
        }
    }
}