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
|             GuXmlSerialize |   522.56 ns |  9.9581 ns |  8.8276 ns |   518.81 ns |   1.00 |     0.00 | 0.0401 |      - |     256 B |
|      StringBuilderToString |    26.70 ns |  0.5294 ns |  0.7421 ns |    26.41 ns |   0.05 |     0.00 | 0.0305 |      - |     192 B |
|     XmlSerializerSerialize | 2,453.97 ns | 20.9575 ns | 18.5783 ns | 2,450.82 ns |   4.70 |     0.08 | 0.6218 | 0.0038 |    3936 B |
| JsonConvertSerializeObject |   512.61 ns | 10.1504 ns | 20.9623 ns |   504.21 ns |   0.98 |     0.04 | 0.2031 |      - |    1280 B |
