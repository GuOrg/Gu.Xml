``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Xeon CPU E5-2637 v4 3.50GHz (Max: 3.49GHz), 1 CPU, 8 logical and 4 physical cores
Frequency=3410074 Hz, Resolution=293.2488 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|                     Method |        Mean |      Error |    StdDev | Scaled | ScaledSD |  Gen 0 |  Gen 1 | Allocated |
|--------------------------- |------------:|-----------:|----------:|-------:|---------:|-------:|-------:|----------:|
|             GuXmlSerialize |   838.26 ns |  3.0250 ns |  2.830 ns |   1.00 |     0.00 | 0.0629 |      - |     400 B |
|      StringBuilderToString |    32.93 ns |  0.6542 ns |  1.307 ns |   0.04 |     0.00 | 0.0432 |      - |     272 B |
|     XmlSerializerSerialize | 2,858.91 ns | 10.6577 ns |  8.321 ns |   3.41 |     0.01 | 0.6294 | 0.0038 |    3984 B |
| JsonConvertSerializeObject |   520.11 ns | 10.3841 ns | 17.912 ns |   0.62 |     0.02 | 0.2041 |      - |    1288 B |
