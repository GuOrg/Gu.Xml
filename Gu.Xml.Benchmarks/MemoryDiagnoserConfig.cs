[assembly: BenchmarkDotNet.Attributes.Config(typeof(Gu.Xml.Benchmarks.MemoryDiagnoserConfig))]

namespace Gu.Xml.Benchmarks
{
    using BenchmarkDotNet.Configs;
    using BenchmarkDotNet.Diagnosers;

    public class MemoryDiagnoserConfig : ManualConfig
    {
        public MemoryDiagnoserConfig()
        {
            this.Add(MemoryDiagnoser.Default);
        }
    }
}