namespace Gu.Xml.Tests.ForProfiling
{
    using System.Linq;
    using NUnit.Framework;

    [Explicit("For profiling.")]
    public class Scenario
    {
        private static readonly WithInt[] Values = Enumerable.Range(1, 1000).Select(x => new WithInt { Number = x }).ToArray();
        private static readonly SealedWithIntClass SealedWithInt = new SealedWithIntClass { Number = 1 };

        [SetUp]
        public void SetUp()
        {
            _ = Xml.Serialize(SealedWithInt);
        }

        [Test]
        public void Run()
        {
            for (var i = 0; i < 10000; i++)
            {
                // ReSharper disable once AssignmentIsFullyDiscarded
                _ = Xml.Serialize(Values);
            }
        }

        [Test]
        public void SerializeSealedWithInt()
        {
            _ = Xml.Serialize(SealedWithInt);
        }

        public class WithInt
        {
            public int Number { get; set; }
        }

        public sealed class SealedWithIntClass
        {
            public int Number { get; set; }
        }
    }
}
