using MasterworkSimulation;
using MasterworkSimulation.Data;

const int trials = 100_000;
var engine = new MasterworkSimulationEngine();
RunSimulations(4, trials, "Unique");
RunSimulations(5, trials, "Legendary");

void RunSimulations(int affixCount, int trials, string title)
{
    var color = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine($"# {title} masterwork statistics");
    Console.ForegroundColor = color;

    var results = engine.GetSimulations(affixCount, trials)
        .OrderBy(t => t.Resets)
        .ToArray();

    Console.WriteLine("## Success Rates");
    Console.WriteLine("|Reset interval|Probability in interval|Total probability|");
    Console.WriteLine("|:----|----:|----:|");

    for (var from = 0; from < 150; from += 10)
    {
        var to = from + 9;
        var rate = (double)results.Count(t => t.Resets >= from && t.Resets <= to) / results.Length;
        var totalRate = (double)results.Count(t => t.Resets <= to) / results.Length;
        Console.WriteLine($"|{from}..{to}|{rate:P2}|{totalRate:P2}|");
    }

    Console.WriteLine("## Average");
    Console.WriteLine("|Obducide|Fogotten Souls|Gold|Resets|");
    Console.WriteLine("|----:|----:|----:|----:|");
    Console.WriteLine($"{(int)results.Average(t => t.Obducide)}|{(int)results.Average(t => t.FogottenSouls)}|{(int)results.Average(t => t.Gold)}|{(int)results.Average(t => t.Resets)}|");

    Console.WriteLine("## Percentile");
    Console.WriteLine("|Percentile|Obducide|Fogotten Souls|Gold|Resets|");
    Console.WriteLine("|----:|----:|----:|----:|----:|");
    WritePercentile(results, 98);
    WritePercentile(results, 95);
    WritePercentile(results, 90);
    WritePercentile(results, 75);
    WritePercentile(results, 50);

    Console.WriteLine();

    static void WritePercentile(SimulationResult[] results, int n)
    {
        var percentile = results[results.Length * n / 100];
        Console.WriteLine($"|{n}th|{percentile.Obducide}|{percentile.FogottenSouls}|{percentile.Gold}|{percentile.Resets}|");
    }
}