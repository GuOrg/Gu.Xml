``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Xeon CPU E5-2637 v4 3.50GHz (Max: 3.49GHz), 1 CPU, 8 logical and 4 physical cores
Frequency=3410071 Hz, Resolution=293.2490 ns, Timer=TSC
.NET Core SDK=2.1.401
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|                 Method |     Mean |    Error |   StdDev | Scaled | ScaledSD |  Gen 0 | Allocated |
|----------------------- |---------:|---------:|---------:|-------:|---------:|-------:|----------:|
|         GuXmlSerialize | 631.6 ns | 1.309 ns | 1.160 ns |   1.00 |     0.00 | 0.1535 |     968 B |
| XmlSerializerSerialize |       NA |       NA |       NA |      ? |        ? |    N/A |       N/A |

Benchmarks with issues:
  WithSingleProperty.XmlSerializerSerialize: DefaultJob
