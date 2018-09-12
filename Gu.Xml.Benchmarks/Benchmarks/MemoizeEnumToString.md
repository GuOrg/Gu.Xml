``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Xeon CPU E5-2637 v4 3.50GHz (Max: 3.49GHz), 1 CPU, 8 logical and 4 physical cores
Frequency=3410074 Hz, Resolution=293.2488 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|        Method |      Mean |     Error |     StdDev | Scaled |  Gen 0 | Allocated |
|-------------- |----------:|----------:|-----------:|-------:|-------:|----------:|
|    EnumFormat | 496.83 ns | 9.9250 ns | 14.5479 ns |   1.00 | 0.0334 |     216 B |
| CacheGetOrAdd |  16.82 ns | 0.2306 ns |  0.1926 ns |   0.03 |      - |       0 B |
