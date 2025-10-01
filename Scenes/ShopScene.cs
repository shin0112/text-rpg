using TEXT_RPG.Core;

namespace TEXT_RPG.Scenes
{
    internal class ShopScene : Scene
    {
        public override void Show()
        {
            int select;
            SceneType type = SceneType.Shop;
            bool isExit = false;

            while (true)
            {
                Console.Clear();
                UI.ShopUI.ShowShop(type);
                UI.UIHelper.WriteOptions(type, Manager.Scenes[type]);
                select = Manager.SelectAct(type);

                switch (type)
                {
                    case SceneType.Shop:
                        isExit = ShopDefault(select, ref type);
                        break;
                    case SceneType.ShopPurchase: // 아이템 구매
                        ShopPurchase(select, ref type);
                        break;
                    default:
                        break;
                }

                if (isExit) break;
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
                Shop.PurchaseItem(Manager.Shop.ShopEntries[select - 1], Manager.Player); // 실제 데이터 idx는 하나 더 작음
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
