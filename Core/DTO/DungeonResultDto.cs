namespace TEXT_RPG.Core.DTO
{
    internal class DungeonResultDto
    {
        public int HpLost { get; set; }
        public int RewardGold { get; set; }
        public int RewardExp { get; set; }

        public DungeonResultDto(int hpLost, int rewardGold, int rewardExp)
        {
            HpLost = hpLost;
            RewardGold = rewardGold;
            RewardExp = rewardExp;
        }
    }
}
