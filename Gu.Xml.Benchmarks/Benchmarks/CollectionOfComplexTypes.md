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
|             GuXmlSerialize | 54.722 ms | 0.8273 ms | 0.7739 ms |   1.00 |     0.00 | 1400.0000 |         - |        - |  19.62 MB |
|      StringBuilderToString |  6.882 ms | 0.1385 ms | 0.3720 ms |   0.13 |     0.01 |  226.5625 |  226.5625 | 226.5625 |  10.47 MB |
|     XmlSerializerSerialize | 72.722 ms | 1.4383 ms | 3.8141 ms |   1.33 |     0.07 | 2714.2857 | 1285.7143 | 428.5714 |   24.8 MB |
| JsonConvertSerializeObject | 37.548 ms | 0.2716 ms | 0.2268 ms |   0.69 |     0.01 |         - |         - |        - |   8.76 MB |
