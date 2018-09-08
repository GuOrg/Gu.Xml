``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.254 (1803/April2018Update/Redstone4)
Intel Core i7-7500U CPU 2.70GHz (Max: 0.80GHz) (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
Frequency=2835943 Hz, Resolution=352.6164 ns, Timer=TSC
.NET Core SDK=2.1.401
  [Host]     : .NET Core 2.1.3 (CoreCLR 4.6.26725.06, CoreFX 4.6.26725.05), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.3 (CoreCLR 4.6.26725.06, CoreFX 4.6.26725.05), 64bit RyuJIT


```
|                     Method |     Mean |    Error |   StdDev |   Median | Scaled | ScaledSD |  Gen 0 | Allocated |
|--------------------------- |---------:|---------:|---------:|---------:|-------:|---------:|-------:|----------:|
|             GuXmlSerialize | 606.0 ns | 20.90 ns | 60.97 ns | 584.6 ns |   1.00 |     0.00 | 0.1326 |     280 B |
|     XmlSerializerSerialize |       NA |       NA |       NA |       NA |      ? |        ? |    N/A |       N/A |
| JsonConvertSerializeObject | 583.8 ns | 11.98 ns | 12.30 ns | 581.0 ns |   0.97 |     0.09 | 0.6056 |    1272 B |

Benchmarks with issues:
  WithSingleProperty.XmlSerializerSerialize: DefaultJob
