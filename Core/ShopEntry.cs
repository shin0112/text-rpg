namespace TEXT_RPG.Core
{
    internal class ShopEntry
    {
        public Item Item { get; private set; }
        public bool IsPurchased { get; private set; }

        public ShopEntry(Item item, bool isPurchased = false)
        {
            Item = item;
            IsPurchased = isPurchased;
        }

        public void TogglePurchased()
        {
            IsPurchased |= !IsPurchased;
        }
    }
}
