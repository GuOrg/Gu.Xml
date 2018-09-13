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
|             GuXmlSerialize |   906.93 ns | 19.5009 ns | 46.3460 ns |   1.00 |     0.00 | 0.0505 |      - |     320 B |
|      StringBuilderToString |    26.58 ns |  0.5194 ns |  0.5981 ns |   0.03 |     0.00 | 0.0305 |      - |     192 B |
|     XmlSerializerSerialize | 2,774.43 ns | 27.1115 ns | 22.6393 ns |   3.07 |     0.15 | 0.6218 | 0.0038 |    3936 B |
| JsonConvertSerializeObject |   501.13 ns | 20.3533 ns | 19.0385 ns |   0.55 |     0.03 | 0.2031 |      - |    1280 B |
