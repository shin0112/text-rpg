using TEXT_RPG.Data;

namespace TEXT_RPG.Scenes.Shop
{
    internal class ShopPurchaseScene : ShopSceneBase
    {
        protected override string Title => "상점";
        public override string[] Options => ["나가기"];
        public override byte SelectionCount => (byte)(Options.Length + Manager.Shop.ShopEntries.Count);

        protected override void HandleInput(int select)
        {
            if (select == 0) // 그만두기
            {
                Manager.ChangeScene(SceneType.Shop);
                ToggleShopNumbered();
            }
            else if (0 < select && select <= Manager.Shop.ShopEntries.Count) // 구매
            {
                Manager.Shop.PurchaseItem(Manager.Shop.ShopEntries[select - 1], Manager.Player); // 실제 데이터 idx는 하나 더 작음
            }
        }
    }
}
