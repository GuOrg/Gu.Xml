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
|             GuXmlSerialize |   771.74 ns | 15.2429 ns | 27.4860 ns |   1.00 |     0.00 | 0.1822 |     384 B |
|      StringBuilderToString |    36.69 ns |  0.8801 ns |  0.9038 ns |   0.05 |     0.00 | 0.1182 |     248 B |
|     XmlSerializerSerialize | 2,782.10 ns | 31.4498 ns | 27.8794 ns |   3.61 |     0.14 | 1.8883 |    3968 B |
| JsonConvertSerializeObject |   553.92 ns |  5.3982 ns |  4.2146 ns |   0.72 |     0.03 | 0.6056 |    1272 B |
