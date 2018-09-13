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
|             GuXmlSerialize |   675.02 ns |  6.4997 ns |  6.0798 ns |   1.00 |     0.00 | 0.0401 |      - |     256 B |
|      StringBuilderToString |    25.28 ns |  0.0270 ns |  0.0252 ns |   0.04 |     0.00 | 0.0305 |      - |     192 B |
|     XmlSerializerSerialize | 2,662.23 ns |  4.2553 ns |  3.9804 ns |   3.94 |     0.03 | 0.6371 | 0.0038 |    4016 B |
| JsonConvertSerializeObject |   505.62 ns | 11.6393 ns | 10.8874 ns |   0.75 |     0.02 | 0.1993 |      - |    1256 B |
