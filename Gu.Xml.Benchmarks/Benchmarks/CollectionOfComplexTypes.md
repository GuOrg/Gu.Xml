``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Xeon CPU E5-2637 v4 3.50GHz (Max: 3.49GHz), 1 CPU, 8 logical and 4 physical cores
Frequency=3410074 Hz, Resolution=293.2488 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|                     Method |      Mean |     Error |    StdDev | Scaled | ScaledSD |     Gen 0 |     Gen 1 |    Gen 2 | Allocated |
|--------------------------- |----------:|----------:|----------:|-------:|---------:|----------:|----------:|---------:|----------:|
|             GuXmlSerialize | 45.767 ms | 0.7259 ms | 0.6435 ms |   1.00 |     0.00 | 1090.9091 |         - |        - |  17.33 MB |
|      StringBuilderToString |  9.716 ms | 0.3641 ms | 1.0735 ms |   0.21 |     0.02 |  187.5000 |  187.5000 | 187.5000 |  10.47 MB |
|     XmlSerializerSerialize | 67.998 ms | 1.3325 ms | 2.3685 ms |   1.49 |     0.05 | 2714.2857 | 1285.7143 | 428.5714 |   24.8 MB |
| JsonConvertSerializeObject | 41.917 ms | 0.8353 ms | 0.6521 ms |   0.92 |     0.02 |         - |         - |        - |   8.76 MB |
