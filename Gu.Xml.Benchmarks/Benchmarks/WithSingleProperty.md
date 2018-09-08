``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Xeon CPU E5-2637 v4 3.50GHz (Max: 3.49GHz), 1 CPU, 8 logical and 4 physical cores
Frequency=3410073 Hz, Resolution=293.2489 ns, Timer=TSC
.NET Core SDK=2.1.401
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|                     Method |     Mean |     Error |     StdDev | Scaled | ScaledSD |  Gen 0 | Allocated |
|--------------------------- |---------:|----------:|-----------:|-------:|---------:|-------:|----------:|
|             GuXmlSerialize | 472.7 ns |  1.018 ns |  0.7950 ns |   1.00 |     0.00 | 0.0439 |     280 B |
|     XmlSerializerSerialize |       NA |        NA |         NA |      ? |        ? |    N/A |       N/A |
| JsonConvertSerializeObject | 521.4 ns | 10.352 ns | 22.5043 ns |   1.10 |     0.05 | 0.2012 |    1272 B |

Benchmarks with issues:
  WithSingleProperty.XmlSerializerSerialize: DefaultJob
