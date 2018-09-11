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
|             GuXmlSerialize |   525.76 ns | 10.3797 ns |  20.7293 ns |   1.00 |     0.00 | 0.0401 |      - |     256 B |
|      StringBuilderToString |    26.15 ns |  0.6234 ns |   0.5832 ns |   0.05 |     0.00 | 0.0305 |      - |     192 B |
|     XmlSerializerSerialize | 2,609.14 ns | 52.1080 ns | 152.0016 ns |   4.97 |     0.34 | 0.6218 | 0.0038 |    3936 B |
| JsonConvertSerializeObject |   537.58 ns | 10.5821 ns |  20.1335 ns |   1.02 |     0.05 | 0.2012 |      - |    1272 B |
