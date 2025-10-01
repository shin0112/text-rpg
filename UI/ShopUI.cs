using TEXT_RPG.Core;

namespace TEXT_RPG.UI
{
    internal class ShopUI
    {
        public static void ShowShop(Player player, Shop shop)
        {
            UIHelper.WriteTitle("상점");
            Console.WriteLine("필요한 아이템을 필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G\n");

            Console.WriteLine("[아이템 목록]");
            foreach (ShopEntry shopEntry in shop.ShopEntries)
            {
                Console.Write("- ");
                ItemUI.ShowItemInfo(shopEntry.Item);
                Console.Write($" | {(shopEntry.IsPurchased ? "구매완료" : shopEntry.Item.Price + " G")}");
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
