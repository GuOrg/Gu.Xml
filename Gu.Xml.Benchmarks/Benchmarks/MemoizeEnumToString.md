``` ini

BenchmarkDotNet=v0.11.1, OS=Windows 10.0.17134.228 (1803/April2018Update/Redstone4)
Intel Core i7-3960X CPU 3.30GHz (Max: 2.10GHz) (Ivy Bridge), 1 CPU, 12 logical and 6 physical cores
Frequency=3224520 Hz, Resolution=310.1237 ns, Timer=TSC
.NET Core SDK=2.1.401
  [Host]     : .NET Core 2.1.3 (CoreCLR 4.6.26725.06, CoreFX 4.6.26725.05), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.3 (CoreCLR 4.6.26725.06, CoreFX 4.6.26725.05), 64bit RyuJIT


```
|        Method |      Mean |     Error |    StdDev | Scaled |  Gen 0 | Allocated |
|-------------- |----------:|----------:|----------:|-------:|-------:|----------:|
|    EnumFormat | 471.98 ns | 1.9812 ns | 1.7563 ns |   1.00 | 0.0334 |     216 B |
| CacheGetOrAdd |  18.17 ns | 0.2599 ns | 0.2304 ns |   0.04 |      - |       0 B |
