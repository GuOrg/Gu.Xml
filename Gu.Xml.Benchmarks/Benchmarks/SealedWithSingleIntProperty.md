``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.285 (1803/April2018Update/Redstone4)
Intel Core i7-7500U CPU 2.70GHz (Max: 0.80GHz) (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
Frequency=2835934 Hz, Resolution=352.6175 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT


```
|                     Method |        Mean |     Error |    StdDev | Scaled | ScaledSD |  Gen 0 | Allocated |
|--------------------------- |------------:|----------:|----------:|-------:|---------:|-------:|----------:|
|             GuXmlSerialize |   897.50 ns | 26.427 ns | 70.538 ns |   1.00 |     0.00 | 0.1507 |     320 B |
|      StringBuilderToString |    33.20 ns |  1.749 ns |  4.961 ns |   0.04 |     0.01 | 0.0915 |     192 B |
|     XmlSerializerSerialize | 2,737.58 ns | 53.598 ns | 61.723 ns |   3.07 |     0.23 | 1.8768 |    3944 B |
| JsonConvertSerializeObject |   547.35 ns | 11.959 ns | 23.041 ns |   0.61 |     0.05 | 0.6094 |    1280 B |
