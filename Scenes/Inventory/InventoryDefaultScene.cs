using TEXT_RPG.UI;

namespace TEXT_RPG.Scenes.Inventory
{
    internal class InventoryDefaultScene : Scene
    {
        protected override string Title => "인벤토리";
        public override string[] Options => ["나가기", "장착 관리", "아이템 정렬"];

        protected override void HandleInput(int select)
        {
            switch (select)
            {
                case 0:
                    Manager.ChangeScene(SceneType.Start);
                    break;
            }
        }

        public override void Show()
        {
            int select;
            SceneType type = SceneType.Inventory;
            bool isExit = false;

            Console.Clear();
            PlayerUI.ShowInventory(Manager.Player.Items);
            UIHelper.WriteOptions();
            select = Manager.SelectAct(type);

            Manager.CurrentScene.Show();
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
