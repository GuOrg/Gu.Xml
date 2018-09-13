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
|             GuXmlSerialize | 17.675 ms | 0.3620 ms | 1.0675 ms |   1.00 |     0.00 |  843.7500 | 218.7500 | 218.7500 |   8.36 MB |
|      StringBuilderToString |  3.334 ms | 0.1110 ms | 0.3274 ms |   0.19 |     0.02 |  195.3125 | 195.3125 | 195.3125 |   4.56 MB |
|     XmlSerializerSerialize | 27.194 ms | 0.5860 ms | 0.8589 ms |   1.54 |     0.10 | 1437.5000 | 750.0000 | 218.7500 |  11.42 MB |
| JsonConvertSerializeObject | 17.909 ms | 0.0232 ms | 0.0206 ms |   1.02 |     0.06 |  562.5000 | 250.0000 |        - |   4.55 MB |
