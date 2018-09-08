``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.254 (1803/April2018Update/Redstone4)
Intel Core i7-7500U CPU 2.70GHz (Max: 0.80GHz) (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
Frequency=2835943 Hz, Resolution=352.6164 ns, Timer=TSC
.NET Core SDK=2.1.401
  [Host]     : .NET Core 2.1.3 (CoreCLR 4.6.26725.06, CoreFX 4.6.26725.05), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.3 (CoreCLR 4.6.26725.06, CoreFX 4.6.26725.05), 64bit RyuJIT


```
|                     Method |       Mean |     Error |    StdDev | Scaled | ScaledSD |  Gen 0 | Allocated |
|--------------------------- |-----------:|----------:|----------:|-------:|---------:|-------:|----------:|
|             GuXmlSerialize |   548.1 ns |  9.789 ns |  9.157 ns |   1.00 |     0.00 | 0.1326 |     280 B |
|     XmlSerializerSerialize | 2,992.6 ns | 56.286 ns | 55.280 ns |   5.46 |     0.13 | 1.8768 |    3944 B |
| JsonConvertSerializeObject |   592.1 ns | 14.873 ns | 18.810 ns |   1.08 |     0.04 | 0.6056 |    1272 B |
