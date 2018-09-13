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
|             GuXmlSerialize |   503.66 ns | 14.4883 ns |  14.229 ns |   500.91 ns |   1.00 |     0.00 | 0.0582 |      - |     368 B |
|      StringBuilderToString |    33.24 ns |  0.6636 ns |   1.429 ns |    32.16 ns |   0.07 |     0.00 | 0.0432 |      - |     272 B |
|     XmlSerializerSerialize | 2,697.71 ns | 53.5224 ns | 150.961 ns | 2,700.07 ns |   5.36 |     0.33 | 0.6294 | 0.0038 |    3984 B |
| JsonConvertSerializeObject |   533.59 ns | 10.5632 ns |  21.096 ns |   528.38 ns |   1.06 |     0.05 | 0.2041 |      - |    1288 B |
