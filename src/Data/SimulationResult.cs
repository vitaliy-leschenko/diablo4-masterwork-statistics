namespace MasterworkSimulation.Data
{
    public record SimulationResult(int Obducide, int Soul, long Gold, int Resets) : Resource(Obducide, Soul, Gold)
    {
        public static implicit operator SimulationResult((Resource Resource, int Resets) data)
        {
            var (obducide, soul, gold) = data.Resource;
            return new SimulationResult(obducide, soul, gold, data.Resets);
        }
    }
}