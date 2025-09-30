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
                    UI.UIHelper.WriteTitle("상점");
                    Console.WriteLine("필요한 아이템을 필요한 아이템을 얻을 수 있는 상점입니다.\n");

                    Console.WriteLine("[보유 골드]");
                    Console.WriteLine($"{player.Gold} G\n");

                    Console.WriteLine("[아이템 목록]");
                    foreach (var keyValue in shop.Items)
                    {
                        Console.Write("- ");
                        ItemUI.ShowItemInfo(keyValue.Key);
                        Console.Write($" | {(keyValue.Value ? "구매완료" : keyValue.Key.Price + " G")}");
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
            }

            public class PlayerUI
            {
                public static void ShowPlayerInfo(Player player)
                {
                    UI.UIHelper.WriteTitle("상태 보기");
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

                public static void ShowInventory(SceneType showType, List<Item> items)
                {
                    if (showType == SceneType.Inventory)
                    {
                        UI.UIHelper.WriteTitle("인벤토리");
                    }
                    else
                    {
                        UI.UIHelper.WriteTitle("인벤토리 - 장착 관리");
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
                public static void ShowItemInfo(Item item)
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

            public class UIHelper
            {
                public static void SetInitDesign()
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                public static void WriteTitle(string text)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"=== {text} ===");
                    SetInitDesign();
                }

                public static void WriteOption(string text)
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine(text);
                    SetInitDesign();
                }

                public static void WriteExitOption()
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("\n0. 나가기");
                    SetInitDesign();
                }

                public static void WriteOptions(SceneType type, string[] selections)
                {
                    for (int i = 1; i < selections.Length; i++)
                    {
                        WriteOption($"{i}. {selections[i]}");
                    }

                    if (type == SceneType.Start) { return; }

                    WriteExitOption();
                }

                public static void WarnBadInput()
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }

                public static string GetPaddedString(string text, int totalWidth)
                {
                    int displaywidth = GetDisplayWidth(text);
                    int padding = Math.Max(0, totalWidth - displaywidth);
                    return text + new string(' ', padding);
                }

                private static int GetDisplayWidth(string text)
                {
                    int width = 0;
                    foreach (char c in text)
                    {
                        width += (c >= 0xAC00 && c <= 0xD7A3) ? 2 : 1; // 한글 탐지
                    }
                    return width;
                }
            }
        }
    }
}
