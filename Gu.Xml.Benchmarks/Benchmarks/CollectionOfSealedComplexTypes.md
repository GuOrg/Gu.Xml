``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Xeon CPU E5-2637 v4 3.50GHz (Max: 3.49GHz), 1 CPU, 8 logical and 4 physical cores
Frequency=3410074 Hz, Resolution=293.2488 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|                     Method |      Mean |     Error |     StdDev |    Median | Scaled | ScaledSD |     Gen 0 |     Gen 1 |    Gen 2 | Allocated |
|--------------------------- |----------:|----------:|-----------:|----------:|-------:|---------:|----------:|----------:|---------:|----------:|
|             GuXmlSerialize | 39.313 ms | 0.7827 ms |  1.8753 ms | 38.203 ms |   1.00 |     0.00 | 1153.8462 |   76.9231 |  76.9231 |  17.33 MB |
|      StringBuilderToString |  7.680 ms | 0.2300 ms |  0.6708 ms |  7.578 ms |   0.20 |     0.02 |  187.5000 |  187.5000 | 187.5000 |  10.47 MB |
|     XmlSerializerSerialize | 71.243 ms | 1.4725 ms |  4.3416 ms | 70.941 ms |   1.82 |     0.14 | 2625.0000 | 1250.0000 | 375.0000 |   24.8 MB |
| JsonConvertSerializeObject | 47.227 ms | 3.9712 ms | 11.7092 ms | 39.119 ms |   1.20 |     0.30 |         - |         - |        - |   8.76 MB |
