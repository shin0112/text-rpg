namespace TEXT_RPG.Core
{
    internal class ShopEntry
    {
        public Item Item { get; private set; }
        public bool IsPurchased { get; private set; }

        public ShopEntry(Item item)
        {
            Item = item;
            IsPurchased = Item.Price == 0;
        }

        public void TogglePurchased()
        {
            IsPurchased |= !IsPurchased;
        }
    }
}
