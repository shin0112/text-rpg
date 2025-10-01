namespace TEXT_RPG.Scenes.Shop
{
    internal class ShopScene : ShopSceneBase
    {
        protected override string Title => "상점";
        public override string[] Options => ["나가기", "아이템 구매"];

        protected override void HandleInput(int select)
        {
            switch (select)
            {
                case 0: // 시작
                    Manager.ChangeScene(SceneType.Start);
                    break;
                case 1: // 아이템 구매
                    Manager.ChangeScene(SceneType.ShopPurchase);
                    break;
                default:
                    break;
            }
        }
        private void ShopPurchase(int select, ref SceneType type)
        {
            if (select == 0) // 나가기
            {
                Console.Clear();
                type = SceneType.Shop;
            }
            else if (0 < select && select <= Manager.Shop.ShopEntries.Count) // 구매
            {
                Manager.Shop.PurchaseItem(Manager.Shop.ShopEntries[select - 1], Manager.Player); // 실제 데이터 idx는 하나 더 작음
            }
        }

        private bool ShopDefault(int select, ref SceneType type)
        {
            switch (select)
            {
                case 0:
                    Console.Clear();
                    return true;
                default:
                    type = SceneType.ShopPurchase;
                    return false;
            }
        }
    }
}
