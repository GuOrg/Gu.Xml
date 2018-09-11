``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.254 (1803/April2018Update/Redstone4)
Intel Core i7-7500U CPU 2.70GHz (Max: 0.80GHz) (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
Frequency=2835934 Hz, Resolution=352.6175 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT


```
|                     Method |        Mean |     Error |     StdDev |      Median | Scaled | ScaledSD |  Gen 0 | Allocated |
|--------------------------- |------------:|----------:|-----------:|------------:|-------:|---------:|-------:|----------:|
|             GuXmlSerialize |   882.39 ns | 17.641 ns |  18.116 ns |   881.56 ns |   1.00 |     0.00 | 0.1678 |     352 B |
|      StringBuilderToString |    40.20 ns |  1.533 ns |   4.249 ns |    38.80 ns |   0.05 |     0.00 | 0.1182 |     248 B |
|     XmlSerializerSerialize | 3,039.87 ns | 88.735 ns | 251.726 ns | 2,925.01 ns |   3.45 |     0.29 | 1.8883 |    3968 B |
| JsonConvertSerializeObject |   620.66 ns | 19.200 ns |  55.702 ns |   605.81 ns |   0.70 |     0.06 | 0.6056 |    1272 B |
