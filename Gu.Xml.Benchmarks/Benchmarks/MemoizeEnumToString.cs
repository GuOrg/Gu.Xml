namespace Gu.Xml.Benchmarks
{
    using System;
    using System.Collections.Concurrent;
    using System.Globalization;
    using BenchmarkDotNet.Attributes;

    public class MemoizeEnumToString
    {
        private static readonly CultureTypes CultureTypes = CultureTypes.NeutralCultures | CultureTypes.SpecificCultures;

        private static readonly ConcurrentDictionary<CultureTypes, string> Cache = new ConcurrentDictionary<CultureTypes, string>();

        [Benchmark(Baseline = true)]
        public string EnumFormat()
        {
            return Enum.Format(typeof(CultureTypes), CultureTypes, "G").Replace(",", string.Empty);
        }

        [Benchmark]
        public string CacheGetOrAdd()
        {
            return Cache.GetOrAdd(CultureTypes, x => Enum.Format(typeof(CultureTypes), CultureTypes, "G").Replace(",", string.Empty));
        }
    }
}
