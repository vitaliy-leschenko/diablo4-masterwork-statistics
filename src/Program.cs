using MasterworkSimulation;

// Setup environment
var engine = new MasterworkSimulationEngine();

// Run simulation
const int numRolls = 100_000;

var affixCount = 4;
var results = Enumerable.Range(0, numRolls).Select(_ => engine.GetSimulation(affixCount)).ToList();

// Output results
Console.WriteLine($"Average Obducide: {results.Average(t => t.Obducide)}, Soul: {results.Average(t => t.Soul)}, Gold: {results.Average(t => t.Gold)}, Resets: {results.Average(t => t.Resets)}");
for (var att = 10; att <= 100; att += 10)
{
    Console.WriteLine($"Success Rate (<= {att} resets): {(double)results.Count(t => t.Resets <= att) / numRolls:P2}");
}
