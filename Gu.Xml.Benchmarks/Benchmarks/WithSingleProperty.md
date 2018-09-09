``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.254 (1803/April2018Update/Redstone4)
Intel Core i7-7500U CPU 2.70GHz (Max: 0.80GHz) (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
Frequency=2835943 Hz, Resolution=352.6164 ns, Timer=TSC
.NET Core SDK=2.1.401
  [Host]     : .NET Core 2.1.3 (CoreCLR 4.6.26725.06, CoreFX 4.6.26725.05), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.3 (CoreCLR 4.6.26725.06, CoreFX 4.6.26725.05), 64bit RyuJIT


```
|                     Method |       Mean |    Error |   StdDev | Scaled | ScaledSD |  Gen 0 | Allocated |
|--------------------------- |-----------:|---------:|---------:|-------:|---------:|-------:|----------:|
|             GuXmlSerialize |   568.1 ns | 10.87 ns | 10.17 ns |   1.00 |     0.00 | 0.1173 |     248 B |
|     XmlSerializerSerialize | 2,810.4 ns | 55.83 ns | 99.24 ns |   4.95 |     0.19 | 1.8768 |    3944 B |
| JsonConvertSerializeObject |   541.6 ns | 10.68 ns | 10.97 ns |   0.95 |     0.02 | 0.6056 |    1272 B |
