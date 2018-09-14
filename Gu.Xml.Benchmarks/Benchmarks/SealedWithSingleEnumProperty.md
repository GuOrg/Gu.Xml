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
|             GuXmlSerialize |   391.29 ns |  1.3161 ns |  1.2311 ns |   1.00 |     0.00 | 0.0582 |      - |     368 B |
|      StringBuilderToString |    31.49 ns |  0.0413 ns |  0.0386 ns |   0.08 |     0.00 | 0.0432 |      - |     272 B |
|     XmlSerializerSerialize | 2,536.97 ns | 49.9310 ns | 86.1284 ns |   6.48 |     0.22 | 0.6294 | 0.0038 |    3984 B |
| JsonConvertSerializeObject |   527.82 ns | 11.0342 ns | 23.0325 ns |   1.35 |     0.06 | 0.2041 |      - |    1288 B |
