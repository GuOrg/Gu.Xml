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
|             GuXmlSerialize | 83.221 ms | 1.9497 ms |  5.4671 ms | 81.733 ms |   1.00 |     0.00 | 6000.0000 |         - |        - |  23.43 MB |
|      StringBuilderToString |  4.128 ms | 0.2709 ms |  0.7988 ms |  4.488 ms |   0.05 |     0.01 |  234.3750 |  234.3750 | 234.3750 |  10.47 MB |
|     XmlSerializerSerialize | 89.314 ms | 6.8377 ms | 20.1611 ms | 81.022 ms |   1.08 |     0.25 | 3000.0000 | 1000.0000 |        - |   24.8 MB |
| JsonConvertSerializeObject | 61.404 ms | 4.3434 ms | 12.4622 ms | 61.361 ms |   0.74 |     0.16 | 1000.0000 |         - |        - |   8.76 MB |
