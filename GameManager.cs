using TEXT_RPG.Core;
using TEXT_RPG.Scenes;
using TEXT_RPG.Scenes.Inventory;
using TEXT_RPG.Scenes.Shop;
using TEXT_RPG.UI;

namespace TEXT_RPG
{
    internal class GameManager
    {
        private static GameManager _instance;
        public static GameManager Instance = _instance ??= new();

        public Player Player { get; }
        public Shop Shop { get; }
        public Scene CurrentScene { get; private set; }
        public Dictionary<SceneType, string[]> ScenesSelections { get; }
        public Dictionary<SceneType, Scene> Scenes { get; }
        public bool InventoryNumbered { get; set; }

        public GameManager()
        {
            Player = new("아무개", PlayerJob.마법사);
            Shop = new();
            Scenes = new()
            {
                { SceneType.Start, new StartScene()},
                { SceneType.Status,  new StatusScene()},
                { SceneType.Inventory, new InventoryDefaultScene()},
                { SceneType.InventoryManagement, new InventoryManagementScene()},
                { SceneType.InventorySort, new InventorySortScene()},
                { SceneType.Shop,  new ShopScene()},
                { SceneType.ShopPurchase, new ShopPurchaseScene()}
            };
            CurrentScene = Scenes[SceneType.Start];

            ScenesSelections = new Dictionary<SceneType, string[]>
            {
                { SceneType.Start, ["", "상태 보기", "인벤토리", "랜덤 모험", "마을 순찰하기", "훈련하기", "상점"] },
                { SceneType.Status, ["나가기"] },
                { SceneType.Inventory, ["나가기", "장착 관리", "아이템 정렬"] },
                { SceneType.InventoryManagement, ["나가기"] },
                { SceneType.InventorySort, ["나가기", "이름", "장착순", "공격력", "방어력"] },
                { SceneType.Shop, ["나가기", "아이템 구매"] },
                { SceneType.ShopPurchase, ["나가기"] }
            };
        }

        public void ChangeScene(SceneType sceneType)
        {
            CurrentScene = Scenes[sceneType];
        }

        public int SelectAct()
        {
            int count = CurrentScene.SelectionCount;

            while (true)
            {
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    if (input < count && input > -1) { return input; }
                    else { UIHelper.WarnBadInput(); }
                }
                else
                {
                    UIHelper.WarnBadInput();
                }
            }
        }
    }
}
