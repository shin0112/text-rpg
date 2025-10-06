using TEXT_RPG.Data;

namespace TEXT_RPG.Core.DTO
{
    internal record class ItemDto
    {
        public string Name { get; init; } = string.Empty;
        public ItemType Type { get; init; }
        public int Value { get; init; }
        public string Description { get; init; } = string.Empty;
        public bool IsEquipped { get; init; }
        public int Price { get; init; }
    }
}
