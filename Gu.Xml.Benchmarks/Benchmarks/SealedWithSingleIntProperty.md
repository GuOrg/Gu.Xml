``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.285 (1803/April2018Update/Redstone4)
Intel Core i7-7500U CPU 2.70GHz (Max: 0.80GHz) (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
Frequency=2835934 Hz, Resolution=352.6175 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT


```
|                     Method |        Mean |      Error |      StdDev |      Median | Scaled | ScaledSD |  Gen 0 | Allocated |
|--------------------------- |------------:|-----------:|------------:|------------:|-------:|---------:|-------:|----------:|
|             GuXmlSerialize | 1,122.27 ns | 92.7682 ns | 273.5293 ns | 1,006.52 ns |   1.00 |     0.00 | 0.1516 |     320 B |
|      StringBuilderToString |    28.93 ns |  0.5151 ns |   0.4301 ns |    28.87 ns |   0.03 |     0.01 | 0.0914 |     192 B |
|     XmlSerializerSerialize | 2,837.88 ns | 14.6999 ns |  13.7503 ns | 2,836.66 ns |   2.67 |     0.60 | 1.8768 |    3944 B |
| JsonConvertSerializeObject |   524.49 ns |  9.5231 ns |   8.9079 ns |   520.85 ns |   0.49 |     0.11 | 0.6094 |    1280 B |
