``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.285 (1803/April2018Update/Redstone4)
Intel Core i7-7500U CPU 2.70GHz (Max: 0.80GHz) (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
Frequency=2835934 Hz, Resolution=352.6175 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT


```
|                     Method |      Mean |     Error |    StdDev |    Median | Scaled | ScaledSD |     Gen 0 |    Gen 1 |    Gen 2 | Allocated |
|--------------------------- |----------:|----------:|----------:|----------:|-------:|---------:|----------:|---------:|---------:|----------:|
|             GuXmlSerialize | 11.947 ms | 0.1947 ms | 0.1821 ms | 11.988 ms |   1.00 |     0.00 | 2171.8750 | 281.2500 | 281.2500 |   8.36 MB |
|      StringBuilderToString |  1.890 ms | 0.0056 ms | 0.0052 ms |  1.891 ms |   0.16 |     0.00 |  197.2656 | 197.2656 | 197.2656 |   4.56 MB |
|     XmlSerializerSerialize | 24.482 ms | 0.1477 ms | 0.1153 ms | 24.457 ms |   2.05 |     0.03 | 1437.5000 | 750.0000 | 218.7500 |  11.42 MB |
| JsonConvertSerializeObject | 17.398 ms | 0.5457 ms | 1.5303 ms | 16.794 ms |   1.46 |     0.13 |  562.5000 | 281.2500 |        - |   4.55 MB |
