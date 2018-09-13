namespace Gu.Xml.Benchmarks
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using BenchmarkDotNet.Attributes;
    using Newtonsoft.Json;

    public class CollectionOfSealedComplexTypes
    {
        private static readonly WithInt[] Value = Enumerable.Range(1, 100000).Select(x => new WithInt { Number = x }).ToArray();
        private static readonly XmlSerializer XmlSerializer = new XmlSerializer(Value.GetType());
        private static readonly StringBuilder StringBuilder = new StringBuilder(Xml.Serialize(Value));

        [Benchmark(Baseline = true)]
        public string GuXmlSerialize()
        {
            return Xml.Serialize(Value);
        }

        [Benchmark]
        public string StringBuilderToString() => StringBuilder.ToString();

        [Benchmark]
        public string XmlSerializerSerialize()
        {
            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            {
                XmlSerializer.Serialize(writer, Value);
            }

            return sb.ToString();
        }

        [Benchmark]
        public string JsonConvertSerializeObject()
        {
            return JsonConvert.SerializeObject(Value);
        }

        public sealed class WithInt
        {
            public int Number { get; set; }
        }
    }
}