``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Xeon CPU E5-2637 v4 3.50GHz (Max: 3.49GHz), 1 CPU, 8 logical and 4 physical cores
Frequency=3410074 Hz, Resolution=293.2488 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|                     Method |     Mean |     Error |    StdDev |   Median | Scaled |     Gen 0 |     Gen 1 |    Gen 2 | Allocated |
|--------------------------- |---------:|----------:|----------:|---------:|-------:|----------:|----------:|---------:|----------:|
|             GuXmlSerialize | 87.81 ms | 0.1984 ms | 0.1657 ms | 87.83 ms |   1.00 | 2000.0000 |         - |        - |  23.43 MB |
|      StringBuilderToString | 10.12 ms | 0.2517 ms | 0.7423 ms | 10.31 ms |   0.12 |  187.5000 |  187.5000 | 187.5000 |  10.47 MB |
|     XmlSerializerSerialize | 65.80 ms | 1.2452 ms | 1.1038 ms | 65.26 ms |   0.75 | 2625.0000 | 1250.0000 | 375.0000 |   24.8 MB |
| JsonConvertSerializeObject | 45.08 ms | 0.3395 ms | 0.3176 ms | 45.01 ms |   0.51 |         - |         - |        - |   8.76 MB |
