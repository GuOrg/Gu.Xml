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
|             GuXmlSerialize | 56.643 ms | 1.1302 ms | 1.0019 ms |   1.00 |     0.00 | 1100.0000 |         - |        - |  17.33 MB |
|      StringBuilderToString |  7.085 ms | 0.1892 ms | 0.5519 ms |   0.13 |     0.01 |  226.5625 |  226.5625 | 226.5625 |  10.47 MB |
|     XmlSerializerSerialize | 72.751 ms | 1.6443 ms | 4.8483 ms |   1.28 |     0.09 | 2714.2857 | 1285.7143 | 428.5714 |   24.8 MB |
| JsonConvertSerializeObject | 41.265 ms | 0.1889 ms | 0.1767 ms |   0.73 |     0.01 |         - |         - |        - |   8.76 MB |
