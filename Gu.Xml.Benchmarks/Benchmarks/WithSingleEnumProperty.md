``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.285 (1803/April2018Update/Redstone4)
Intel Core i7-7500U CPU 2.70GHz (Max: 0.80GHz) (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
Frequency=2835934 Hz, Resolution=352.6175 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT


```
|                     Method |        Mean |      Error |     StdDev | Scaled | ScaledSD |  Gen 0 | Allocated |
|--------------------------- |------------:|-----------:|-----------:|-------:|---------:|-------:|----------:|
|             GuXmlSerialize |   777.68 ns |  7.7709 ns |  7.2689 ns |   1.00 |     0.00 | 0.2050 |     432 B |
|      StringBuilderToString |    34.66 ns |  0.3453 ns |  0.3230 ns |   0.04 |     0.00 | 0.1296 |     272 B |
|     XmlSerializerSerialize | 2,720.33 ns | 44.5068 ns | 34.7480 ns |   3.50 |     0.05 | 1.8997 |    3992 B |
| JsonConvertSerializeObject |   552.93 ns |  9.6413 ns |  8.5468 ns |   0.71 |     0.01 | 0.6132 |    1288 B |
