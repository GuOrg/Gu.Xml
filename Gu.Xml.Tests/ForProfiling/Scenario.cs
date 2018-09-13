namespace Gu.Xml.Tests.ForProfiling
{
    using System.Linq;
    using NUnit.Framework;

    [Explicit("For profiling.")]
    public class Scenario
    {
        private static readonly WithInt[] Value = Enumerable.Range(1, 1000).Select(x => new WithInt { Number = x }).ToArray();

        [Test]
        public void Run()
        {
            for (var i = 0; i < 100; i++)
            {
                // ReSharper disable once AssignmentIsFullyDiscarded
                _ = Xml.Serialize(Value);
            }
        }

        public class WithInt
        {
            public int Number { get; set; }
        }
    }
}
