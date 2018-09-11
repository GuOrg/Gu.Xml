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
|             GuXmlSerialize |   836.51 ns | 16.4694 ns | 18.306 ns |   836.29 ns |   1.00 |     0.00 | 0.0553 |      - |     352 B |
|      StringBuilderToString |    32.49 ns |  0.6359 ns |  1.225 ns |    32.58 ns |   0.04 |     0.00 | 0.0394 |      - |     248 B |
|     XmlSerializerSerialize | 2,621.87 ns | 50.8606 ns | 66.133 ns | 2,636.59 ns |   3.14 |     0.10 | 0.6256 | 0.0038 |    3960 B |
| JsonConvertSerializeObject |   543.51 ns | 10.7757 ns | 20.239 ns |   532.25 ns |   0.65 |     0.03 | 0.2012 |      - |    1272 B |
