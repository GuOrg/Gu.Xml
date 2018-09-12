``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Xeon CPU E5-2637 v4 3.50GHz (Max: 3.49GHz), 1 CPU, 8 logical and 4 physical cores
Frequency=3410074 Hz, Resolution=293.2488 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|                     Method |      Mean |      Error |    StdDev |    Median | Scaled | ScaledSD |   Gen 0 |   Gen 1 |   Gen 2 | Allocated |
|--------------------------- |----------:|-----------:|----------:|----------:|-------:|---------:|--------:|--------:|--------:|----------:|
|             GuXmlSerialize | 534.77 us |  8.7686 us | 7.7731 us | 532.58 us |   1.00 |     0.00 | 33.2031 | 33.2031 | 33.2031 | 189.46 KB |
|      StringBuilderToString |  16.49 us |  0.4380 us | 0.7437 us |  16.13 us |   0.03 |     0.00 | 33.3252 | 33.3252 | 33.3252 | 103.48 KB |
|     XmlSerializerSerialize | 564.60 us | 10.5604 us | 9.8782 us | 565.88 us |   1.06 |     0.02 | 33.2031 | 33.2031 | 33.2031 | 250.81 KB |
| JsonConvertSerializeObject | 446.38 us |  7.1547 us | 6.6926 us | 446.27 us |   0.83 |     0.02 | 13.6719 |  1.9531 |       - |  85.55 KB |
