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
|             GuXmlSerialize |   597.53 ns |  3.0630 ns |  2.5578 ns |   596.72 ns |   1.00 |     0.00 | 0.0401 |      - |     256 B |
|      StringBuilderToString |    27.76 ns |  0.5348 ns |  0.7670 ns |    27.50 ns |   0.05 |     0.00 | 0.0305 |      - |     192 B |
|     XmlSerializerSerialize | 2,768.44 ns | 54.7115 ns | 97.2496 ns | 2,715.03 ns |   4.63 |     0.16 | 0.6371 | 0.0038 |    4016 B |
| JsonConvertSerializeObject |   512.27 ns |  0.2966 ns |  0.2476 ns |   512.29 ns |   0.86 |     0.00 | 0.1993 |      - |    1256 B |
