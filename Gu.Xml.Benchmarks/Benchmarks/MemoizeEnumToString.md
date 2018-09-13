``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.285 (1803/April2018Update/Redstone4)
Intel Core i7-7500U CPU 2.70GHz (Max: 0.80GHz) (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
Frequency=2835934 Hz, Resolution=352.6175 ns, Timer=TSC
.NET Core SDK=2.1.402
  [Host]     : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.4 (CoreCLR 4.6.26814.03, CoreFX 4.6.26814.02), 64bit RyuJIT


```
|        Method |      Mean |     Error |     StdDev |    Median | Scaled |  Gen 0 | Allocated |
|-------------- |----------:|----------:|-----------:|----------:|-------:|-------:|----------:|
|    EnumFormat | 688.21 ns | 44.839 ns | 132.210 ns | 746.08 ns |   1.00 | 0.1030 |     216 B |
| CacheGetOrAdd |  17.67 ns |  1.173 ns |   3.250 ns |  16.97 ns |   0.03 |      - |       0 B |
