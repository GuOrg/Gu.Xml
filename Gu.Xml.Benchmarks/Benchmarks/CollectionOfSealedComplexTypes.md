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
|             GuXmlSerialize | 42.713 ms | 2.6376 ms |  7.777 ms |   1.00 |     0.00 | 3656.2500 |  250.0000 | 250.0000 |  17.33 MB |
|      StringBuilderToString |  4.602 ms | 0.3407 ms |  1.005 ms |   0.11 |     0.03 |  218.7500 |  218.7500 | 218.7500 |  10.47 MB |
|     XmlSerializerSerialize | 89.883 ms | 8.8027 ms | 25.955 ms |   2.18 |     0.75 | 3000.0000 | 1000.0000 |        - |   24.8 MB |
| JsonConvertSerializeObject | 44.930 ms | 1.3505 ms |  3.697 ms |   1.09 |     0.22 | 1000.0000 |         - |        - |   8.76 MB |
