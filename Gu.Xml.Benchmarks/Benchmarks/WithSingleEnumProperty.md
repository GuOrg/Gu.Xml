``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Xeon CPU E5-2637 v4 3.50GHz (Max: 3.49GHz), 1 CPU, 8 logical and 4 physical cores
Frequency=3410074 Hz, Resolution=293.2488 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|                     Method |        Mean |      Error |      StdDev | Scaled | ScaledSD |  Gen 0 |  Gen 1 | Allocated |
|--------------------------- |------------:|-----------:|------------:|-------:|---------:|-------:|-------:|----------:|
|             GuXmlSerialize |   495.76 ns |  9.7103 ns |  10.7930 ns |   1.00 |     0.00 | 0.0620 |      - |     392 B |
|      StringBuilderToString |    33.13 ns |  0.4333 ns |   0.3383 ns |   0.07 |     0.00 | 0.0432 |      - |     272 B |
|     XmlSerializerSerialize | 2,601.87 ns | 51.4543 ns | 112.9434 ns |   5.25 |     0.25 | 0.6294 | 0.0038 |    3984 B |
| JsonConvertSerializeObject |   538.51 ns | 10.5955 ns |  15.8589 ns |   1.09 |     0.04 | 0.2041 |      - |    1288 B |
