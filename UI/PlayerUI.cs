using TEXT_RPG.Core;

namespace TEXT_RPG.UI
{
    internal class PlayerUI
    {
        public static void ShowPlayerInfo()
        {
            Player player = GameManager.Instance.Player;

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

        public static void ShowInventoryHeader(string title)
        {
            UIHelper.WriteTitle(title);
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
        }

        public static void ShowInventory(bool numbered = false)
        {
            List<Item> items = GameManager.Instance.Player.Items;

            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < items.Count; i++)
            {
                Console.Write("- ");
                if (numbered)
                {
                    Console.Write($"{i + 1} ");
                }
                ItemUI.ShowItemInfo(items[i]);
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
