``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Xeon CPU E5-2637 v4 3.50GHz (Max: 3.49GHz), 1 CPU, 8 logical and 4 physical cores
Frequency=3410068 Hz, Resolution=293.2493 ns, Timer=TSC
.NET Core SDK=2.1.401
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|                     Method |        Mean |      Error |    StdDev |      Median | Scaled | ScaledSD |  Gen 0 |  Gen 1 | Allocated |
|--------------------------- |------------:|-----------:|----------:|------------:|-------:|---------:|-------:|-------:|----------:|
|             GuXmlSerialize |   518.41 ns | 10.3587 ns | 20.925 ns |   514.65 ns |   1.00 |     0.00 | 0.0401 |      - |     256 B |
|      StringBuilderToString |    28.11 ns |  0.5583 ns |  1.411 ns |    27.30 ns |   0.05 |     0.00 | 0.0305 |      - |     192 B |
|     XmlSerializerSerialize | 2,497.45 ns | 16.6500 ns | 12.999 ns | 2,502.18 ns |   4.83 |     0.19 | 0.6218 | 0.0038 |    3936 B |
| JsonConvertSerializeObject |   511.86 ns |  7.8299 ns |  6.941 ns |   510.13 ns |   0.99 |     0.04 | 0.2012 |      - |    1272 B |
