``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Xeon CPU E5-2637 v4 3.50GHz (Max: 3.49GHz), 1 CPU, 8 logical and 4 physical cores
Frequency=3410074 Hz, Resolution=293.2488 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|                     Method |        Mean |      Error |     StdDev | Scaled | ScaledSD |  Gen 0 |  Gen 1 | Allocated |
|--------------------------- |------------:|-----------:|-----------:|-------:|---------:|-------:|-------:|----------:|
|             GuXmlSerialize |   419.12 ns |  2.3280 ns |   1.818 ns |   1.00 |     0.00 | 0.0405 |      - |     256 B |
|      StringBuilderToString |    29.11 ns |  0.5768 ns |   1.382 ns |   0.07 |     0.00 | 0.0305 |      - |     192 B |
|     XmlSerializerSerialize | 2,749.88 ns | 54.6374 ns | 117.613 ns |   6.56 |     0.28 | 0.6218 | 0.0038 |    3936 B |
| JsonConvertSerializeObject |   488.47 ns |  4.2307 ns |   3.303 ns |   1.17 |     0.01 | 0.2031 |      - |    1280 B |
