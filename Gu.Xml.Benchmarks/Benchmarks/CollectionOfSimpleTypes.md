``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Xeon CPU E5-2637 v4 3.50GHz (Max: 3.49GHz), 1 CPU, 8 logical and 4 physical cores
Frequency=3410074 Hz, Resolution=293.2488 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|                     Method |      Mean |     Error |    StdDev | Scaled | ScaledSD |     Gen 0 |    Gen 1 |    Gen 2 | Allocated |
|--------------------------- |----------:|----------:|----------:|-------:|---------:|----------:|---------:|---------:|----------:|
|             GuXmlSerialize | 44.517 ms | 0.2262 ms | 0.1766 ms |   1.00 |     0.00 |  909.0909 |        - |        - |  10.65 MB |
|      StringBuilderToString |  2.892 ms | 0.0573 ms | 0.1117 ms |   0.06 |     0.00 |  195.3125 | 195.3125 | 195.3125 |   4.56 MB |
|     XmlSerializerSerialize | 29.391 ms | 0.5870 ms | 1.2761 ms |   0.66 |     0.03 | 1437.5000 | 750.0000 | 218.7500 |  11.42 MB |
| JsonConvertSerializeObject | 18.723 ms | 0.4499 ms | 0.3989 ms |   0.42 |     0.01 |  562.5000 | 250.0000 |        - |   4.55 MB |
