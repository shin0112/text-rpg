namespace TEXT_RPG.Scenes.Shop
{
    internal class ShopScene : ShopSceneBase
    {
        protected override string Title => "상점";
        public override string[] Options => ["나가기", "아이템 구매", "아이템 판매"];

        protected override void HandleInput(int select)
        {
            switch (select)
            {
                case 0: // 시작
                    Manager.ChangeScene(SceneType.Start);
                    break;
                case 1: // 아이템 구매
                    Manager.ChangeScene(SceneType.ShopPurchase);
                    ToggleShopNumbered();
                    break;
                case 2: // 아이템 판매
                    Manager.ChangeScene(SceneType.ShopSell);
                    ToggleInventoryNumbered();
                    break;
                default:
                    break;
            }
        }
    }
}
