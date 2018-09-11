``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Xeon CPU E5-2637 v4 3.50GHz (Max: 3.49GHz), 1 CPU, 8 logical and 4 physical cores
Frequency=3410068 Hz, Resolution=293.2493 ns, Timer=TSC
.NET Core SDK=2.1.401
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|                     Method |        Mean |      Error |      StdDev | Scaled | ScaledSD |  Gen 0 |  Gen 1 | Allocated |
|--------------------------- |------------:|-----------:|------------:|-------:|---------:|-------:|-------:|----------:|
|             GuXmlSerialize |   845.50 ns | 16.8185 ns |  26.6759 ns |   1.00 |     0.00 | 0.0553 |      - |     352 B |
|      StringBuilderToString |    31.99 ns |  0.6545 ns |   0.8959 ns |   0.04 |     0.00 | 0.0394 |      - |     248 B |
|     XmlSerializerSerialize | 2,700.01 ns | 53.5257 ns | 110.5399 ns |   3.20 |     0.16 | 0.6256 | 0.0038 |    3960 B |
| JsonConvertSerializeObject |   544.19 ns | 10.7883 ns |  24.7878 ns |   0.64 |     0.04 | 0.2012 |      - |    1272 B |
