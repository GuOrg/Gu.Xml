﻿namespace Gu.Xml.Benchmarks
{
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;
    using BenchmarkDotNet.Attributes;
    using Newtonsoft.Json;

    public class SealedWithSingleIntProperty
    {
        private static readonly WithInt Value = new WithInt { Number = 1 };
        private static readonly XmlSerializer XmlSerializer = new XmlSerializer(typeof(WithInt));
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
