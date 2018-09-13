``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.285 (1803/April2018Update/Redstone4)
Intel Core i7-7500U CPU 2.70GHz (Max: 0.80GHz) (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
Frequency=2835934 Hz, Resolution=352.6175 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT


```
|                     Method |      Mean |     Error |     StdDev |    Median | Scaled | ScaledSD |     Gen 0 |     Gen 1 |    Gen 2 | Allocated |
|--------------------------- |----------:|----------:|-----------:|----------:|-------:|---------:|----------:|----------:|---------:|----------:|
|             GuXmlSerialize | 37.696 ms | 0.6485 ms |  0.5063 ms | 37.764 ms |   1.00 |     0.00 | 1923.0769 |   76.9231 |  76.9231 |   8.36 MB |
|      StringBuilderToString |  2.025 ms | 0.1072 ms |  0.3160 ms |  2.108 ms |   0.05 |     0.01 |  238.2813 |  238.2813 | 238.2813 |   4.56 MB |
|     XmlSerializerSerialize | 88.497 ms | 6.0390 ms | 17.8060 ms | 88.994 ms |   2.35 |     0.47 | 3125.0000 | 1250.0000 | 375.0000 |  25.55 MB |
| JsonConvertSerializeObject | 19.192 ms | 2.0336 ms |  5.9961 ms | 16.688 ms |   0.51 |     0.16 |  437.5000 |  218.7500 |        - |   2.26 MB |
