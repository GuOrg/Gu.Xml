``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Core i7-3960X CPU 3.30GHz (Max: 2.10GHz) (Ivy Bridge), 1 CPU, 12 logical and 6 physical cores
Frequency=3224520 Hz, Resolution=310.1237 ns, Timer=TSC
.NET Core SDK=2.1.401
  [Host]     : .NET Core 2.1.3 (CoreCLR 4.6.26725.06, CoreFX 4.6.26725.05), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.3 (CoreCLR 4.6.26725.06, CoreFX 4.6.26725.05), 64bit RyuJIT


```
|                     Method |        Mean |      Error |     StdDev | Scaled | ScaledSD |  Gen 0 |  Gen 1 | Allocated |
|--------------------------- |------------:|-----------:|-----------:|-------:|---------:|-------:|-------:|----------:|
|             GuXmlSerialize |   936.29 ns | 17.0569 ns | 15.9551 ns |   1.00 |     0.00 | 0.0553 |      - |     352 B |
|      StringBuilderToString |    37.21 ns |  0.3609 ns |  0.3199 ns |   0.04 |     0.00 | 0.0394 |      - |     248 B |
|     XmlSerializerSerialize | 3,643.73 ns | 20.0753 ns | 18.7784 ns |   3.89 |     0.07 | 0.6294 | 0.0038 |    3968 B |
| JsonConvertSerializeObject |   603.16 ns |  7.4850 ns |  7.0015 ns |   0.64 |     0.01 | 0.2012 |      - |    1272 B |
