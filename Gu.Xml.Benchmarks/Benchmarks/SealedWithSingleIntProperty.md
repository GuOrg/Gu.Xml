``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Xeon CPU E5-2637 v4 3.50GHz (Max: 3.49GHz), 1 CPU, 8 logical and 4 physical cores
Frequency=3410074 Hz, Resolution=293.2488 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|                     Method |        Mean |     Error |    StdDev | Scaled | ScaledSD |  Gen 0 |  Gen 1 | Allocated |
|--------------------------- |------------:|----------:|----------:|-------:|---------:|-------:|-------:|----------:|
|             GuXmlSerialize |   512.58 ns | 2.1715 ns | 1.9250 ns |   1.00 |     0.00 | 0.0401 |      - |     256 B |
|      StringBuilderToString |    25.25 ns | 0.0276 ns | 0.0258 ns |   0.05 |     0.00 | 0.0305 |      - |     192 B |
|     XmlSerializerSerialize | 2,427.22 ns | 9.7349 ns | 8.1290 ns |   4.74 |     0.02 | 0.6218 | 0.0038 |    3936 B |
| JsonConvertSerializeObject |   475.81 ns | 0.1906 ns | 0.1592 ns |   0.93 |     0.00 | 0.2031 |      - |    1280 B |
