using TEXT_RPG.Core.DTO;
using TEXT_RPG.Data;

namespace TEXT_RPG.Core
{
    internal class Item
    {
        public string Name { get; private set; }
        public ItemType Type { get; private set; }
        public int Value { get; private set; }
        public string Description { get; private set; }
        public bool IsEquipped { get; private set; }
        public int Price { get; private set; }

        public Item(string name, ItemType type, int value, string description, int price)
        {
            Name = name;
            Type = type;
            Value = value;
            Description = description;
            IsEquipped = false;
            Price = price;
        }

        public Item(string name, ItemType type, int value, string description)
        {
            Name = name;
            Type = type;
            Value = value;
            Description = description;
            IsEquipped = false;
            Price = 0;
        }

        public void ToggleEquip()
        {
            IsEquipped = !IsEquipped;
        }
        public ItemDto ToDto()
        {
            return new ItemDto
            {
                Name = Name,
                Type = Type,
                Value = Value,
                Description = Description,
                IsEquipped = IsEquipped,
                Price = Price
            };
        }

        public static Item FromDto(ItemDto dto)
        {
            var item = new Item(dto.Name, dto.Type, dto.Value, dto.Description, dto.Price);
            if (dto.IsEquipped) item.ToggleEquip();
            return item;
        }
    }
}
