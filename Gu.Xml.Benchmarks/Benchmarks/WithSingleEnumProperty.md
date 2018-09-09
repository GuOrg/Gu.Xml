``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.254 (1803/April2018Update/Redstone4)
Intel Core i7-7500U CPU 2.70GHz (Max: 0.80GHz) (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
Frequency=2835943 Hz, Resolution=352.6164 ns, Timer=TSC
.NET Core SDK=2.1.401
  [Host]     : .NET Core 2.1.3 (CoreCLR 4.6.26725.06, CoreFX 4.6.26725.05), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.3 (CoreCLR 4.6.26725.06, CoreFX 4.6.26725.05), 64bit RyuJIT


```
|                     Method |        Mean |      Error |     StdDev | Scaled | ScaledSD |  Gen 0 | Allocated |
|--------------------------- |------------:|-----------:|-----------:|-------:|---------:|-------:|----------:|
|             GuXmlSerialize |   807.89 ns | 13.1670 ns | 12.3164 ns |   1.00 |     0.00 | 0.1678 |     352 B |
|                  XmlString |    36.69 ns |  0.8801 ns |  0.9038 ns |   0.05 |     0.00 | 0.1182 |     248 B |
|     XmlSerializerSerialize | 2,857.28 ns | 42.9487 ns | 40.1742 ns |   3.54 |     0.07 | 1.8883 |    3968 B |
| JsonConvertSerializeObject |   573.89 ns | 10.9347 ns | 13.0170 ns |   0.71 |     0.02 | 0.6056 |    1272 B |
