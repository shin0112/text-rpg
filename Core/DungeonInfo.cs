namespace TEXT_RPG.Core
{
    internal class DungeonInfo
    {
        public int RequiredAtk { get; set; }
        public int RequiredDef { get; set; }
        public int RewardGold { get; set; }
        public int RewardExp { get; set; }

        public DungeonInfo(int requiredAtk, int requiredDef, int rewardGold, int rewardExp)
        {
            RequiredAtk = requiredAtk;
            RequiredDef = requiredDef;
            RewardGold = rewardGold;
            RewardExp = rewardExp;
        }
    }
}
