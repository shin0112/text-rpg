namespace TEXT_RPG
{
    enum PlayerJob { 전사, 마법사, 궁수 }
    enum ItemType { Weapon, Armor }
    enum SceneType { Start, Status, Inventory, InventoryManagement, InventorySort, Shop, ShopPurchase }

    internal partial class Program
    {
        // scene
        public static StatusScene statusScene = new StatusScene();
        public static InventoryScene inventoryScene = new InventoryScene();
        public static ShopScene shopScene = new ShopScene();

        // object 정의
        private static Player player = new("강한 전사", PlayerJob.전사);
        private static Shop shop = new();
        public static Dictionary<SceneType, string[]> sceneSelections = new()
        {
            { SceneType.Start, ["", "상태 보기", "인벤토리", "랜덤 모험", "마을 순찰하기", "훈련하기", "상점"] },
            { SceneType.Status, ["나가기"] },
            { SceneType.Inventory, ["나가기", "장착 관리", "아이템 정렬"] },
            { SceneType.InventoryManagement, ["나가기"] },
            { SceneType.InventorySort, ["나가기", "이름", "장착순", "공격력", "방어력"] },
            { SceneType.Shop, ["나가기", "아이템 구매"] },
            { SceneType.ShopPurchase, ["나가기"] }
        };

        static void Main(string[] args)
        {
            ArgumentNullException.ThrowIfNull(args);
            UI.UIHelper.SetInitDesign();
            int select;

            // STEP 1
            while (true)
            {
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
                UI.UIHelper.WriteOptions(SceneType.Start, sceneSelections[SceneType.Start]);

                select = SelectAct(SceneType.Start);
                Console.Clear();

                switch (select)
                {
                    case 1:
                        statusScene.Show();
                        break;
                    case 2:
                        inventoryScene.Show();
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
                    case 6:
                        shopScene.Show();
                        break;
                    default:
                        UI.UIHelper.WarnBadInput();
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

        private static int SelectAct(SceneType type)
        {
            int count = GetSelectionCount(type);

            while (true)
            {
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    if (input < count && input > -1) { return input; }
                    else { UI.UIHelper.WarnBadInput(); }
                }
                else
                {
                    UI.UIHelper.WarnBadInput();
                }
            }
        }

        private static int GetSelectionCount(SceneType type)
        {
            return type switch
            {
                SceneType.InventoryManagement => player.Items.Count + 1,
                SceneType.ShopPurchase => shop.ShopEntries.Count + 1,
                _ => sceneSelections[type].Length
            };
        }

        class Player
        {
            public int Level { get; private set; }
            public string Name { get; private set; }
            public PlayerJob Job { get; private set; }
            public int AttackPower { get; private set; }
            public int DefensePower { get; private set; }
            public int Hp { get; private set; }
            public int Stamina { get; private set; }
            public int Exp { get; private set; }
            public int Gold { get; private set; }
            private Dictionary<ItemType, Item?> equipped = [];
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

            internal void GetItem(Item item) => Items.Add(item);

            public void UpdateGold(int gold) => Gold += gold;
            public void UpdateExp(int exp) => Exp += exp;

            public void SortItems(byte num)
            {
                switch (num)
                {
                    case 1: // 이름 길이 순
                        Items.Sort((item, another) => another.Name.Length.CompareTo(item.Name.Length));
                        break;
                    case 2: // 장착순
                        Items.Sort((item, another) => another.IsEquipped.CompareTo(item.IsEquipped));
                        break;
                    case 3: // 공격력
                        Items.Sort((item, another) =>
                        {
                            int ret = item.Type.CompareTo(another.Type);
                            return ret != 0 ? ret : another.Value.CompareTo(item.Value);
                        });
                        break;
                    case 4: // 방어력
                        Items.Sort((item, another) =>
                        {
                            int ret = another.Type.CompareTo(item.Type);
                            return ret != 0 ? ret : another.Value.CompareTo(item.Value);
                        });
                        break;
                    default:
                        break;
                }
            }

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
                foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
                {
                    equipped[type] = null;
                }

                // STEP 5
                Items =
                [
                    new Item("연습용 창", ItemType.Weapon, 3, "검보다는 그래도 창이 다루기 쉽죠."),
                    new Item("무쇠갑옷", ItemType.Armor, 5, "무쇠로 만들어져 튼튼한 갑옷입니다."),
                    new Item("낡은 검", ItemType.Weapon, 2, "쉽게 볼 수 있는 낡은 검 입니다."),
                ];
            }

            public (int attackPower, int defensePower) CalculatePlusPower()
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

            public void ToggleEquip(Item item)
            {
                ItemType type = item.Type;

                // 착용 중인 장비 해제 
                if (equipped[type] == item)
                {
                    item.ToggleEquip();
                    equipped[type] = null;
                    return;
                }

                // 다른 장비 해제
                if (equipped[type] != null)
                {
                    equipped[type]!.ToggleEquip();
                }

                // 새 장비 장착
                equipped[type] = item;
                item.ToggleEquip();
            }
        }

        public class Item
        {
            public string Name { get; private set; }
            public ItemType Type { get; private set; }
            public int Value { get; private set; }
            public string Description { get; private set; }
            public bool IsEquipped { get; private set; }
            public int Price { get; private set; }

            public Item(string name, ItemType type, int value, string description, int price)
            {
                Name = name;
                Type = type;
                Value = value;
                Description = description;
                IsEquipped = false;
                Price = price;
            }

            public Item(string name, ItemType type, int value, string description)
            {
                Name = name;
                Type = type;
                Value = value;
                Description = description;
                IsEquipped = false;
                Price = 0;
            }

            public void ToggleEquip()
            {
                IsEquipped = !IsEquipped;
            }
        }

        public class ShopEntry
        {
            public Item Item { get; private set; }
            public bool IsPurchased { get; private set; }

            public ShopEntry(Item item)
            {
                Item = item;
                IsPurchased = Item.Price == 0;
            }

            public void TogglePurchased()
            {
                IsPurchased |= !IsPurchased;
            }
        }

        public class Shop
        {
            public List<ShopEntry> ShopEntries { get; private set; }

            public Shop()
            {
                // 상점 아이템 리스트
                var items = new List<Item> {
                    new("수련자 갑옷", ItemType.Armor, 5, "수련에 도움을 주는 갑옷입니다.", 1000),
                    new("무쇠갑옷", ItemType.Armor, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 0),
                    new("스파르타의 갑옷", ItemType.Armor, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500),
                    new("낡은 검", ItemType.Weapon, 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600),
                    new("청동 도끼", ItemType.Weapon, 5, "어디선가 사용됐던거 같은 도끼입니다.", 1500),
                    new("스파르타의 창", ItemType.Weapon, 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 0)
                };

                ShopEntries = new();
                foreach (Item item in items)
                {
                    ShopEntries.Add(new ShopEntry(item));
                }
            }

            public static void PurchaseItem(ShopEntry shopEntry)
            {
                if (shopEntry.IsPurchased) // 이미 구매
                {
                    Console.WriteLine("이미 구매한 아이템입니다.");
                }


                if (shopEntry.Item.Price <= player.Gold) // 보유금액 충족
                {
                    player.UpdateGold(-shopEntry.Item.Price);
                    player.GetItem(shopEntry.Item);
                    shopEntry.TogglePurchased();
                    Console.WriteLine("구매를 완료했습니다.");
                }
                else // 보유 금액 미달
                {
                    Console.WriteLine("Gold가 부족합니다.");
                }
            }
        }
    }
}
