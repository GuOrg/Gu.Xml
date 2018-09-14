namespace Gu.Xml.Benchmarks
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;
    using BenchmarkDotNet.Attributes;
    using Newtonsoft.Json;

    public class SmallGraph
    {
        private static readonly Graph Value = new Graph
        {
            Number = 1, 
            Text = "abc", 
            KeyValuePair = new KeyValuePair<int, double>(1, 1.1),
            Node = new Node
            {
                Number = 2,
                Text = "cde",
                KeyValuePair = new KeyValuePair<int, double>(3, 2.1),
            },
        };

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

        public sealed class Graph
        {
            public int Number { get; set; }

            public string Text { get; set; }

            public KeyValuePair<int, double> KeyValuePair { get; set; }

            public Node Node { get; set; }
        }

        public sealed class Node
        {
            public int Number { get; set; }

            public string Text { get; set; }

            public KeyValuePair<int, double> KeyValuePair { get; set; }
        }
    }
}