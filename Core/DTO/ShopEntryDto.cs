namespace TEXT_RPG.Core.DTO
{
    internal record class ShopEntryDto
    {
        public required ItemDto Item { get; init; }
        public bool IsPurchased { get; init; }
    }
}
