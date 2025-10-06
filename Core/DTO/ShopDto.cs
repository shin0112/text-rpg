namespace TEXT_RPG.Core.DTO
{
    internal record class ShopDto
    {
        public List<ShopEntryDto> ShopEntries { get; init; } = [];
    }
}
