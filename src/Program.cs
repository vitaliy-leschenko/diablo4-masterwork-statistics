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

    var prev = -1;
    for (var att = 10; att <= 150; prev = att, att += 10)
    {
        var rate = (double)results.Count(t => t.Resets <= att) / results.Length;
        var rate2 = (double)results.Count(t => t.Resets > prev && t.Resets <= att) / results.Length;
        Console.WriteLine($"|{att - 10}..{att}|{rate2:P2}|{rate:P2}|");
    }

    Console.WriteLine("## Average");
    Console.WriteLine($"`Obducide: {results.Average(t => t.Obducide)}, Fogotten Souls: {results.Average(t => t.FogottenSouls)}, Gold: {results.Average(t => t.Gold)}, Resets: {results.Average(t => t.Resets)}`");

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