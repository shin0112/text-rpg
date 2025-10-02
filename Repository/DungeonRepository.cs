using TEXT_RPG.Core;

namespace TEXT_RPG.Repository
{
    internal class DungeonRepository
    {
        public static readonly Dictionary<DungeonLevel, DungeonInfo> Dungeons = new()
        {
            // 방어력, 공격력, 골드, 경험치
            { DungeonLevel.쉬운, new DungeonInfo(5, 0, 1000, 50) },
            { DungeonLevel.일반, new DungeonInfo(11, 0, 1700, 100) },
            { DungeonLevel.어려운, new DungeonInfo(17, 0, 2500, 200) }
        };
    }
}
