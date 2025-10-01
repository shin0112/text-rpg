using TEXT_RPG.Core;
using TEXT_RPG.UI;

namespace TEXT_RPG
{
    internal partial class Program
    {
        static class UI
        {
            public class ShopUI
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

            public class PlayerUI
            {
                public static void ShowPlayerInfo(Player player)
                {
                    UIHelper.WriteTitle("상태 보기");
                    Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

                    // [attack, defnse]
                    (int atk, int def) = player.CalculatePlusPower();
                    Console.WriteLine($"Lv. {player.Level:D2}");
                    Console.WriteLine($"Chad ( {player.Job} )");
                    Console.WriteLine($"공격력 : {player.AttackPower} (+{atk})");
                    Console.WriteLine($"방어력 : {player.DefensePower} (+{def})");
                    Console.WriteLine($"체 력 : {player.Hp}");
                    Console.WriteLine($"스테미나 : {player.Stamina}");
                    Console.WriteLine($"경험치 : {player.Exp}");
                    Console.WriteLine($"Gold : {player.Gold} G");
                }

                public static void ShowInventory(SceneType showType, List<Core.Item> items)
                {
                    if (showType == SceneType.Inventory)
                    {
                        UIHelper.WriteTitle("인벤토리");
                    }
                    else
                    {
                        UIHelper.WriteTitle("인벤토리 - 장착 관리");
                    }
                    Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
                    Console.WriteLine("[아이템 목록]");
                    for (int i = 0; i < items.Count; i++)
                    {
                        Console.Write("- ");
                        if (showType == SceneType.InventoryManagement)
                        {
                            Console.Write($"{i + 1} ");
                        }
                        UI.ItemUI.ShowItemInfo(items[i]);
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
            }

            public class ItemUI
            {
                public static void ShowItemInfo(Core.Item item)
                {
                    string prefix = item.IsEquipped ? "[E]" : "";
                    string displayName = prefix + item.Name;

                    string statType = item.Type switch
                    {
                        ItemType.Weapon => "공격력 +",
                        ItemType.Armor => "방어력 +",
                        _ => ""
                    };
                    string displayStat = statType + item.Value;

                    string paddedName = UIHelper.GetPaddedString(displayName, 15);
                    string paddedStat = UIHelper.GetPaddedString(displayStat, 10);
                    string paddedDescription = UIHelper.GetPaddedString(item.Description, 50);

                    Console.Write($"{paddedName} | {paddedStat} | {paddedDescription}");
                }
            }
        }
    }
}
