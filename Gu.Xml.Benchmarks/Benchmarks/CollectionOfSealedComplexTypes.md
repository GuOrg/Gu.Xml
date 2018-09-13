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
|             GuXmlSerialize | 44.462 ms | 0.4860 ms | 0.4059 ms | 44.426 ms |   1.00 |     0.00 | 1166.6667 |   83.3333 |  83.3333 |  17.33 MB |
|      StringBuilderToString |  7.448 ms | 0.1964 ms | 0.5636 ms |  7.339 ms |   0.17 |     0.01 |  187.5000 |  187.5000 | 187.5000 |  10.47 MB |
|     XmlSerializerSerialize | 70.617 ms | 1.4077 ms | 3.8536 ms | 69.481 ms |   1.59 |     0.09 | 2625.0000 | 1250.0000 | 375.0000 |   24.8 MB |
| JsonConvertSerializeObject | 39.929 ms | 0.8339 ms | 0.9269 ms | 39.661 ms |   0.90 |     0.02 |  846.1538 |  384.6154 |        - |   8.76 MB |
