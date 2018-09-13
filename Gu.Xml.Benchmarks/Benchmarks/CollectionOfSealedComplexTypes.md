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
|             GuXmlSerialize | 32.017 ms | 0.6403 ms | 1.3079 ms | 31.437 ms |   1.00 |     0.00 | 3533.3333 |  133.3333 | 133.3333 |  17.33 MB |
|      StringBuilderToString |  3.815 ms | 0.2273 ms | 0.6703 ms |  3.890 ms |   0.12 |     0.02 |  187.5000 |  187.5000 | 187.5000 |  10.47 MB |
|     XmlSerializerSerialize | 62.235 ms | 1.2601 ms | 2.0704 ms | 61.470 ms |   1.95 |     0.10 | 2555.5556 | 1000.0000 | 222.2222 |   24.8 MB |
| JsonConvertSerializeObject | 37.299 ms | 0.5427 ms | 0.4811 ms | 37.189 ms |   1.17 |     0.05 |  923.0769 |  384.6154 |        - |   8.76 MB |
