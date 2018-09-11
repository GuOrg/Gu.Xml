``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Xeon CPU E5-2637 v4 3.50GHz (Max: 3.49GHz), 1 CPU, 8 logical and 4 physical cores
Frequency=3410068 Hz, Resolution=293.2493 ns, Timer=TSC
.NET Core SDK=2.1.401
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|                     Method |        Mean |      Error |     StdDev |      Median | Scaled | ScaledSD |  Gen 0 |  Gen 1 | Allocated |
|--------------------------- |------------:|-----------:|-----------:|------------:|-------:|---------:|-------:|-------:|----------:|
|             GuXmlSerialize |   513.78 ns |  0.5854 ns |  0.4889 ns |   513.70 ns |   1.00 |     0.00 | 0.0401 |      - |     256 B |
|      StringBuilderToString |    26.10 ns |  0.5076 ns |  0.7752 ns |    25.62 ns |   0.05 |     0.00 | 0.0305 |      - |     192 B |
|     XmlSerializerSerialize | 2,424.00 ns | 23.9071 ns | 21.1931 ns | 2,421.31 ns |   4.72 |     0.04 | 0.6218 | 0.0038 |    3936 B |
| JsonConvertSerializeObject |   539.20 ns | 10.7319 ns | 19.6239 ns |   540.97 ns |   1.05 |     0.04 | 0.2012 |      - |    1272 B |
