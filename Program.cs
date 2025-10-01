using TEXT_RPG.Core;

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

        static void Main(string[] args)
        {
            ArgumentNullException.ThrowIfNull(args);

            GameManager gameManager = new();
            Player player = gameManager.Player;
            Shop shop = gameManager.Shop;
            var sceneSelections = gameManager.Scenes;

            UI.UIHelper.SetInitDesign();
            int select;

            // STEP 1
            while (true)
            {
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
                UI.UIHelper.WriteOptions(SceneType.Start, sceneSelections[SceneType.Start]);

                select = SelectAct(SceneType.Start, player, shop, sceneSelections);
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
                        SelectRandomAdventure(player);
                        break;
                    case 4:
                        SelectPatrolTown(player);
                        break;
                    case 5:
                        SelectTraining(player);
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

        private static void SelectTraining(Player player)
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

        private static void SelectPatrolTown(Player player)
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

        private static void SelectRandomAdventure(Player player)
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

        private static int SelectAct(
            SceneType type,
            Player player,
            Shop shop, Dictionary<SceneType,
                string[]> sceneSelections)
        {
            int count = GetSelectionCount(type, player, shop, sceneSelections);

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

        private static int GetSelectionCount(
            SceneType type,
            Player player,
            Shop shop,
            Dictionary<SceneType, string[]> sceneSelections)
        {
            return type switch
            {
                SceneType.InventoryManagement => player.Items.Count + 1,
                SceneType.ShopPurchase => shop.ShopEntries.Count + 1,
                _ => sceneSelections[type].Length
            };
        }
    }
}
