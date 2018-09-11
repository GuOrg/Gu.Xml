``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Xeon CPU E5-2637 v4 3.50GHz (Max: 3.49GHz), 1 CPU, 8 logical and 4 physical cores
Frequency=3410068 Hz, Resolution=293.2493 ns, Timer=TSC
.NET Core SDK=2.1.401
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|                     Method |        Mean |      Error |     StdDev | Scaled | ScaledSD |  Gen 0 |  Gen 1 | Allocated |
|--------------------------- |------------:|-----------:|-----------:|-------:|---------:|-------:|-------:|----------:|
|             GuXmlSerialize |   835.43 ns | 16.7015 ns | 27.4410 ns |   1.00 |     0.00 | 0.0591 |      - |     376 B |
|      StringBuilderToString |    30.43 ns |  0.0982 ns |  0.0919 ns |   0.04 |     0.00 | 0.0394 |      - |     248 B |
|     XmlSerializerSerialize | 2,651.97 ns | 21.4774 ns | 17.9346 ns |   3.18 |     0.10 | 0.6256 | 0.0038 |    3960 B |
| JsonConvertSerializeObject |   557.58 ns | 11.0475 ns | 20.4773 ns |   0.67 |     0.03 | 0.2012 |      - |    1272 B |
