``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.285 (1803/April2018Update/Redstone4)
Intel Core i7-7500U CPU 2.70GHz (Max: 0.80GHz) (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
Frequency=2835934 Hz, Resolution=352.6175 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT


```
|        Method |      Mean |      Error |     StdDev |    Median | Scaled |  Gen 0 | Allocated |
|-------------- |----------:|-----------:|-----------:|----------:|-------:|-------:|----------:|
|    EnumFormat | 454.13 ns | 14.2736 ns | 40.9535 ns | 439.93 ns |   1.00 | 0.1030 |     216 B |
| CacheGetOrAdd |  15.85 ns |  0.1047 ns |  0.0928 ns |  15.84 ns |   0.04 |      - |       0 B |
