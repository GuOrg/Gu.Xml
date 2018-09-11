``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.254 (1803/April2018Update/Redstone4)
Intel Core i7-7500U CPU 2.70GHz (Max: 0.80GHz) (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
Frequency=2835934 Hz, Resolution=352.6175 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT


```
|                     Method |        Mean |       Error |     StdDev |      Median | Scaled | ScaledSD |  Gen 0 | Allocated |
|--------------------------- |------------:|------------:|-----------:|------------:|-------:|---------:|-------:|----------:|
|             GuXmlSerialize |   576.42 ns |  11.4113 ns |  10.116 ns |   574.63 ns |   1.00 |     0.00 | 0.1211 |     256 B |
|      StringBuilderToString |    30.07 ns |   0.7882 ns |   1.873 ns |    29.26 ns |   0.05 |     0.00 | 0.0915 |     192 B |
|     XmlSerializerSerialize | 2,874.94 ns | 151.5050 ns | 134.305 ns | 2,843.33 ns |   4.99 |     0.24 | 1.8768 |    3944 B |
| JsonConvertSerializeObject |   547.58 ns |   5.3716 ns |   4.486 ns |   547.74 ns |   0.95 |     0.02 | 0.6056 |    1272 B |
