// Setup environment
var rnd = new Random();

var resetCost = new Resource(0, 1, 5000000);
Resource[] upgradeCosts = [
    new Resource(  10,  1,   64000),
    new Resource(  20,  1,   86000),
    new Resource(  30,  2,  110000),
    new Resource(  40,  2,  150000),
    new Resource(  60,  6,  270000),
    new Resource( 120,  7,  360000),
    new Resource( 240,  8,  480000),
    new Resource( 360, 10,  800000),
    new Resource( 450, 11, 1000000),
    new Resource( 900, 12, 1400000),
    new Resource(1350, 13, 1800000),
    new Resource(2250, 15, 3000000),
];
var levelCosts = upgradeCosts
    .Chunk(4)
    .Select(chunk => new Resource(
        chunk.Sum(c => c.Obducide), 
        chunk.Sum(c => c.Soul), 
        chunk.Sum(c => c.Gold)))
    .ToArray();

// Run simulation
int numRolls = 1_000_000;
int attempts = 10;
int count = 0;
var results = new List<Resource>(numRolls);
for (int t = 0; t < numRolls; t++)
{
    var result = MasterworkSimulation(4, 3);
    results.Add(result);
    if (result.Resets <= attempts)
    {
        count++;
    }
}

// Output results
Console.WriteLine($"Average Obducide: {results.Average(t => t.Obducide)}, Soul: {results.Average(t => t.Soul)}, Gold: {results.Average(t => t.Gold)}, Resets: {results.Average(t => t.Resets)}");
for (var att = 10; att <= 100; att += 10)
{
    Console.WriteLine($"Success Rate (<= {att} resets): {(double)results.Count(t=>t.Resets <= att) / numRolls:P2}");
}

Resource MasterworkSimulation(int n = 5, int count = 3)
{
    var resource = new Resource(0, 0, 0);
    int streak = 0;
    while (true)
    {
        resource = new Resource(
            resource.Obducide + levelCosts[streak].Obducide,
            resource.Soul + levelCosts[streak].Soul,
            resource.Gold + levelCosts[streak].Gold,
            resource.Resets
        );
        var roll = rnd.Next(n);
        if (roll == n - 1)
        {
            streak++;
            if (streak == count)
            {
                return resource;
            }
        }
        else
        {
            streak = 0;
            resource = new Resource(
                resource.Obducide + resetCost.Obducide,
                resource.Soul + resetCost.Soul,
                resource.Gold + resetCost.Gold,
                resource.Resets + 1
            );
        }
    }
}

record Resource(int Obducide, int Soul, long Gold, int Resets = 0);