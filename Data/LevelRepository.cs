namespace TEXT_RPG.Data
{
    internal class LevelRepository
    {
        public static readonly Dictionary<int, int> RequiredExp = new()
        {
            { 1, 50 },
            { 2, 80 },
            { 3, 150 },
            { 4, 500 },
        };
    }
}
