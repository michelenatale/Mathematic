



namespace BigPrimesGeneratorTest;

using michle.natale.BigPrimesGenerator;

public class Program
{
  public async static Task Main()
  {
    await BigPrimesBenchmarks.RunAsync();
    await BigPrimesBenchmarks.RunWithStatsAsync();


    Console.WriteLine();
    Console.WriteLine("FINISH");
    Console.ReadLine();
  }
}