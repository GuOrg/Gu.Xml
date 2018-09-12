``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Xeon CPU E5-2637 v4 3.50GHz (Max: 3.49GHz), 1 CPU, 8 logical and 4 physical cores
Frequency=3410074 Hz, Resolution=293.2488 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|                     Method |        Mean |      Error |      StdDev |      Median | Scaled | ScaledSD |  Gen 0 |  Gen 1 | Allocated |
|--------------------------- |------------:|-----------:|------------:|------------:|-------:|---------:|-------:|-------:|----------:|
|             GuXmlSerialize |   539.18 ns |  7.5153 ns |   6.2756 ns |   537.57 ns |   1.00 |     0.00 | 0.0439 |      - |     280 B |
|      StringBuilderToString |    25.86 ns |  0.0724 ns |   0.0642 ns |    25.84 ns |   0.05 |     0.00 | 0.0305 |      - |     192 B |
|     XmlSerializerSerialize | 2,484.40 ns | 49.4713 ns | 125.0202 ns | 2,442.22 ns |   4.61 |     0.24 | 0.6218 | 0.0038 |    3936 B |
| JsonConvertSerializeObject |   530.31 ns | 10.5840 ns |  14.8373 ns |   526.07 ns |   0.98 |     0.03 | 0.2031 |      - |    1280 B |
