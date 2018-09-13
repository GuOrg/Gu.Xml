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
|             GuXmlSerialize | 38.029 ms | 0.7550 ms | 1.7797 ms | 37.063 ms |   1.00 |     0.00 |  642.8571 |   71.4286 |  71.4286 |   8.36 MB |
|      StringBuilderToString |  3.320 ms | 0.0908 ms | 0.2678 ms |  3.361 ms |   0.09 |     0.01 |  195.3125 |  195.3125 | 195.3125 |   4.56 MB |
|     XmlSerializerSerialize | 66.106 ms | 1.7663 ms | 1.9632 ms | 65.346 ms |   1.74 |     0.09 | 3125.0000 | 1375.0000 | 500.0000 |  25.55 MB |
| JsonConvertSerializeObject | 16.072 ms | 0.3147 ms | 0.5754 ms | 15.764 ms |   0.42 |     0.02 |  156.2500 |   62.5000 |        - |   2.26 MB |
