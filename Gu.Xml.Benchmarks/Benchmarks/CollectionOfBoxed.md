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
|             GuXmlSerialize | 41.042 ms | 0.8128 ms | 1.3579 ms | 40.454 ms |   1.00 |     0.00 |  692.3077 |   76.9231 |  76.9231 |   8.36 MB |
|      StringBuilderToString |  5.031 ms | 0.2953 ms | 0.8706 ms |  5.407 ms |   0.12 |     0.02 |  187.5000 |  187.5000 | 187.5000 |   4.56 MB |
|     XmlSerializerSerialize | 67.486 ms | 1.3698 ms | 3.3082 ms | 66.462 ms |   1.65 |     0.10 | 3142.8571 | 1428.5714 | 428.5714 |  25.56 MB |
| JsonConvertSerializeObject | 16.094 ms | 0.3175 ms | 0.6628 ms | 15.762 ms |   0.39 |     0.02 |  156.2500 |   62.5000 |        - |   2.26 MB |
