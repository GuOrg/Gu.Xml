``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.285 (1803/April2018Update/Redstone4)
Intel Core i7-7500U CPU 2.70GHz (Max: 0.80GHz) (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
Frequency=2835934 Hz, Resolution=352.6175 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT


```
|                     Method |        Mean |      Error |    StdDev |      Median | Scaled | ScaledSD |  Gen 0 | Allocated |
|--------------------------- |------------:|-----------:|----------:|------------:|-------:|---------:|-------:|----------:|
|             GuXmlSerialize | 1,034.87 ns | 20.2609 ns | 30.941 ns | 1,026.66 ns |   1.00 |     0.00 | 0.1507 |     320 B |
|      StringBuilderToString |    29.27 ns |  0.6220 ns |  1.122 ns |    29.06 ns |   0.03 |     0.00 | 0.0915 |     192 B |
|     XmlSerializerSerialize | 3,106.94 ns | 65.3262 ns | 77.766 ns | 3,075.69 ns |   3.00 |     0.11 | 1.9150 |    4024 B |
| JsonConvertSerializeObject |   649.92 ns | 25.6028 ns | 73.459 ns |   620.24 ns |   0.63 |     0.07 | 0.5980 |    1256 B |
