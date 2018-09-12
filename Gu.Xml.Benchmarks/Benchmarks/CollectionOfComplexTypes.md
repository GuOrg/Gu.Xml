``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Xeon CPU E5-2637 v4 3.50GHz (Max: 3.49GHz), 1 CPU, 8 logical and 4 physical cores
Frequency=3410074 Hz, Resolution=293.2488 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|                     Method |      Mean |     Error |    StdDev |    Median | Scaled | ScaledSD |     Gen 0 |     Gen 1 |    Gen 2 | Allocated |
|--------------------------- |----------:|----------:|----------:|----------:|-------:|---------:|----------:|----------:|---------:|----------:|
|             GuXmlSerialize | 61.950 ms | 0.8911 ms | 0.8335 ms | 62.199 ms |   1.00 |     0.00 |         - |         - |        - |  17.33 MB |
|      StringBuilderToString |  6.917 ms | 0.1366 ms | 0.3220 ms |  6.891 ms |   0.11 |     0.01 |  226.5625 |  226.5625 | 226.5625 |  10.47 MB |
|     XmlSerializerSerialize | 69.029 ms | 1.4319 ms | 4.1995 ms | 66.390 ms |   1.11 |     0.07 | 2625.0000 | 1250.0000 | 375.0000 |   24.8 MB |
| JsonConvertSerializeObject | 36.469 ms | 0.3670 ms | 0.3064 ms | 36.365 ms |   0.59 |     0.01 |         - |         - |        - |   8.76 MB |
