namespace TEXT_RPG.UI
{
    internal class ShopUI
    {
        public static void ShowShop(string title)
        {
            GameManager manager = GameManager.Instance;

            UIHelper.WriteTitle(title);
            Console.WriteLine("필요한 아이템을 필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{manager.Player.Gold} G\n");

            Console.WriteLine("[아이템 목록]");
            if (manager.InventoryNumbered)
            {
                ShowSellShopEntry(manager);
            }
            else
            {
                ShowShopEntry(manager);
            }
        }

        private static void ShowSellShopEntry(GameManager manager)
        {
            var items = manager.Player.Items;

            for (int i = 0; i < items.Count; i++)
            {
                Console.Write($"- {i + 1} ");
                ItemUI.ShowItemInfo(items[i]);
                Console.Write($" | {(int)(items[i].Price * 0.85)} G");
                Console.WriteLine();
            }
        }

        private static void ShowShopEntry(GameManager manager)
        {

            for (int i = 0; i < manager.Shop.ShopEntries.Count; i++)
            {
                var shopEntry = manager.Shop.ShopEntries[i];

                Console.Write("- ");
                if (manager.ShopNumbered)
                {
                    Console.Write($"{i + 1} ");
                }
                ItemUI.ShowItemInfo(shopEntry.Item);
                Console.Write($" | {(shopEntry.IsPurchased ? "구매완료" : shopEntry.Item.Price + " G")}");
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
