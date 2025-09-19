namespace MasterworkSimulation.Data
{
    public record Resource(int Obducide, int Soul, long Gold)
    {
        public static Resource Zero => new(0, 0, 0);

        public static Resource operator +(Resource a, Resource b) => new(
            a.Obducide + b.Obducide,
            a.Soul + b.Soul,
            a.Gold + b.Gold
        );
    }
}
