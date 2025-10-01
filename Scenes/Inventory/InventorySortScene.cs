namespace TEXT_RPG.Scenes.Inventory
{
    internal class InventorySortScene : InventorySceneBase
    {
        protected override string Title => "인벤토리 - 아이템 정렬";
        public override string[] Options => ["나가기", "이름", "장착순", "공격력", "방어력"];

        protected override void HandleInput(int select)
        {
            if (select == 0)
            {
                Manager.ChangeScene(SceneType.Inventory);
            }
            else
            {
                Manager.Player.SortItems((byte)select);
            }
        }
    }
}
