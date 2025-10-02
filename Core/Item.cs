using Newtonsoft.Json;
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

        [JsonConstructor]
        public Item(string name, ItemType type, int value, string description, int price, bool isEquipped)
        {
            Name = name;
            Type = type;
            Value = value;
            Description = description;
            Price = price;
            IsEquipped = isEquipped;
        }

        public void ToggleEquip()
        {
            IsEquipped = !IsEquipped;
        }
    }
}
