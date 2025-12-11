


using System.Diagnostics;


namespace michle.natale.BigPrimesGenerator;

using michele.natale.Services;

public class BigPrimesBenchmarks
{
  public static async Task RunAsync()
  {
    var bits = Enum.GetValues<BitPrimeLength>();
    foreach (var bit in bits)
      if ((int)bit <= 1024)
        await RunAsync(bit, 10);
      else if ((int)bit < 4096)
        await RunAsync(bit, 2);
      else if ((int)bit <= 4096)
        await RunAsync(bit, 1);
      else break;

    Console.WriteLine();
  }

  public static async Task RunAsync(
    BitPrimeLength bits, int count)
  {
    var options = new BigPrimesOption
    {
      BitLength = bits,
      MaxParallelism = Environment.ProcessorCount,
      MillerRabinRounds = PrimalityConfidence.Normal
    };

    int parallelism = ServiceBigPrimesGenerator.GetAdaptiveParallelism(options.BitLength);

    Console.WriteLine($"Benchmark: {count} primes, {options.BitLength} bits");
    Console.Write($"Parallelism: {parallelism}, ");
    Console.WriteLine($"Confidence: {options.MillerRabinRounds}");

    // Serial
    var swserial = Stopwatch.StartNew();
    var serialprimes = await BigPrimeGenerator.ToPrimesSerialAsync(options, count, CancellationToken.None);
    swserial.Stop();
    Console.WriteLine($"Serial: {serialprimes.Count} primes in {swserial.Elapsed.TotalSeconds:F2} s");

    // Parallel
    var swparallel = Stopwatch.StartNew();
    var parallelprimes = await BigPrimeGenerator.ToPrimesParallelAsync(options, count, CancellationToken.None);
    swparallel.Stop();
    Console.WriteLine($"Parallel: {parallelprimes.Count} primes in {swparallel.Elapsed.TotalSeconds:F2} s");

    // Optional: Durchschnitt pro Prime
    Console.WriteLine($"Avg Serial: {swserial.Elapsed.TotalSeconds / count:F2} s/prime");
    Console.WriteLine($"Avg Parallel: {swparallel.Elapsed.TotalSeconds / count:F2} s/prime");
    Console.WriteLine();
  }


  public static async Task RunWithStatsAsync()
  {
    var bits = Enum.GetValues<BitPrimeLength>();
    foreach (var bit in bits)
      if ((int)bit <= 1024)
        await RunWithStatsAsync(bit, 10);
      else if ((int)bit < 4096)
        await RunWithStatsAsync(bit, 2);
      else if ((int)bit <= 4096)
        await RunWithStatsAsync(bit, 1);
      else break;

    Console.WriteLine();
  }

  public static async Task RunWithStatsAsync(
    BitPrimeLength bits, int count, int runs_per_length = 5)
  { 
    var options = new BigPrimesOption
    {
      BitLength = bits,
      MaxParallelism = Environment.ProcessorCount,
      MillerRabinRounds = PrimalityConfidence.Normal
    };

    //int parallelism = ServiceBigPrimesGenerator.GetAdaptiveParallelism(options.BitLength);

    var serialtimes = new List<double>();
    var paralleltimes = new List<double>();
    for (int run = 0; run < runs_per_length; run++)
    {
      // Serial
      var swserial = Stopwatch.StartNew();
      await BigPrimeGenerator.ToPrimesSerialAsync(options, count, CancellationToken.None);
      swserial.Stop();
      serialtimes.Add(swserial.Elapsed.TotalSeconds);

      // Parallel
      var swparallel = Stopwatch.StartNew();
      await BigPrimeGenerator.ToPrimesParallelAsync(options, count, CancellationToken.None);
      swparallel.Stop();
      paralleltimes.Add(swparallel.Elapsed.TotalSeconds);
    }

    // Statistik berechnen
    double avgserial = serialtimes.Average();
    double stdserial = Math.Sqrt(serialtimes.Select(t => Math.Pow(t - avgserial, 2)).Average());

    double avgparallel = paralleltimes.Average();
    double stdparallel = Math.Sqrt(paralleltimes.Select(t => Math.Pow(t - avgparallel, 2)).Average());

    Console.WriteLine($"Benchmark: {count} primes, {bits}, Confidence: Normal");
    Console.WriteLine($"Serial:   avg {avgserial:F2}s, std {stdserial:F2}");
    Console.WriteLine($"Parallel: avg {avgparallel:F2}s, std {stdparallel:F2}");
    Console.WriteLine($"Speed-up: {(avgserial / avgparallel):F2}×");
    Console.WriteLine();
  } 
}
