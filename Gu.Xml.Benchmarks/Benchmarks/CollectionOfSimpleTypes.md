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
|             GuXmlSerialize | 16.811 ms | 0.3709 ms | 0.3097 ms |   1.00 |     0.00 |  843.7500 | 218.7500 | 218.7500 |   8.36 MB |
|      StringBuilderToString |  2.959 ms | 0.0589 ms | 0.1389 ms |   0.18 |     0.01 |  195.3125 | 195.3125 | 195.3125 |   4.56 MB |
|     XmlSerializerSerialize | 28.624 ms | 0.5656 ms | 1.1424 ms |   1.70 |     0.07 | 1437.5000 | 750.0000 | 218.7500 |  11.42 MB |
| JsonConvertSerializeObject | 18.603 ms | 0.3952 ms | 0.7024 ms |   1.11 |     0.05 |  562.5000 | 250.0000 |        - |   4.55 MB |
