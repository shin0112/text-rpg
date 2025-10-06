using TEXT_RPG.Data;

namespace TEXT_RPG.Core.DTO
{
    internal record class PlayerDto
    {
        public int Level { get; init; }
        public string Name { get; init; } = string.Empty;
        public PlayerJob Job { get; init; }
        public float AttackPower { get; init; }
        public float DefensePower { get; init; }
        public int Hp { get; init; }
        public int Stamina { get; init; }
        public int Exp { get; init; }
        public int Gold { get; init; }
        public List<ItemDto> Items { get; init; } = [];
        public Dictionary<ItemType, ItemDto?> Equipped { get; init; } = [];
    }
}
