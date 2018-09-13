``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.285 (1803/April2018Update/Redstone4)
Intel Core i7-7500U CPU 2.70GHz (Max: 0.80GHz) (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
Frequency=2835934 Hz, Resolution=352.6175 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT


```
|                     Method |      Mean |     Error |    StdDev |    Median | Scaled | ScaledSD |     Gen 0 |     Gen 1 |    Gen 2 | Allocated |
|--------------------------- |----------:|----------:|----------:|----------:|-------:|---------:|----------:|----------:|---------:|----------:|
|             GuXmlSerialize | 85.448 ms | 2.4693 ms | 6.8010 ms | 83.210 ms |   1.00 |     0.00 | 6000.0000 |         - |        - |  23.43 MB |
|      StringBuilderToString |  4.723 ms | 0.3173 ms | 0.9356 ms |  4.855 ms |   0.06 |     0.01 |  218.7500 |  218.7500 | 218.7500 |  10.47 MB |
|     XmlSerializerSerialize | 59.243 ms | 1.1833 ms | 1.9107 ms | 59.201 ms |   0.70 |     0.06 | 3000.0000 | 1000.0000 |        - |   24.8 MB |
| JsonConvertSerializeObject | 42.296 ms | 0.7114 ms | 0.6654 ms | 42.002 ms |   0.50 |     0.04 | 1000.0000 |         - |        - |   8.76 MB |
