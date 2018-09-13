``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.285 (1803/April2018Update/Redstone4)
Intel Core i7-7500U CPU 2.70GHz (Max: 0.80GHz) (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
Frequency=2835934 Hz, Resolution=352.6175 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT


```
|                     Method |      Mean |     Error |    StdDev | Scaled | ScaledSD |     Gen 0 |     Gen 1 |    Gen 2 | Allocated |
|--------------------------- |----------:|----------:|----------:|-------:|---------:|----------:|----------:|---------:|----------:|
|             GuXmlSerialize | 38.431 ms | 0.7531 ms | 1.1272 ms |   1.00 |     0.00 | 1928.5714 |   71.4286 |  71.4286 |   8.36 MB |
|      StringBuilderToString |  1.724 ms | 0.0998 ms | 0.2942 ms |   0.04 |     0.01 |  236.3281 |  236.3281 | 236.3281 |   4.56 MB |
|     XmlSerializerSerialize | 64.389 ms | 1.3916 ms | 3.9927 ms |   1.68 |     0.11 | 3000.0000 | 1000.0000 |        - |  25.55 MB |
| JsonConvertSerializeObject | 14.536 ms | 0.2896 ms | 0.6477 ms |   0.38 |     0.02 |         - |         - |        - |   2.26 MB |
