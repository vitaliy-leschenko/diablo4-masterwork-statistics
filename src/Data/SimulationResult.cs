namespace MasterworkSimulation.Data
{
    public record SimulationResult(int Obducide, int FogottenSouls, long Gold, int Resets) : Resource(Obducide, FogottenSouls, Gold)
    {
        public static implicit operator SimulationResult((Resource Resource, int Resets) data)
        {
            var (obducide, fogottenSouls, gold) = data.Resource;
            return new SimulationResult(obducide, fogottenSouls, gold, data.Resets);
        }
    }
}