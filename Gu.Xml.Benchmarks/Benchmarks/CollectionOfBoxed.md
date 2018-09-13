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
|             GuXmlSerialize | 41.726 ms | 0.8273 ms | 1.4706 ms | 41.309 ms |   1.00 |     0.00 |  692.3077 |   76.9231 |  76.9231 |   8.36 MB |
|      StringBuilderToString |  3.413 ms | 0.0943 ms | 0.2781 ms |  3.385 ms |   0.08 |     0.01 |  195.3125 |  195.3125 | 195.3125 |   4.56 MB |
|     XmlSerializerSerialize | 68.349 ms | 1.3635 ms | 3.3190 ms | 67.956 ms |   1.64 |     0.10 | 3125.0000 | 1375.0000 | 500.0000 |  25.55 MB |
| JsonConvertSerializeObject | 16.561 ms | 0.3300 ms | 0.6437 ms | 16.842 ms |   0.40 |     0.02 |  156.2500 |   62.5000 |        - |   2.26 MB |
