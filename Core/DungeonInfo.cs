namespace TEXT_RPG.Core
{
    internal class DungeonInfo
    {
        public int RequiredDef { get; set; }
        public int RequiredAtk { get; set; }
        public int RewardGold { get; set; }
        public int RewardExp { get; set; }

        public DungeonInfo(int requiredDef, int requiredAtk, int rewardGold, int rewardExp)
        {
            RequiredDef = requiredDef;
            RequiredAtk = requiredAtk;
            RewardGold = rewardGold;
            RewardExp = rewardExp;
        }
    }
}
