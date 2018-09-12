``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Xeon CPU E5-2637 v4 3.50GHz (Max: 3.49GHz), 1 CPU, 8 logical and 4 physical cores
Frequency=3410074 Hz, Resolution=293.2488 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|                     Method |      Mean |     Error |    StdDev | Scaled | ScaledSD |     Gen 0 |     Gen 1 |    Gen 2 | Allocated |
|--------------------------- |----------:|----------:|----------:|-------:|---------:|----------:|----------:|---------:|----------:|
|             GuXmlSerialize | 60.210 ms | 1.1499 ms | 0.9603 ms |   1.00 |     0.00 | 1444.4444 |         - |        - |  19.62 MB |
|      StringBuilderToString |  6.868 ms | 0.1369 ms | 0.3436 ms |   0.11 |     0.01 |  187.5000 |  187.5000 | 187.5000 |  10.47 MB |
|     XmlSerializerSerialize | 69.846 ms | 1.4699 ms | 3.5780 ms |   1.16 |     0.06 | 2625.0000 | 1250.0000 | 375.0000 |   24.8 MB |
| JsonConvertSerializeObject | 36.607 ms | 0.2092 ms | 0.1957 ms |   0.61 |     0.01 |         - |         - |        - |   8.76 MB |
