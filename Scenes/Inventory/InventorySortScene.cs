using TEXT_RPG.UI;

namespace TEXT_RPG.Scenes.Inventory
{
    internal class InventorySortScene : Scene
    {
        protected override string Title => "인벤토리 - 아이템 정렬";
        public override string[] Options => ["나가기", "이름", "장착순", "공격력", "방어력"];

        protected override void HandleInput(int select)
        {
            throw new NotImplementedException();
        }

        public override void Show()
        {
            Console.Clear();
            PlayerUI.ShowInventory(Manager.Player.Items);
            UIHelper.WriteOptions();
            int select = Manager.SelectAct(SceneType.InventorySort);

            if (select == 0)
            {
                Console.Clear();
                Manager.ChangeScene(SceneType.Inventory);
            }
            else
            {
                Manager.Player.SortItems((byte)select);
            }
        }
    }
}
