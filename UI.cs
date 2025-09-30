namespace TEXT_RPG
{
    internal partial class Program
    {
        static class UI
        {
            public class ItemUI
            {
                public static void WriteItemInfo(Item item)
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

                    string paddedName = UIHelper.GetPaddedString(displayName, 10);
                    string paddedStat = UIHelper.GetPaddedString(displayStat, 10);
                    string paddedDescription = UIHelper.GetPaddedString(item.Description, 30);

                    Console.WriteLine($"{paddedName} | {paddedStat} | {paddedDescription}");
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
                    Console.WriteLine(text);
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
