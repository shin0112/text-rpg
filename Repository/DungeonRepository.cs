using TEXT_RPG.Core;

namespace TEXT_RPG.Repository
{
    internal class DungeonRepository
    {
        public static readonly Dictionary<DungeonLevel, DungeonInfo> Dungeons = new()
        {
            { DungeonLevel.Easy, new DungeonInfo(5, 0, 1000, 50) },
            { DungeonLevel.Normal, new DungeonInfo(11, 0, 1700, 100) },
            { DungeonLevel.Hard, new DungeonInfo(17, 0, 2500, 200) }
        };
    }
}
