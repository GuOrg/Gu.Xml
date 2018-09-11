``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Xeon CPU E5-2637 v4 3.50GHz (Max: 3.49GHz), 1 CPU, 8 logical and 4 physical cores
Frequency=3410068 Hz, Resolution=293.2493 ns, Timer=TSC
.NET Core SDK=2.1.401
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|                     Method |        Mean |     Error |    StdDev | Scaled | ScaledSD |  Gen 0 |  Gen 1 | Allocated |
|--------------------------- |------------:|----------:|----------:|-------:|---------:|-------:|-------:|----------:|
|             GuXmlSerialize |   515.08 ns | 4.5802 ns | 4.0602 ns |   1.00 |     0.00 | 0.0439 |      - |     280 B |
|      StringBuilderToString |    26.49 ns | 0.0738 ns | 0.0576 ns |   0.05 |     0.00 | 0.0305 |      - |     192 B |
|     XmlSerializerSerialize | 2,414.31 ns | 7.5529 ns | 7.0650 ns |   4.69 |     0.04 | 0.6218 | 0.0038 |    3936 B |
| JsonConvertSerializeObject |   492.00 ns | 3.1232 ns | 2.6080 ns |   0.96 |     0.01 | 0.2012 |      - |    1272 B |
