``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Xeon CPU E5-2637 v4 3.50GHz (Max: 3.49GHz), 1 CPU, 8 logical and 4 physical cores
Frequency=3410068 Hz, Resolution=293.2493 ns, Timer=TSC
.NET Core SDK=2.1.401
  [Host]     : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.0.9 (CoreCLR 4.6.26614.01, CoreFX 4.6.26614.01), 64bit RyuJIT


```
|        Method |      Mean |      Error |     StdDev | Scaled |  Gen 0 | Allocated |
|-------------- |----------:|-----------:|-----------:|-------:|-------:|----------:|
|    EnumFormat | 484.17 ns | 11.3410 ns | 12.6055 ns |   1.00 | 0.0334 |     216 B |
| CacheGetOrAdd |  17.01 ns |  0.0755 ns |  0.0631 ns |   0.04 |      - |       0 B |
