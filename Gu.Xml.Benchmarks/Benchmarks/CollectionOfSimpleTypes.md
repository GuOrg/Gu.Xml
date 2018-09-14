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
|             GuXmlSerialize | 17.782 ms | 0.3541 ms | 1.0384 ms | 17.116 ms |   1.00 |     0.00 |  843.7500 | 218.7500 | 218.7500 |   8.36 MB |
|      StringBuilderToString |  3.153 ms | 0.1396 ms | 0.4073 ms |  3.126 ms |   0.18 |     0.02 |  226.5625 | 226.5625 | 226.5625 |   4.56 MB |
|     XmlSerializerSerialize | 28.226 ms | 0.5576 ms | 1.1006 ms | 27.456 ms |   1.59 |     0.11 | 1437.5000 | 750.0000 | 218.7500 |  11.42 MB |
| JsonConvertSerializeObject | 18.270 ms | 0.3607 ms | 0.5926 ms | 17.922 ms |   1.03 |     0.07 |  562.5000 | 250.0000 |        - |   4.55 MB |
