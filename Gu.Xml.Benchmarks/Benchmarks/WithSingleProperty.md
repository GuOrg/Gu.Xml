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
|             GuXmlSerialize |   542.77 ns |  6.0818 ns |  5.6889 ns |   1.00 |     0.00 | 0.0401 |      - |     256 B |
|      StringBuilderToString |    31.30 ns |  0.1584 ns |  0.1482 ns |   0.06 |     0.00 | 0.0305 |      - |     192 B |
|     XmlSerializerSerialize | 3,800.72 ns | 71.8957 ns | 70.6112 ns |   7.00 |     0.14 | 0.6256 | 0.0038 |    3944 B |
| JsonConvertSerializeObject |   596.25 ns | 12.6039 ns | 12.9433 ns |   1.10 |     0.03 | 0.2012 |      - |    1272 B |
