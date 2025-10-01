namespace TEXT_RPG.UI
{
    internal class ShopUI
    {
        public static void ShowShop(string title)
        {
            UIHelper.WriteTitle(title);
            Console.WriteLine("필요한 아이템을 필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{GameManager.Instance.Player.Gold} G\n");

            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < GameManager.Instance.Shop.ShopEntries.Count; i++)
            {
                var shopEntry = GameManager.Instance.Shop.ShopEntries[i];

                Console.Write("- ");
                if (GameManager.Instance.ShopNumbered)
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
