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
|             GuXmlSerialize |   547.94 ns | 10.9513 ns | 14.2398 ns |   1.00 |     0.00 | 0.0439 |      - |     280 B |
|      StringBuilderToString |    27.88 ns |  0.5215 ns |  0.4879 ns |   0.05 |     0.00 | 0.0305 |      - |     192 B |
|     XmlSerializerSerialize | 3,160.10 ns | 62.3722 ns | 69.3266 ns |   5.77 |     0.19 | 0.6218 | 0.0038 |    3936 B |
| JsonConvertSerializeObject |   520.57 ns | 10.2363 ns | 12.9456 ns |   0.95 |     0.03 | 0.2031 |      - |    1280 B |
