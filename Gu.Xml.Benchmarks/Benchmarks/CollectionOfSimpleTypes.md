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
|             GuXmlSerialize | 18.467 ms | 1.2555 ms | 3.7018 ms | 18.700 ms |   1.00 |     0.00 | 2171.8750 | 281.2500 | 281.2500 |   8.36 MB |
|      StringBuilderToString |  2.195 ms | 0.0733 ms | 0.2161 ms |  2.158 ms |   0.12 |     0.03 |  234.3750 | 234.3750 | 234.3750 |   4.56 MB |
|     XmlSerializerSerialize | 39.740 ms | 2.6639 ms | 7.8546 ms | 41.144 ms |   2.25 |     0.65 | 1454.5455 | 727.2727 |  90.9091 |  11.42 MB |
| JsonConvertSerializeObject | 20.253 ms | 1.3444 ms | 3.9640 ms | 18.751 ms |   1.14 |     0.33 |  562.5000 | 281.2500 |        - |   4.55 MB |
