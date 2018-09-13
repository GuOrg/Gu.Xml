``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Xeon CPU E5-2637 v4 3.50GHz (Max: 3.49GHz), 1 CPU, 8 logical and 4 physical cores
Frequency=3410074 Hz, Resolution=293.2488 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|                     Method |      Mean |     Error |    StdDev |    Median | Scaled | ScaledSD |     Gen 0 |    Gen 1 |    Gen 2 | Allocated |
|--------------------------- |----------:|----------:|----------:|----------:|-------:|---------:|----------:|---------:|---------:|----------:|
|             GuXmlSerialize | 28.773 ms | 0.6680 ms | 1.9696 ms | 27.726 ms |   1.00 |     0.00 | 1125.0000 | 125.0000 | 125.0000 |  10.65 MB |
|      StringBuilderToString |  3.047 ms | 0.0602 ms | 0.1188 ms |  3.043 ms |   0.11 |     0.01 |  195.3125 | 195.3125 | 195.3125 |   4.56 MB |
|     XmlSerializerSerialize | 27.267 ms | 0.0655 ms | 0.0512 ms | 27.260 ms |   0.95 |     0.06 | 1437.5000 | 750.0000 | 218.7500 |  11.42 MB |
| JsonConvertSerializeObject | 18.662 ms | 0.3714 ms | 0.7915 ms | 19.085 ms |   0.65 |     0.05 |  562.5000 | 250.0000 |        - |   4.55 MB |
