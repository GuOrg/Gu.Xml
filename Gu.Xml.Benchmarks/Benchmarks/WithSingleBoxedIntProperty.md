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
|             GuXmlSerialize |   752.05 ns |  3.7287 ns |  3.4878 ns |   752.44 ns |   1.00 |     0.00 | 0.0401 |      - |     256 B |
|      StringBuilderToString |    27.09 ns |  0.3143 ns |  0.2454 ns |    26.94 ns |   0.04 |     0.00 | 0.0305 |      - |     192 B |
|     XmlSerializerSerialize | 2,741.20 ns | 11.8220 ns | 11.0583 ns | 2,745.46 ns |   3.65 |     0.02 | 0.6371 | 0.0038 |    4016 B |
| JsonConvertSerializeObject |   544.25 ns | 10.8163 ns | 23.5138 ns |   535.36 ns |   0.72 |     0.03 | 0.1993 |      - |    1256 B |
