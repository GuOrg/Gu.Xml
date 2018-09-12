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
|             GuXmlSerialize |   519.39 ns | 10.3524 ns |  15.4950 ns |   517.60 ns |   1.00 |     0.00 | 0.0439 |      - |     280 B |
|      StringBuilderToString |    27.74 ns |  0.5295 ns |   0.6502 ns |    27.67 ns |   0.05 |     0.00 | 0.0305 |      - |     192 B |
|     XmlSerializerSerialize | 2,685.21 ns | 53.4447 ns | 121.7207 ns | 2,731.62 ns |   5.17 |     0.28 | 0.6218 | 0.0038 |    3936 B |
| JsonConvertSerializeObject |   485.11 ns |  2.7921 ns |   2.4751 ns |   484.13 ns |   0.93 |     0.03 | 0.2031 |      - |    1280 B |
