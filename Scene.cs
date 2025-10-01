namespace TEXT_RPG
{
    internal partial class Program
    {
        public abstract class Scene
        {
            public abstract void Show();
        }

        public class StatusScene : Scene
        {
            public override void Show()
            {
                int select;
                UI.PlayerUI.ShowPlayerInfo(player);
                UI.UIHelper.WriteOptions(SceneType.Status, sceneSelections[SceneType.Status]);
                select = SelectAct(SceneType.Status);
                if (select == 0)
                {
                    Console.Clear();
                    return;
                }
            }
        }

        public class InventoryScene : Scene
        {

            public override void Show()
            {
                int select;
                SceneType type = SceneType.Inventory;
                bool isExit = false;

                while (true)
                {
                    Console.Clear();
                    UI.PlayerUI.ShowInventory(type, player.Items);
                    UI.UIHelper.WriteOptions(type, sceneSelections[type]);
                    select = SelectAct(type);

                    switch (type)
                    {
                        case SceneType.Inventory:
                            isExit = InventoryDefault(select, ref type);
                            break;
                        case SceneType.InventoryManagement:
                            InventoryManagement(select, ref type);
                            break;
                        case SceneType.InventorySort:
                            InventorySort(select, ref type);
                            break;
                        default:
                            break;
                    }

                    if (isExit) break;
                }
            }

            private static void InventorySort(int select, ref SceneType type)
            {
                if (select == 0)
                {
                    Console.Clear();
                    type = SceneType.Inventory;
                }
                else
                {
                    player.SortItems((byte)select);
                }
            }

            private static void InventoryManagement(int select, ref SceneType type)
            {
                if (select == 0)
                {
                    Console.Clear();
                    type = SceneType.Inventory;
                }
                else if (0 < select && select <= player.Items.Count)
                {
                    player.ToggleEquip(player.Items[select - 1]);
                }
            }

            private static bool InventoryDefault(int select, ref SceneType type)
            {
                switch (select)
                {
                    case 0: // start
                        Console.Clear();
                        return true;
                    case 1: // 장착 관리
                        type = SceneType.InventoryManagement;
                        break;
                    case 2: // 아이템 정렬
                        type = SceneType.InventorySort;
                        break;
                    default:
                        break;
                }
                return false;
            }

        }

        public class ShopScene : Scene
        {
            public override void Show()
            {
                int select;
                SceneType type = SceneType.Shop;
                bool isExit = false;

                while (true)
                {
                    Console.Clear();
                    UI.ShopUI.ShowShop(player, shop);
                    UI.UIHelper.WriteOptions(type, sceneSelections[type]);
                    select = SelectAct(type);

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

            private static void ShopPurchase(int select, ref SceneType type)
            {
                if (select == 0) // 나가기
                {
                    Console.Clear();
                    type = SceneType.Shop;
                }
                else if (0 < select && select <= shop.ShopEntries.Count) // 구매
                {
                    Shop.PurchaseItem(shop.ShopEntries[select - 1]); // 실제 데이터 idx는 하나 더 작음
                }
            }

            private static bool ShopDefault(int select, ref SceneType type)
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
}
