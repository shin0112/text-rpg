namespace TEXT_RPG
{
    enum PlayerJob { 전사, 마법사, 궁수 }
    enum ItemType { Weapon, Armor }
    enum SceneType { Start, Status, Inventory, InventoryManagement }

    internal class Program
    {
        private static Player player;
        public static Dictionary<SceneType, string[]> sceneSelections = new Dictionary<SceneType, string[]>()
        {
            { SceneType.Start, ["", "상태 보기", "인벤토리", "랜덤 모험", "마을 순찰하기", "훈련하기"] },
            { SceneType.Status, ["나가기"] },
            { SceneType.Inventory, ["나가기", "장착 관리"] },
            { SceneType.InventoryManagement, ["나가기"] }
        };

        static void Main(string[] args)
        {
            UI.SetInitDesign();
            player = new Player("강한 전사", PlayerJob.전사);
            int select;

            // STEP 1
            while (true)
            {
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
                UI.WriteOptions(SceneType.Start, sceneSelections[SceneType.Start]);

                select = SelectAct(SceneType.Start);
                Console.Clear();

                switch (select)
                {
                    case 1:
                        SceneStatus();
                        break;
                    case 2:
                        SceneInventory(SceneType.Inventory);
                        break;
                    case 3:
                        SelectRandomAdventure();
                        break;
                    case 4:
                        SelectPatrolTown();
                        break;
                    case 5:
                        SelectTraining();
                        break;
                    default:
                        UI.WarnBadInput();
                        break;
                }
            }
        }

        private static void SelectTraining()
        {
            int encounterProb = new Random().Next(100);
            if (player.SpendStamina(15))
            {
                if (encounterProb < 15)
                {
                    Console.WriteLine("훈련이 잘 되었습니다!");
                    player.UpdateExp(60);
                }
                else if (encounterProb < 60)
                {
                    Console.WriteLine("오늘하루 열심히 훈련했습니다.");
                    player.UpdateExp(40);
                }
                else
                {
                    Console.WriteLine("하기 싫다... 훈련이...");
                    player.UpdateExp(30);
                }
            }
        }

        private static void SelectPatrolTown()
        {
            int encounterProb = new Random().Next(100);
            if (player.SpendStamina(5))
            {
                if (encounterProb < 10)
                {
                    Console.WriteLine("마을 아이들이 모여있다. 간식을 사줘볼까?");
                    player.UpdateGold(-500);
                }
                else if (encounterProb < 20)
                {
                    Console.WriteLine("촌장님을 만나서 심부름을 했다.");
                    player.UpdateGold(2000);
                }
                else if (encounterProb < 40)
                {
                    Console.WriteLine("길 읽은 사람을 안내해주었다.");
                    player.UpdateGold(1000);
                }
                else if (encounterProb < 70)
                {
                    Console.WriteLine("마을 주민과 인사를 나눴다. 선물을 받았다.");
                    player.UpdateGold(500);
                }
                else
                {
                    Console.WriteLine("아무 일도 일어나지 않았다");
                }
            }
        }

        private static void SelectRandomAdventure()
        {
            int encounterProb = new Random().Next(2);
            if (player.SpendStamina(10))
            {
                switch (encounterProb)
                {
                    case 0:
                        Console.WriteLine("몬스터 조우! 골드 500 획득");
                        player.UpdateGold(500);
                        break;
                    case 1:
                        Console.WriteLine("아무 일도 일어나지 않았다");
                        break;
                    default:
                        break;
                }
            }
        }

        private static void SceneStatus()
        {
            int select;
            player.ShowStatus();
            UI.WriteOptions(SceneType.Status, sceneSelections[SceneType.Status]);
            select = SelectAct(SceneType.Status);
            if (select == 0)
            {
                Console.Clear();
                return;
            }
        }

        private static void SceneInventory(SceneType type)
        {
            int select;
            bool isDefault = SceneType.Inventory == type;
            while (true)
            {
                Console.Clear();
                player.ShowInventory(type);
                UI.WriteOptions(type, sceneSelections[type]);
                select = isDefault
                    ? SelectAct(SceneType.Inventory)
                    : SelectAct(SceneType.InventoryManagement);

                if (isDefault)
                {
                    if (select == 0)
                    {
                        Console.Clear();
                        return;
                    }
                    else if (select == 1)
                    {
                        isDefault = false;
                        type = SceneType.InventoryManagement;
                        continue;
                    }
                }
                else
                {
                    if (select == 0)
                    {
                        Console.Clear();
                        isDefault = true;
                        type = SceneType.Inventory;
                        continue;
                    }
                    else if (0 < select && select <= player.Items.Count)
                    {
                        player.Items[select - 1].ToggleEquip();
                        continue;
                    }
                }
            }
        }

        private static int SelectAct(SceneType type)
        {
            int count = (type == SceneType.InventoryManagement)
                ? player.Items.Count + 1
                : sceneSelections[type].Length;

            while (true)
            {
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    if (input < count && input > -1) { return input; }
                    else { UI.WarnBadInput(); }
                }
                else
                {
                    UI.WarnBadInput();
                }
            }
        }


        class Player
        {
            private int Level { get; set; }
            private string Name { get; set; }
            private PlayerJob Job { get; set; }
            private int AttackPower { get; set; }
            private int DefensePower { get; set; }
            private int Hp { get; set; }
            private int Stamina { get; set; }
            private int Exp { get; set; }
            private int Gold { get; set; }

            public List<Item> Items { get; }

            public bool SpendStamina(int stamina)
            {
                if (Stamina >= stamina)
                {
                    Stamina -= stamina;
                    return true;
                }
                else
                {
                    Console.WriteLine("스테미나가 부족합니다.");
                    return false;
                }
            }

            public void UpdateGold(int gold) => Gold += gold;
            public void UpdateExp(int exp) => Exp += exp;

            // STEP 2
            public Player(string name, PlayerJob job)
            {
                Level = 1;
                Name = name;
                Job = job;
                AttackPower = 10;
                DefensePower = 5;
                Hp = 100;
                Stamina = 20;
                Exp = 0;
                Gold = 1500;

                // STEP 5
                Items = new List<Item>();
                Items.Add(new Item("무쇠갑옷", ItemType.Armor, 5, "무쇠로 만들어져 튼튼한 갑옷입니다."));
                Items.Add(new Item("낡은 검", ItemType.Weapon, 2, "쉽게 볼 수 있는 낡은 검 입니다."));
                Items.Add(new Item("연습용 창", ItemType.Weapon, 3, "검보다는 그대로 창이 다루기 쉽죠."));
            }

            // STEP 3 & 7
            public void ShowStatus()
            {
                UI.WriteTitle("===상태 보기 ===");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

                // [attack, defnse]
                (int atk, int def) = CalculatePlusPower();
                Console.WriteLine($"Lv. {Level:D2}");
                Console.WriteLine($"Chad ( {Job.ToString()} )");
                Console.WriteLine($"공격력 : {AttackPower} (+{atk})");
                Console.WriteLine($"방어력 : {DefensePower} (+{def})");
                Console.WriteLine($"체 력 : {Hp}");
                Console.WriteLine($"스테미나 : {Stamina}");
                Console.WriteLine($"경험치 : {Exp}");
                Console.WriteLine($"Gold : {Gold} G");
            }

            // STEP 5
            public void ShowInventory(SceneType showType)
            {
                if (showType == SceneType.Inventory)
                {
                    UI.WriteTitle("=== 인벤토리 ===");
                }
                else
                {
                    UI.WriteTitle("=== 인벤토리 - 장착 관리 ===");
                }
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < Items.Count; i++)
                {
                    Console.Write("- ");
                    if (showType == SceneType.InventoryManagement)
                    {
                        Console.Write($"{i + 1} ");
                    }
                    Items[i].ShowItemInfo();
                }
                Console.WriteLine();
            }

            private (int attackPower, int defensePower) CalculatePlusPower()
            {
                int attackPower = 0, defensePower = 0;
                foreach (Item item in Items)
                {
                    if (!item.IsEquipped) continue;

                    if (item.Type == ItemType.Weapon) { attackPower += item.Value; }
                    else if (item.Type == ItemType.Armor) { defensePower += item.Value; }
                }
                return (attackPower, defensePower);
            }
        }

        class Item
        {
            private string Name { get; set; }
            public ItemType Type { get; private set; }
            public int Value { get; private set; }
            private string Description { get; set; }
            public bool IsEquipped { get; private set; }

            public Item(string name, ItemType type, int value, string description)
            {
                Name = name;
                Type = type;
                Value = value;
                Description = description;
                IsEquipped = false;
            }

            public void ShowItemInfo()
            {
                Console.Write($"{(IsEquipped ? "[E]" : "") + Name,-12} | ");
                switch (Type)
                {
                    case ItemType.Weapon:
                        Console.Write("공격력 +");
                        break;
                    case ItemType.Armor:
                        Console.Write("방어력 +");
                        break;
                    default:
                        break;
                }
                Console.WriteLine($"{Value} | {Description}");
            }

            public void ToggleEquip()
            {
                IsEquipped = !IsEquipped;
            }
        }
    }
    static class UI
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

        public static void WritePadded(string text, int totalWidth)
        {
            int displaywidth = GetDisplayWidth(text);
            int padding = Math.Max(0, totalWidth - displaywidth);
            Console.WriteLine(text + new string(' ', padding));
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
