``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Xeon CPU E5-2637 v4 3.50GHz (Max: 3.49GHz), 1 CPU, 8 logical and 4 physical cores
Frequency=3410074 Hz, Resolution=293.2488 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|                     Method |      Mean |     Error |    StdDev |    Median | Scaled | ScaledSD |     Gen 0 |     Gen 1 |    Gen 2 | Allocated |
|--------------------------- |----------:|----------:|----------:|----------:|-------:|---------:|----------:|----------:|---------:|----------:|
|             GuXmlSerialize | 37.145 ms | 0.7968 ms | 1.8625 ms | 36.688 ms |   1.00 |     0.00 | 1214.2857 |  142.8571 | 142.8571 |  17.33 MB |
|      StringBuilderToString |  7.374 ms | 0.2276 ms | 0.6675 ms |  7.280 ms |   0.20 |     0.02 |  187.5000 |  187.5000 | 187.5000 |  10.47 MB |
|     XmlSerializerSerialize | 70.097 ms | 1.3971 ms | 3.4270 ms | 69.861 ms |   1.89 |     0.13 | 2625.0000 | 1250.0000 | 375.0000 |   24.8 MB |
| JsonConvertSerializeObject | 40.828 ms | 3.0920 ms | 7.8139 ms | 37.095 ms |   1.10 |     0.22 |         - |         - |        - |   8.76 MB |
