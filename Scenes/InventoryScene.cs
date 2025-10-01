using TEXT_RPG.UI;

namespace TEXT_RPG.Scenes
{
    internal class InventoryScene : Scene
    {
        public override void Show()
        {
            int select;
            SceneType type = SceneType.Inventory;
            bool isExit = false;

            while (true)
            {
                Console.Clear();
                PlayerUI.ShowInventory(type, Manager.Player.Items);
                UIHelper.WriteOptions(type, Manager.Scenes[type]);
                select = Manager.SelectAct(type);

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

        private void InventorySort(int select, ref SceneType type)
        {
            if (select == 0)
            {
                Console.Clear();
                type = SceneType.Inventory;
            }
            else
            {
                Manager.Player.SortItems((byte)select);
            }
        }

        private void InventoryManagement(int select, ref SceneType type)
        {
            if (select == 0)
            {
                Console.Clear();
                type = SceneType.Inventory;
            }
            else if (0 < select && select <= Manager.Player.Items.Count)
            {
                Manager.Player.ToggleEquip(Manager.Player.Items[select - 1]);
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
}
