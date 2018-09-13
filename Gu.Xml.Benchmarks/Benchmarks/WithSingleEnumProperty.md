``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.285 (1803/April2018Update/Redstone4)
Intel Core i7-7500U CPU 2.70GHz (Max: 0.80GHz) (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
Frequency=2835934 Hz, Resolution=352.6175 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT


```
|                     Method |        Mean |       Error |      StdDev |      Median | Scaled | ScaledSD |  Gen 0 | Allocated |
|--------------------------- |------------:|------------:|------------:|------------:|-------:|---------:|-------:|----------:|
|             GuXmlSerialize |   842.76 ns |  16.5408 ns |  19.0485 ns |   841.94 ns |   1.00 |     0.00 | 0.2050 |     432 B |
|      StringBuilderToString |    35.33 ns |   0.6974 ns |   0.6850 ns |    35.37 ns |   0.04 |     0.00 | 0.1296 |     272 B |
|     XmlSerializerSerialize | 3,089.44 ns | 198.0064 ns | 551.9627 ns | 2,868.63 ns |   3.67 |     0.66 | 1.8997 |    3992 B |
| JsonConvertSerializeObject |   544.71 ns |   9.9386 ns |   9.2965 ns |   544.37 ns |   0.65 |     0.02 | 0.6132 |    1288 B |
