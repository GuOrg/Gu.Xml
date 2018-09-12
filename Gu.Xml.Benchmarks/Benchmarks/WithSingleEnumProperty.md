``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Xeon CPU E5-2637 v4 3.50GHz (Max: 3.49GHz), 1 CPU, 8 logical and 4 physical cores
Frequency=3410074 Hz, Resolution=293.2488 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|                     Method |        Mean |      Error |      StdDev | Scaled | ScaledSD |  Gen 0 |  Gen 1 | Allocated |
|--------------------------- |------------:|-----------:|------------:|-------:|---------:|-------:|-------:|----------:|
|             GuXmlSerialize |   544.04 ns | 10.8411 ns |  27.9843 ns |   1.00 |     0.00 | 0.0582 |      - |     368 B |
|      StringBuilderToString |    31.88 ns |  0.6147 ns |   0.6832 ns |   0.06 |     0.00 | 0.0432 |      - |     272 B |
|     XmlSerializerSerialize | 2,517.17 ns | 50.0464 ns | 101.0961 ns |   4.64 |     0.30 | 0.6294 | 0.0038 |    3984 B |
| JsonConvertSerializeObject |   552.13 ns | 10.6339 ns |  13.0593 ns |   1.02 |     0.06 | 0.2041 |      - |    1288 B |
