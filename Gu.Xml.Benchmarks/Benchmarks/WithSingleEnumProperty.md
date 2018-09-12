``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Xeon CPU E5-2637 v4 3.50GHz (Max: 3.49GHz), 1 CPU, 8 logical and 4 physical cores
Frequency=3410074 Hz, Resolution=293.2488 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|                     Method |        Mean |      Error |     StdDev | Scaled | ScaledSD |  Gen 0 |  Gen 1 | Allocated |
|--------------------------- |------------:|-----------:|-----------:|-------:|---------:|-------:|-------:|----------:|
|             GuXmlSerialize |   524.45 ns |  6.5118 ns |  5.4377 ns |   1.00 |     0.00 | 0.0620 |      - |     392 B |
|      StringBuilderToString |    32.29 ns |  0.6125 ns |  0.5430 ns |   0.06 |     0.00 | 0.0432 |      - |     272 B |
|     XmlSerializerSerialize | 2,431.57 ns | 14.9827 ns | 11.6975 ns |   4.64 |     0.05 | 0.6294 | 0.0038 |    3984 B |
| JsonConvertSerializeObject |   542.63 ns | 10.7992 ns | 16.4915 ns |   1.03 |     0.03 | 0.2041 |      - |    1288 B |
