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
|             GuXmlSerialize | 38.742 ms | 0.7719 ms | 1.8936 ms | 37.597 ms |   1.00 |     0.00 |  642.8571 |   71.4286 |  71.4286 |   8.36 MB |
|      StringBuilderToString |  3.419 ms | 0.0907 ms | 0.2647 ms |  3.421 ms |   0.09 |     0.01 |  195.3125 |  195.3125 | 195.3125 |   4.56 MB |
|     XmlSerializerSerialize | 69.721 ms | 1.4337 ms | 4.2273 ms | 68.862 ms |   1.80 |     0.14 | 3142.8571 | 1428.5714 | 428.5714 |  25.55 MB |
| JsonConvertSerializeObject | 15.877 ms | 0.0390 ms | 0.0304 ms | 15.881 ms |   0.41 |     0.02 |  156.2500 |   62.5000 |        - |   2.26 MB |
