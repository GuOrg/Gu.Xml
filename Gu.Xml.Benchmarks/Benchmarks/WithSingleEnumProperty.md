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
|             GuXmlSerialize |   487.04 ns |  0.3992 ns |  0.3116 ns |   487.03 ns |   1.00 |     0.00 | 0.0582 |      - |     368 B |
|      StringBuilderToString |    30.77 ns |  0.0295 ns |  0.0262 ns |    30.77 ns |   0.06 |     0.00 | 0.0432 |      - |     272 B |
|     XmlSerializerSerialize | 2,438.75 ns |  3.2629 ns |  2.5474 ns | 2,438.24 ns |   5.01 |     0.01 | 0.6294 | 0.0038 |    3984 B |
| JsonConvertSerializeObject |   550.06 ns | 10.9444 ns | 20.8229 ns |   561.19 ns |   1.13 |     0.04 | 0.2041 |      - |    1288 B |
