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
|             GuXmlSerialize | 56.346 ms | 1.1175 ms | 1.0975 ms |   1.00 |     0.00 | 1100.0000 |         - |        - |  17.33 MB |
|      StringBuilderToString |  9.518 ms | 0.3957 ms | 1.1667 ms |   0.17 |     0.02 |  187.5000 |  187.5000 | 187.5000 |  10.47 MB |
|     XmlSerializerSerialize | 65.138 ms | 0.3789 ms | 0.3164 ms |   1.16 |     0.02 | 2625.0000 | 1250.0000 | 375.0000 |   24.8 MB |
| JsonConvertSerializeObject | 41.509 ms | 0.1980 ms | 0.1755 ms |   0.74 |     0.01 |         - |         - |        - |   8.76 MB |
