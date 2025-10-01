using TEXT_RPG.Core;

namespace TEXT_RPG
{
    internal class GameManager
    {
        public Player Player { get; }
        public Shop Shop { get; }
        public Dictionary<SceneType, string[]> Scenes { get; }

        public GameManager()
        {
            Player = new("아무개", PlayerJob.마법사);
            Shop = new();
            Scenes = new Dictionary<SceneType, string[]>
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
    }
}
