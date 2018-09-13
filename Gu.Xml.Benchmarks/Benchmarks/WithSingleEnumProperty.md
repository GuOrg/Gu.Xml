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
|             GuXmlSerialize |   821.33 ns | 15.9966 ns | 13.3579 ns |   1.00 |     0.00 | 0.0677 |      - |     432 B |
|      StringBuilderToString |    32.35 ns |  0.0709 ns |  0.0592 ns |   0.04 |     0.00 | 0.0432 |      - |     272 B |
|     XmlSerializerSerialize | 2,530.90 ns | 47.4989 ns | 78.0421 ns |   3.08 |     0.10 | 0.6294 | 0.0038 |    3984 B |
| JsonConvertSerializeObject |   581.09 ns |  2.2967 ns |  2.1483 ns |   0.71 |     0.01 | 0.2041 |      - |    1288 B |
