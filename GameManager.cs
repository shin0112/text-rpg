using TEXT_RPG.Core;
using TEXT_RPG.Core.DTO;
using TEXT_RPG.Data;
using TEXT_RPG.Scenes;
using TEXT_RPG.Scenes.Dungeon;
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
        public Dictionary<SceneType, Scene> Scenes { get; }
        public bool InventoryNumbered { get; set; } = false;
        public bool ShopNumbered { get; set; } = false;
        public string HeaderText { get; set; } = "";
        public DungeonResultDto? LastDungeonResult { get; set; }

        public GameManager()
        {
            Player = new("아무개", PlayerJob.마법사);
            Shop = new();
            Scenes = new()
            {
                { SceneType.Start, new StartScene() },
                { SceneType.Status,  new StatusScene() },
                { SceneType.Inventory, new InventoryDefaultScene() },
                { SceneType.InventoryManagement, new InventoryManagementScene() },
                { SceneType.InventorySort, new InventorySortScene() },
                { SceneType.Shop,  new ShopScene() },
                { SceneType.ShopPurchase, new ShopPurchaseScene() },
                { SceneType.ShopSell, new ShopSellScene() },
                { SceneType.Dungeon, new DungeonEnterScene() },
                { SceneType.DungeonClear, new DungeonClearScene() },
                { SceneType.Rest, new RestScene() }
            };
            CurrentScene = Scenes[SceneType.Start];
        }

        public void ResetHeaderText() => HeaderText = "";
        public void WarnBadInput() => HeaderText = "잘못된 입력입니다.";
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
                    else { WarnBadInput(); }
                }
                else
                {
                    WarnBadInput();
                }
            }
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                UIHelper.WriteHeader();
                Instance.ResetHeaderText();
                Instance.CurrentScene.Show();
            }
        }
    }
}
