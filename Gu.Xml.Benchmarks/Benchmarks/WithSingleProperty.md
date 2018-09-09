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
|             GuXmlSerialize |   435.97 ns |  8.6378 ns |  9.9473 ns |   1.00 |     0.00 | 0.1216 |     256 B |
|                  XmlString |    29.60 ns |  0.5751 ns |  0.8430 ns |   0.07 |     0.00 | 0.0915 |     192 B |
|     XmlSerializerSerialize | 2,882.35 ns | 56.1762 ns | 52.5472 ns |   6.61 |     0.19 | 1.8768 |    3944 B |
| JsonConvertSerializeObject |   567.11 ns |  9.6379 ns | 12.1889 ns |   1.30 |     0.04 | 0.6056 |    1272 B |
