namespace TEXT_RPG
{
    internal class Program
    {
        private static Player player;
        private static Dictionary<string, string> selectCount = new Dictionary<string, string>()
        {
            { "start", "3" },
            { "status", "1" },
            { "inventory", "2" },
            { "manage", "2" }
        };

        static void Main(string[] args)
        {
            player = new Player("", PlayerJob.전사);

            // STEP 1
            while (true)
            {
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
                Console.WriteLine("1. 상태 보기\n2. 인벤토리");

                switch (selectAct("start"))
                {
                    case 1:
                        Console.Clear();
                        player.ShowStatus();
                        Console.WriteLine("0. 나가기");
                        if (selectAct("status") == 0)
                        {
                            Console.Clear();
                            continue;
                        }
                        break;
                    case 2:
                        Console.Clear();
                        player.ShowInventory(ShowType.Default);
                        Console.WriteLine("1. 장착 관리");
                        Console.WriteLine("0. 나가기");
                        if (selectAct("inventory") == 0)
                        {
                            Console.Clear();
                            continue;
                        }
                        else if (selectAct("manage") == 1)
                        {

                        }
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.\n");
                        continue;
                }
            }
        }

        private static int selectAct(string type)
        {
            int count = int.Parse(selectCount.GetValueOrDefault(type) ?? "0");
            while (true)
            {
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    if (input < count && input > -1) return input;
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                }
            }
        }

        enum PlayerJob { 전사, 마법사, 궁수 }
        enum ItemType { Weapon, Armor }
        enum ShowType { Default, Management }

        class Player
        {
            public int Level { get; private set; }
            public string Name { get; private set; }
            public PlayerJob Job { get; private set; }
            public int AttackPower { get; private set; }
            public int DefensePower { get; private set; }
            public int Hp { get; private set; }
            public int Gold { get; private set; }
            public List<Item> items = new List<Item>();

            // STEP 2
            public Player(string name, PlayerJob job)
            {
                Level = 1;
                Name = name;
                Job = job;
                AttackPower = 10;
                DefensePower = 5;
                Hp = 100;
                Gold = 1500;

                // STEP 5
                items.Add(new Item("무쇠갑옷", ItemType.Armor, 5, "무쇠로 만들어져 튼튼한 갑옷입니다."));
                items.Add(new Item("낡은 검", ItemType.Weapon, 2, "쉽게 볼 수 있는 낡은 검 입니다."));
                items.Add(new Item("연습용 창", ItemType.Weapon, 3, "검보다는 그대로 창이 다루기 쉽죠."));
            }

            // STEP 3
            public void ShowStatus()
            {
                Console.WriteLine("상태 보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

                Console.WriteLine($"Lv. {Level:D2}");
                Console.WriteLine($"Chad ( {Job.ToString()} )");
                Console.WriteLine($"공격력 : {AttackPower}");
                Console.WriteLine($"방어력 : {DefensePower}");
                Console.WriteLine($"체 력 : {Hp}");
                Console.WriteLine($"Gold : {Gold} G");

                Console.WriteLine();
            }

            // STEP 5
            public void ShowInventory(ShowType showType)
            {
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].IsEquipped == true)
                    {
                        Console.Write("[E] ");
                    }
                    Console.Write("- ");
                    items[i].showItemInfo();
                }
                Console.WriteLine();
            }
        }

        class Item
        {
            private string Name { get; set; }
            private ItemType Type { get; set; }
            private int Value { get; set; }
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

            public void showItemInfo()
            {
                Console.Write($"{Name,-12} | ");
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
        }
    }
}
