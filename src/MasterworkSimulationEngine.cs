using MasterworkSimulation.Data;

namespace MasterworkSimulation
{
    public class MasterworkSimulationEngine
    {
        private readonly Random rnd = new();
        private readonly Resource resetCost;
        private readonly Resource[] upgradeCosts;

        public MasterworkSimulationEngine()
        {
            Resource[] itemCosts = [
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
            resetCost = new(0, 1, 5000000);
            upgradeCosts = [.. itemCosts.Chunk(4).Select(chunk => chunk.Aggregate(Resource.Zero, (sum, item) => item + sum))];
        }

        public SimulationResult GetSimulation(int n = 5)
        {
            int count = 3;

            var resource = new Resource(0, 0, 0);
            int streak = 0;
            int resets = 0;
            while (true)
            {
                resource += upgradeCosts[streak];
                var roll = rnd.Next(n);
                if (roll == n - 1)
                {
                    streak++;
                    if (streak == count)
                    {
                        return (resource, resets);
                    }
                }
                else
                {
                    streak = 0;
                    resets++;
                    resource += resetCost;
                }
            }
        }
    }
}
