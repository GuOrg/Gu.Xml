``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.254 (1803/April2018Update/Redstone4)
Intel Core i7-7500U CPU 2.70GHz (Max: 0.80GHz) (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
Frequency=2835943 Hz, Resolution=352.6164 ns, Timer=TSC
.NET Core SDK=2.1.401
  [Host]     : .NET Core 2.1.3 (CoreCLR 4.6.26725.06, CoreFX 4.6.26725.05), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.3 (CoreCLR 4.6.26725.06, CoreFX 4.6.26725.05), 64bit RyuJIT


```
|                     Method |        Mean |      Error |     StdDev | Scaled | ScaledSD |  Gen 0 | Allocated |
|--------------------------- |------------:|-----------:|-----------:|-------:|---------:|-------:|----------:|
|             GuXmlSerialize |   598.65 ns |  9.7937 ns |  9.1610 ns |   1.00 |     0.00 | 0.1211 |     256 B |
|      StringBuilderToString |    29.98 ns |  0.4597 ns |  0.4075 ns |   0.05 |     0.00 | 0.0915 |     192 B |
|     XmlSerializerSerialize | 2,916.28 ns | 49.6134 ns | 46.4084 ns |   4.87 |     0.10 | 1.8768 |    3944 B |
| JsonConvertSerializeObject |   562.53 ns |  9.9019 ns |  9.2622 ns |   0.94 |     0.02 | 0.6056 |    1272 B |
