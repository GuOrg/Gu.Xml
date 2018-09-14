``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Xeon CPU E5-2637 v4 3.50GHz (Max: 3.49GHz), 1 CPU, 8 logical and 4 physical cores
Frequency=3410074 Hz, Resolution=293.2488 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|                     Method |        Mean |      Error |     StdDev |      Median | Scaled | ScaledSD |  Gen 0 |  Gen 1 | Allocated |
|--------------------------- |------------:|-----------:|-----------:|------------:|-------:|---------:|-------:|-------:|----------:|
|             GuXmlSerialize | 3,349.13 ns |  65.153 ns |  91.335 ns | 3,374.56 ns |   1.00 |     0.00 | 0.1602 |      - |    1016 B |
|      StringBuilderToString |    70.23 ns |   1.401 ns |   2.632 ns |    68.33 ns |   0.02 |     0.00 | 0.1105 | 0.0002 |     696 B |
|     XmlSerializerSerialize | 6,467.70 ns | 145.683 ns | 136.272 ns | 6,392.63 ns |   1.93 |     0.07 | 0.8774 | 0.0076 |    5544 B |
| JsonConvertSerializeObject | 4,324.79 ns |  86.098 ns | 177.806 ns | 4,390.48 ns |   1.29 |     0.06 | 0.3586 |      - |    2272 B |
