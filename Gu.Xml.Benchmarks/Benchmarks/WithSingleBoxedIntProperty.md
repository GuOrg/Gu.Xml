``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Xeon CPU E5-2637 v4 3.50GHz (Max: 3.49GHz), 1 CPU, 8 logical and 4 physical cores
Frequency=3410074 Hz, Resolution=293.2488 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|                     Method |        Mean |      Error |     StdDev | Scaled | ScaledSD |  Gen 0 |  Gen 1 | Allocated |
|--------------------------- |------------:|-----------:|-----------:|-------:|---------:|-------:|-------:|----------:|
|             GuXmlSerialize | 1,131.07 ns | 22.3451 ns |  52.231 ns |   1.00 |     0.00 | 0.0496 |      - |     320 B |
|      StringBuilderToString |    29.20 ns |  0.5787 ns |   1.483 ns |   0.03 |     0.00 | 0.0305 |      - |     192 B |
|     XmlSerializerSerialize | 2,965.39 ns | 72.4879 ns | 213.732 ns |   2.63 |     0.22 | 0.6371 | 0.0038 |    4016 B |
| JsonConvertSerializeObject |   542.02 ns | 10.7064 ns |  21.870 ns |   0.48 |     0.03 | 0.1993 |      - |    1256 B |
