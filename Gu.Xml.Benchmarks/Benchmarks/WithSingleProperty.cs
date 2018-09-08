namespace Gu.Xml.Benchmarks.Benchmarks
{
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;
    using BenchmarkDotNet.Attributes;
    using Newtonsoft.Json;

    public class WithSingleProperty
    {
        private static readonly WithInt Value = new WithInt { Value = 1 };
        private static readonly XmlSerializer XmlSerializer = new XmlSerializer(Value.GetType());

        [Benchmark(Baseline = true)]
        public string GuXmlSerialize()
        {
            return Xml.Serialize(Value);
        }

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

        public class WithInt
        {
            public int Value { get; set; }
        }
    }
}
