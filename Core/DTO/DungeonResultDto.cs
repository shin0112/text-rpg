namespace TEXT_RPG.Core.DTO
{
    internal class DungeonResultDto
    {
        public DungeonLevel Level { get; set; }
        // Before
        public int BeforeHp { get; set; }
        public int BeforeGold { get; set; }
        public int BeforeExp { get; set; }

        // After
        public int AfterHp { get; set; }
        public int AfterGold { get; set; }
        public int AfterExp { get; set; }

        public bool IsSuccess { get; set; }

        public DungeonResultDTO(
            DungeonLevel level,
            int beforeHp,
            int beforeGold,
            int beforeExp,
            int afterHp,
            int afterGold,
            int afterExp,
            bool isSuccess = true)
        {
            Level = level;

            BeforeHp = beforeHp;
            BeforeGold = beforeGold;
            BeforeExp = beforeExp;

            AfterHp = afterHp;
            AfterGold = afterGold;
            AfterExp = afterExp;

            IsSuccess = isSuccess;
        }
    }
}
