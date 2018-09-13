``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.285 (1803/April2018Update/Redstone4)
Intel Core i7-7500U CPU 2.70GHz (Max: 0.80GHz) (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
Frequency=2835934 Hz, Resolution=352.6175 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT


```
|                     Method |        Mean |      Error |     StdDev | Scaled | ScaledSD |  Gen 0 | Allocated |
|--------------------------- |------------:|-----------:|-----------:|-------:|---------:|-------:|----------:|
|             GuXmlSerialize |   987.40 ns | 11.0660 ns | 10.3512 ns |   1.00 |     0.00 | 0.1507 |     320 B |
|      StringBuilderToString |    27.35 ns |  0.4438 ns |  0.4151 ns |   0.03 |     0.00 | 0.0915 |     192 B |
|     XmlSerializerSerialize | 3,144.16 ns | 20.2655 ns | 17.9649 ns |   3.18 |     0.04 | 1.9150 |    4024 B |
| JsonConvertSerializeObject |   554.48 ns |  9.6870 ns |  9.0612 ns |   0.56 |     0.01 | 0.5980 |    1256 B |
